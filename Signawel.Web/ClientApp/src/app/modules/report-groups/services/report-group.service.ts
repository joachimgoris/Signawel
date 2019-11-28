import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { REPORT_GROUPS } from "src/app/constants/api.constants";
import {CITIES} from "src/app/constants/api.constants";
import { ReportGroupCreationRequestModel } from "../models/report-group-creation-request-model";
import { CityResponseModel } from '../models/city-response-model';
import { ReportGroupResponseModel } from '../models/report-group-response-model';

@Injectable({
  providedIn: 'root'
})
export class ReportGroupService {

  constructor(private http: HttpClient) { }

  setReportGroup(
    model: ReportGroupCreationRequestModel
  ): Observable<ReportGroupCreationRequestModel> {
    return this.http.post<ReportGroupCreationRequestModel>(REPORT_GROUPS, model);
  }

  getCities(): Observable<CityResponseModel[]> {
    return this.http.get<CityResponseModel[]>(CITIES);
  }

  deleteReport(id: string): Observable<ReportGroupResponseModel>{
    return this.http.delete<ReportGroupResponseModel>(REPORT_GROUPS,{
      params: new HttpParams()
      .set("id",id)
    });
  }

  getReportGroups(
    city: string,
    mail: string
  ): Observable<ReportGroupResponseModel[]> {
    return this.http.get<ReportGroupResponseModel[]>(REPORT_GROUPS, {
      params: new HttpParams()
        .set("city", city)
        .set("mail", mail)
    });
  }
}
