import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportsRoutingModule } from './reports-routing.module';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { SignawelMaterialModule } from '../signawel-material/signawel-material.module';
import { SharedModule } from '../shared/shared.module';
import { ReportDetailComponent } from './components/report-detail/report-detail.component';
import { FormsModule } from '@angular/forms';
import { ReportsOverviewComponent } from './components/reports-overview/reports-overview.component';


@NgModule({
  declarations: [ReportsOverviewComponent, ReportDetailComponent],
  imports: [
    CommonModule,
    ReportsRoutingModule,
    SharedModule,
    SignawelMaterialModule,
    FormsModule
  ]
})
export class ReportsModule { }
