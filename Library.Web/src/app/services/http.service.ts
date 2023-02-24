import {HttpClient} from "@angular/common/http";
import {BookFullDetail, BookWithRatingAndRevsCount} from "../../models/input/inputmodels";
import {CreateBook, MakeARate, MakeAReview} from "../../models/output/outputmodels";
import {Observable} from "rxjs";
import {map} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private baseUrl: string = "https://localhost:5000/api";
  constructor(private http: HttpClient) {
  }

  getAll(): Observable<BookWithRatingAndRevsCount[]> {
    let fullUrl: string = this.baseUrl + `/books?order=title`;
    let books = this.http.get<BookWithRatingAndRevsCount[]>(fullUrl);
    return books;
  }

  getRecommended (genre?: string): Observable<BookWithRatingAndRevsCount[]> {
    let fullUrl: string = this.baseUrl + "/recommended" + `?genre=${genre == undefined? '' : genre}`;
    let books = this.http.get<BookWithRatingAndRevsCount[]>(fullUrl);
    return books;
  }

  getBookDetail(id: number): Observable<BookFullDetail> {
    let fullUrl: string = this.baseUrl + "/books/" + id;
    let book = this.http.get<BookFullDetail>(fullUrl);
    return book;
  }

  deleteBook(secret: string, id: number) {
    let fullUrl: string = this.baseUrl + `/${id}?secret=${secret}`;
    this.http.delete(fullUrl);
  }

  saveBook(book: CreateBook): Observable<number> {
    let fullUrl = this.baseUrl + "/books/save";
    let res = this.http.post<number>(fullUrl, book);
    return res;
  }


  makeAReview(id: number, review: MakeAReview): Observable<number> {
    let fullUrl = this.baseUrl + `/books/${id}/review`;
    let res = this.http.put<number>(fullUrl, review);
    return res;
  }

  makeARate(id:number, rate: MakeARate)  {
    let fullUrl = this.baseUrl + `/books/${id}/rate`;
    let res = this.http.put<number>(fullUrl, rate);
  }
}
