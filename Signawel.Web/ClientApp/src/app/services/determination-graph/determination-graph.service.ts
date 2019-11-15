import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DeterminationGraphModel } from "src/app/models/determination-graph/determination-graph.model";
import { Observable } from "rxjs";
import { DETERMINATION_GRAPH } from "src/app/constants/api.constants";

@Injectable({
  providedIn: "root"
})
export class DeterminationGraphService {
  constructor(private http: HttpClient) {}

  getGraph(): Observable<DeterminationGraphModel> {
    return this.http.get<DeterminationGraphModel>(DETERMINATION_GRAPH);
  }

  setGraph(
    model: DeterminationGraphModel
  ): Observable<DeterminationGraphModel> {
    return this.http.post<DeterminationGraphModel>(DETERMINATION_GRAPH, model);
  }
}
