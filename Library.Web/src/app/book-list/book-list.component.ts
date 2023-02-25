import { Component, EventEmitter, OnInit, Output} from '@angular/core';
import {HttpService} from "../services/http.service";
import {BookWithRatingAndRevsCount} from "../../models/input/inputmodels";
import { Observable } from "rxjs";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  allBooks$: Observable<BookWithRatingAndRevsCount[]>
  recommendedBooks$: Observable<BookWithRatingAndRevsCount[]>
  @Output() changeToEditMode = new EventEmitter<number>();

  constructor(private httpclient: HttpService) {
  }

  updateAll() {
    this.allBooks$ = this.httpclient.getAll();
  }

  updateRecommended() {
    this.recommendedBooks$ = this.httpclient.getRecommended();
  }

  ngOnInit(): void {
    this.allBooks$ = this.httpclient.getAll();
    this.recommendedBooks$ = this.httpclient.getRecommended();
  }

  onClickEditBook(id: number) {
    this.changeToEditMode.emit(id);
  }

  updateBooks() {
    this.updateRecommended();
    this.updateAll();
  }
  afterSubmit() {
    this.updateBooks();
  }

  onBookDelete() {
    this.updateBooks();
  }
}
