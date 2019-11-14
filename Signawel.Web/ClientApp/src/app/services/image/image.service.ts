import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ImageResponseModel } from "src/app/models/image-response.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class ImageService {
  constructor(private http: HttpClient) {}

  postImage(file: File): Observable<ImageResponseModel> {
    let url = "https://localhost:5001/api/images";

    const formData = new FormData();
    formData.append("file", file);

    return this.http.post<ImageResponseModel>(url, formData);
  }
}
