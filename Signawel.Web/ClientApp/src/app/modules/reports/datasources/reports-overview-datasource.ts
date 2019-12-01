import { DataSource, CollectionViewer } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { Observable, of as observableOf, merge, BehaviorSubject } from 'rxjs';
import { ReportModel } from '../models/report.model';
import { ReportService } from '../services/report.service';

export class ReportsOverviewDataSource extends DataSource<ReportModel> {
  paginator: MatPaginator;
  total: number;
  private subject: BehaviorSubject<ReportModel[]> = new BehaviorSubject<
  ReportModel[]>([]);

  constructor(private reportService: ReportService) {
    super();
  }

  connect(collectionViewer: CollectionViewer): Observable<ReportModel[]> {
    return this.subject.asObservable();
  }

  disconnect() {
    this.subject.complete();
  }

  loadReports(search: string = "",
              sortDirection: string = "asc",
              pageIndex: number = 0,
              pageSize: number = 15) {
    this.reportService.searchReports(search, sortDirection, pageIndex, pageSize)
    .subscribe(result => {
      this.total = result.total;
      this.subject.next(result.reports);
    })
  }
}
