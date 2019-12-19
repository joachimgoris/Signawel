import { AfterViewInit, Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { merge, fromEvent } from "rxjs";
import { tap, debounceTime, distinctUntilChanged } from "rxjs/operators";

import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ReportService } from '../../services/report.service';
import { ReportsOverviewDataSource } from '../../datasources/reports-overview-datasource';
import { BladeModalService } from 'src/app/modules/shared/services/blade-modal.service';
import { ReportModel } from '../../models/report.model';
import { ModalCloseEvent } from 'src/app/modules/shared/components/blade-modal/modal-close-event';

@Component({
  selector: 'reports-overview',
  templateUrl: './reports-overview.component.html',
  styleUrls: ['./reports-overview.component.sass']
})
export class ReportsOverviewComponent implements AfterViewInit, OnInit {
  displayedColumns = ['senderEmail', 'creationTime', 'issue', 'description', 'actions'];
  selectedReport: ReportModel;

  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: false}) sort: MatSort;
  @ViewChild("searchInput", { static: false }) searchInput: ElementRef;
  
  listData: ReportsOverviewDataSource;

  constructor(private reportService: ReportService,
              private modalService: BladeModalService) {}

  ngOnInit() {
    this.listData = new ReportsOverviewDataSource(this.reportService);
    this.listData.loadReports();
  }

  ngAfterViewInit(): void {
    fromEvent(this.searchInput.nativeElement, "keyup")
      .pipe(
        debounceTime(1000),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.loadReports();
        })
      )
      .subscribe();

    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(tap(() => this.loadReports()))
      .subscribe();
  }

  loadReports() {
    this.listData.loadReports(
      this.searchInput.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize
    );
  }

  doShow(report: ReportModel) {
    this.selectedReport = report;
    this.modalService.open("reportEditor");
  }

  doRemove(report: ReportModel) {
    this.reportService.deleteReport(report.id).subscribe(res => {
      this.loadReports();
    });
  }

  onModalClose(event: ModalCloseEvent) {
    if (event.reason == "background") {
      event.preventDefault();
      return;
    }

    this.selectedReport = null;
  }
}