import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IMAGES } from "src/app/constants/api.constants";
import { ImageResponseModel } from "../../models/image-response.model";

@Injectable({
  providedIn: "root"
})
export class ImageService {
  constructor(private http: HttpClient) {}

  postImage(file: File): Observable<ImageResponseModel> {
    const formData = new FormData();
    formData.append("file", file);

    return this.http.post<ImageResponseModel>(IMAGES, formData);
  }
}
