import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DeterminationGraphModel } from "src/app/models/determination-graph/determination-graph.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class DeterminationGraphService {
  constructor(private http: HttpClient) {}

  getGraph(): Observable<DeterminationGraphModel> {
    // return this.http.get<DeterminationGraphModel>("../../assets/determination-test2.json");
    return this.http.get<DeterminationGraphModel>(
      "https://localhost:5001/api/determination-graph"
    );
  }

  setGraph(
    model: DeterminationGraphModel
  ): Observable<DeterminationGraphModel> {
    return this.http.post<DeterminationGraphModel>(
      "https://localhost:5001/api/determination-graph",
      model
    );
  }
}
