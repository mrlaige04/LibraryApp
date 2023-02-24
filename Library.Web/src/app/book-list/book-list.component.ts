import {Component, OnInit} from '@angular/core';
import {HttpService} from "../services/http.service";
import {BookWithRatingAndRevsCount} from "../../models/input/inputmodels";
import {map, Observable, tap} from "rxjs";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  allBooks$: Observable<BookWithRatingAndRevsCount[]>
  recommendedBooks$: Observable<BookWithRatingAndRevsCount[]>
  waiting: boolean = true;
  constructor(private httpclient: HttpService) {

  }

  ngOnInit(): void {
    this.allBooks$ = this.httpclient.getAll();
    this.recommendedBooks$ = this.httpclient.getRecommended();
  }
}
