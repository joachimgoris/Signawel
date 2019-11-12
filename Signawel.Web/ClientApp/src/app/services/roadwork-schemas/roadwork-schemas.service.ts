import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { RoadworkSchemaModel } from "src/app/models/RoadworkSchema.model";
import { RoadworkSchemaResult } from "src/app/models/roadwork-schema-result.model";

@Injectable({
  providedIn: "root"
})
export class RoadworkSchemasService {
  constructor(private http: HttpClient) {}

  public searchRoadworkSchemas(
    search: string,
    sort: string,
    pageIndex: number,
    pageLimit: number
  ): Observable<RoadworkSchemaResult> {
    let url = "https://localhost:5001/api/roadwork-schemas";
    return this.http.get<RoadworkSchemaResult>(url, {
      params: new HttpParams()
        .set("search", search)
        .set("sort", sort)
        .set("page", pageIndex.toString())
        .set("limit", pageLimit.toString())
    });
  }
}
