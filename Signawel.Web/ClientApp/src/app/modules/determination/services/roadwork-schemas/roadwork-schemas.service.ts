import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";
import { ROADWORK_SCHEMAS } from "src/app/constants/api.constants";
import { RoadworkSchemaResult } from "../../models/roadwork-schema-result.model";
import { RoadworkSchemaModel } from "../../models/roadwork-schema.model";

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
    return this.http.get<RoadworkSchemaResult>(ROADWORK_SCHEMAS, {
      params: new HttpParams()
        .set("search", search)
        .set("sort", sort)
        .set("page", pageIndex.toString())
        .set("limit", pageLimit.toString())
    });
  }

  public getRoadworkSchema(id: string): Observable<RoadworkSchemaModel> {
    return this.http.get<RoadworkSchemaModel>(ROADWORK_SCHEMAS + `/${id}`);
  }

  public updateRoadworkSchema(
    model: RoadworkSchemaModel
  ): Observable<RoadworkSchemaModel> {
    return this.http.put<RoadworkSchemaModel>(
      ROADWORK_SCHEMAS + `/${model.id}`,
      model
    );
  }

  public createRoadworkSchema(
    model: RoadworkSchemaModel
  ): Observable<RoadworkSchemaModel> {
    return this.http.post<RoadworkSchemaModel>(ROADWORK_SCHEMAS, model);
  }

  public deleteRoadworkSchema(id: string) {
    return this.http.delete<RoadworkSchemaModel>(ROADWORK_SCHEMAS + `/${id}`);
  }
}
