import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { PRIORITY_EMAILS } from "src/app/constants/api.constants";
import { PriorityEmailModel } from "../../models/priority-email.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class PriorityEmailsService {
  constructor(private http: HttpClient) {}

  public getAllPriorityEmails(): Observable<PriorityEmailModel[]> {
    return this.http.get<PriorityEmailModel[]>(PRIORITY_EMAILS);
  }

  public addPriorityEmail(emailSuffix: string): Observable<PriorityEmailModel> {
    return this.http.post<PriorityEmailModel>(PRIORITY_EMAILS, {
      emailSuffix
    });
  }

  public removePriorityEmail(id: string) {
    return this.http.delete(PRIORITY_EMAILS + `/${id}`);
  }
}
