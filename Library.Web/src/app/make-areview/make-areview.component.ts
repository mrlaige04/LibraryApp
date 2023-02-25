import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpService} from "../services/http.service";

@Component({
  selector: 'app-make-areview',
  templateUrl: './make-areview.component.html',
  styleUrls: ['./make-areview.component.css']
})
export class MakeAReviewComponent {
  private reviewTemplate = {
    reviewer: '',
    message: ''
  }
  @Input() bookId : number | undefined;
   reviewGroup: FormGroup;
  @Output() onAddReview = new EventEmitter();

  constructor(private httpService: HttpService) {
    this.reviewGroup = new FormGroup({
      'reviewer': new FormControl('', [Validators.required, Validators.minLength(3)]),
      'message' : new FormControl('', [Validators.required, Validators.minLength(3)])
    })
  }

  submit(form: HTMLFormElement) {
    if (this.reviewGroup.valid && this.bookId != undefined) {
      this.httpService.makeAReview(this.bookId!, this.reviewGroup.value).subscribe(
        data=>{
          console.log("review id: " + data);
        },
        error=>{
          console.log("Error while adding a review: " + error.json())
        },
        ()=>{
          console.log("Success")
          this.reviewGroup.setValue(this.reviewTemplate);
          this.onAddReview.emit();
        }
      );
    }
  }


}
