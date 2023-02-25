import {Component, EventEmitter, Input, OnInit, Output, AfterContentChecked, SimpleChanges} from '@angular/core';
import {BookFullDetail} from "../../models/input/inputmodels";
import {HttpService} from "../services/http.service";
import {map, Observable} from "rxjs";
import {BookListItemComponent} from "../book-list-item/book-list-item.component";

import {CreateBook} from "../../models/output/outputmodels";
import {HttpClient, HttpHeaders} from "@angular/common/http";

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.component.html',
  styleUrls: ['./view-book.component.css']
})
export class ViewBookComponent implements OnInit {
  public book: BookFullDetail;
  @Input() id: number;
  @Input() isOpened: boolean = false;
  book$: Observable<BookFullDetail>;
  @Output() onClose = new EventEmitter<boolean>();

  constructor(private http: HttpService, private client: HttpClient) {
  }

  close(increase: boolean) {
    this.onClose.emit(increase);
  }

  public Open(id: number) {
    this.book$ = this.http.getBookDetail(id)
  }




  ngOnInit() : void {
    this.Open(this.id);
  }
}
