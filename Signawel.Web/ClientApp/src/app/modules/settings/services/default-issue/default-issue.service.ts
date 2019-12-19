import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DefaultIssueModel } from "../../models/default-issue.model";
import { DEFAULT_ISSUES } from "src/app/constants/api.constants";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class DefaultIssueService {
  constructor(private http: HttpClient) {}

  public getAllDefaultIssues(): Observable<DefaultIssueModel[]> {
    return this.http.get<DefaultIssueModel[]>(DEFAULT_ISSUES);
  }

  public addDefaultIssue(
    defaultIssue: DefaultIssueModel
  ): Observable<DefaultIssueModel> {
    return this.http.post<DefaultIssueModel>(DEFAULT_ISSUES, defaultIssue);
  }

  public removeDefaultIssue(id: string) {
    return this.http.delete(DEFAULT_ISSUES + `/${id}`);
  }
}
