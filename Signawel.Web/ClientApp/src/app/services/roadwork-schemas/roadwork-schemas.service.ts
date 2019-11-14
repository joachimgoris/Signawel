import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";
import { RoadworkSchemaModel } from "src/app/models/RoadworkSchema.model";
import { RoadworkSchemaResult } from "src/app/models/roadwork-schema-result.model";
import { registerLocaleData } from "@angular/common";
import { map } from "rxjs/operators";

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

  public getRoadworkSchema(id: string): Observable<RoadworkSchemaModel> {
    let url = "https://localhost:5001/api/roadwork-schemas/" + id;
    return this.http.get<RoadworkSchemaModel>(url);
  }

  public updateRoadworkSchema(
    model: RoadworkSchemaModel
  ): Observable<RoadworkSchemaModel> {
    let url = "https://localhost:5001/api/roadwork-schemas/" + model.id;
    return this.http.put<RoadworkSchemaModel>(url, model);
  }

  public createRoadworkSchema(
    model: RoadworkSchemaModel
  ): Observable<RoadworkSchemaModel> {
    let url = "https://localhost:5001/api/roadwork-schemas";
    return this.http.post<RoadworkSchemaModel>(url, model);
  }

  public deleteRoadworkSchema(id: string) {
    let url = "https://localhost:5001/api/roadwork-schemas/" + id;
    return this.http.delete<RoadworkSchemaModel>(url);
  }
}
