import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DeterminationGraphModel } from "src/app/models/determination-graph/determination-graph.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class DeterminationGraphService {
  constructor(private http: HttpClient) {}

  getSchema(): Observable<DeterminationGraphModel> {
    return this.http.get<DeterminationGraphModel>(
      "../../assets/determination-test.json"
    );
  }
}
