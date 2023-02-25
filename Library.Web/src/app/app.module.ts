import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';


import { AppComponent } from './app.component';
import { BooksPageComponent } from './books-page/books-page.component';
import { BookListComponent } from './book-list/book-list.component';
import { BookListItemComponent } from './book-list-item/book-list-item.component';
import { EditBookComponent } from './edit-book/edit-book.component';
import { ViewBookComponent } from './view-book/view-book.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FormsModule, NG_VALUE_ACCESSOR} from "@angular/forms";
import {ReactiveFormsModule} from "@angular/forms";
import { DeleteBookComponent } from './delete-book/delete-book.component';
import {HttpService} from "./services/http.service";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ɵEmptyOutletComponent} from "@angular/router";
import {ImageconverterService} from "./services/imageconverter.service";

@NgModule({
  declarations: [
    AppComponent,
    BooksPageComponent,
    BookListComponent,
    BookListItemComponent,
    EditBookComponent,
    ViewBookComponent,
    DeleteBookComponent

  ],
    imports: [
        BrowserModule,
        HttpClientModule,
        BrowserAnimationsModule,
        FormsModule,
        ɵEmptyOutletComponent,
        ReactiveFormsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
