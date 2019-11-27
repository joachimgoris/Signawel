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
    const data = new FormData();
    data.append("value", JSON.stringify(model, this.hideImageFromJson));

    if (model.image) {
      data.append("files", model.image);
    }

    return this.http.put<RoadworkSchemaModel>(
      ROADWORK_SCHEMAS + `/${model.id}`,
      data
    );
  }

  public createRoadworkSchema(
    model: RoadworkSchemaModel
  ): Observable<RoadworkSchemaModel> {
    const data = new FormData();

    data.append("files", model.image);
    data.append("value", JSON.stringify(model, this.hideImageFromJson));

    return this.http.post<RoadworkSchemaModel>(ROADWORK_SCHEMAS, data);
  }

  private hideImageFromJson(key, value) {
    if (key == "model.image") return undefined;
    if (key == "model.imageId") return undefined;
    else return value;
  }

  public deleteRoadworkSchema(id: string) {
    return this.http.delete<RoadworkSchemaModel>(ROADWORK_SCHEMAS + `/${id}`);
  }
}
