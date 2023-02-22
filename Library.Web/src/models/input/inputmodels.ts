class BookWithRatingAndRevsCount {
  constructor(public id: number, public title: string,
              public author: string, public rating: number,
              public reviewsNumber: number, public cover: string) {
  }
}


class BookFullDetail {
  constructor(public id: number, public title: string,
              public author: string, public cover: string,
              public content:string, public genre:string,
              public reviews: Array<Review>) {
  }
}

class Review {
  constructor(public id: number, public message:string, public reviewer: string) {
  }
}

class Recommended {
  constructor(public book: Array<BookWithRatingAndRevsCount>) {
  }
}
export {BookWithRatingAndRevsCount, BookFullDetail, Review, Recommended}
