class CreateBook {
  constructor(public id: number, public title: string,
              public author: string, public cover: string,
              public content:string, public genre: string) {
  }
}

class MakeARate {
  constructor(public rating: number) {
  }
}

class MakeAReview {
  constructor(public message: string, public reviewer: string) {
  }
}

export {CreateBook, MakeARate, MakeAReview}
