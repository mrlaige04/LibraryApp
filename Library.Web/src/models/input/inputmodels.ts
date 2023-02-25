class BookWithRatingAndRevsCount {
  constructor(public id: number, public title: string,
              public author: string, public rating: number,
              public reviewsNumber: number, public cover: string) {
  }
}

class BookFullDetail {
  public id: number;
  public title:string;
  public author: string;
  public cover: string;
  public content: string;
  public genre: string;
  public reviews: Review[];
  public rating: number;
}

class Review {
  constructor(public id: number, public message:string, public reviewer: string) {
  }
}

export {BookWithRatingAndRevsCount, BookFullDetail, Review}
