import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { REPORTS } from 'src/app/constants/api.constants';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ReportResult } from '../models/report-result.model';

@Injectable({
    providedIn: "root"
})
export class ReportService {
    constructor(private http: HttpClient) { }

    searchReports(search: string,
        sort: string,
        pageIndex: number,
        pageLimit: number,
    ): Observable<ReportResult> {
        return this.http.get<ReportResult>(REPORTS, {
            params: new HttpParams()
                .set("search", search)
                .set("sort", sort)
                .set("page", pageIndex.toString())
                .set("limit", pageLimit.toString())
        });
    }
}