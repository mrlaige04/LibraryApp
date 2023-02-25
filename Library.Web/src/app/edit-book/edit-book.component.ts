import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import { map} from "rxjs";
import {HttpService} from "../services/http.service";

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent implements OnInit{
  bookForm: FormGroup;
  srcImage: string;
  @Input() isEditMode: boolean = false;

  private initialFormState = {
    title: '',
    author: '',
    cover: '',
    content: '',
    genre: '',
    id: 0
  };

  @Input() selectedEditBookId: number;
  @Output() onSubmitted = new EventEmitter();

  constructor(private formBuilder: FormBuilder,
              private httpService: HttpService) {}
  clearForm(form: HTMLFormElement) {
    form.reset()
    this.bookForm.setValue(this.initialFormState);
    this.srcImage = "";
    this.isEditMode = false;
    this.selectedEditBookId = 0;
  }

  ngOnInit() {
    this.bookForm = new FormGroup({
      id: new FormControl(this.initialFormState.id),
      title: new FormControl(this.initialFormState.title, Validators.required),
      author: new FormControl(this.initialFormState.author, Validators.required),
      cover: new FormControl(this.initialFormState.cover, [Validators.required, Validators.pattern("^data:image\\/(jpeg|jpg|png);base64,(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?")]),
      content: new FormControl(this.initialFormState.content, Validators.required),
      genre: new FormControl(this.initialFormState.genre, Validators.required)
    });
  }

  onSubmit(form: HTMLFormElement) {
    if (this.bookForm.valid) {
      this.httpService.saveBook(this.bookForm.value).subscribe(
        data => {},
        error => {
          console.log("Error: " + error);
        },
        () => {
          this.afterSubmit();
        }
      );
      console.log(this.bookForm.value);
      this.clearForm(form);
    }
  }

  onFileChange(event: any, fileinput: HTMLInputElement) {
    // Функція для конвертації картинки в base64
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      if (!/['.jpg','.png','.jpeg']$/.test(file.name)) {
        fileinput.value = "";
        console.log('Invalid file');
        return;
      }
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.bookForm.patchValue({
          cover: reader.result
        });
        this.srcImage = reader.result!.toString();
      };
    }
  }

  onEnableEditMode(id: number) {
    console.log("EDIT MODE ENABLED");
    console.log("BOOK ID: " + id);
    this.isEditMode = true;
    this.selectedEditBookId = id;

    this.httpService.getBookDetail(id)
      .pipe(map((bookhttp)=>{
        return {
          title: bookhttp.title,
          author: bookhttp.author,
          cover: bookhttp.cover,
          content: bookhttp.content,
          genre: bookhttp.genre,
          id: bookhttp.id
        }
      }))
      .subscribe(
      {next:(book)=> {
          console.log("Got book");
          this.srcImage = book.cover;
          this.bookForm.setValue(book);
        }}
      );
  }

  disableEditMode(form: HTMLFormElement) {
    console.log("EDIT MODE DISABLED");
    this.isEditMode = false;
    this.selectedEditBookId = 0;
    this.clearForm(form);
    this.srcImage = '';
  }

  afterSubmit() {
    this.onSubmitted.emit();
    this.isEditMode = false;
    this.selectedEditBookId = 0;
  }
}
