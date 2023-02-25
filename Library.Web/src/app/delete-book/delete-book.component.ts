import {Component, EventEmitter, Output} from '@angular/core';
import {HttpService} from "../services/http.service";
import {FormControl, FormGroup, Validators} from "@angular/forms";
@Component({
  selector: 'app-delete-book',
  templateUrl: './delete-book.component.html',
  styleUrls: ['./delete-book.component.css']
})
export class DeleteBookComponent {

  @Output() onDeleted = new EventEmitter();
  deleteGroup: FormGroup;

  private defGroup = {
    'id': 0,
    'secret': ''
  }
  constructor(private httpService: HttpService) {
    this.deleteGroup = new FormGroup( {
      'id': new FormControl(0, Validators.required),
      'secret': new FormControl('', Validators.required)
    })
  }

  submit(form: HTMLFormElement) {
    if (this.deleteGroup.valid) {
      this.httpService.deleteBook(this.deleteGroup.value.secret, this.deleteGroup.value.id).subscribe(
        data => {
        },
        error => {
          console.log(error == null)
          if (error != null && error != undefined) console.log("Error: " + error?.json());
        },
        () => {
          console.log("deleted")
          this.onDeleted.emit();
        });

      this.clearForm(form);
      this.deleteGroup.setValue(this.defGroup);
    }
  }

  clearForm(form: HTMLFormElement) {
    form.reset()
  }
}
