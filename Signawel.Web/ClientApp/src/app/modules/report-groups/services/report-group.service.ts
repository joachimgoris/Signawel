import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { REPORT_GROUPS, USERS } from "src/app/constants/api.constants";
import {CITIES} from "src/app/constants/api.constants";
import { ReportGroupCreationRequestModel } from "../models/report-group-creation-request-model";
import { CityResponseModel } from '../models/city-response-model';
import { ReportGroupResponseModel } from '../models/report-group-response-model';
import { UserResponseModel } from '../models/user-response-model';

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

  modifyReportGroup(
    id: string,
    model: ReportGroupCreationRequestModel
  ): Observable<ReportGroupResponseModel> {
    return this.http.put<ReportGroupResponseModel>(REPORT_GROUPS + `/${id}`, model);
  }

  getCities(): Observable<CityResponseModel[]> {
    return this.http.get<CityResponseModel[]>(CITIES);
  }

  getUsers(
    user:string
  ): Observable<UserResponseModel[]> {
    return this.http.get<UserResponseModel[]>(USERS,{
      params: new HttpParams()
      .set("user",user)
    });
  }

  deleteReport(id: string): Observable<ReportGroupResponseModel>{
    return this.http.delete<ReportGroupResponseModel>(REPORT_GROUPS,{
      params: new HttpParams()
      .set("id",id)
    });
  }

  getReportGroups(
    city: string,
    mail: string,
    user: string
  ): Observable<ReportGroupResponseModel[]> {
    return this.http.get<ReportGroupResponseModel[]>(REPORT_GROUPS, {
      params: new HttpParams()
        .set("city", city)
        .set("mail", mail)
        .set("user",user)
    });
  }
}
