import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class ImageconverterService {
  convertFileToBase64(file: File): string {

    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      return reader.result;
    }
    reader.onerror = function (ev) {
      console.log(ev);
      console.log("ERROR")
    }
    return "";
  }
}
