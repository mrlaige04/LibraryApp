import {Component, ContentChild, ElementRef, EventEmitter, ViewChild} from '@angular/core';
import {Input, Output} from "@angular/core";
import {BookFullDetail, BookWithRatingAndRevsCount} from "../../models/input/inputmodels";
import {HttpService} from "../services/http.service";
import {map, Observable} from "rxjs";
import {ViewBookComponent} from "../view-book/view-book.component";

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.component.html',
  styleUrls: ['./book-list-item.component.css']
})
export class BookListItemComponent {
  public toggle: boolean = false;
  @Input() book : BookWithRatingAndRevsCount
  @Output() onEditClick = new EventEmitter<number>();

  constructor(private http: HttpService) {
  }


  editBook() {
    this.onEditClick.emit(this.book.id);
  }
}
