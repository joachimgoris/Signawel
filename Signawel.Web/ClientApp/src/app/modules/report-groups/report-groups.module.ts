import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddReportGroupComponent } from './components/add-report-group/add-report-group.component';
import { SignawelMaterialModule } from "../signawel-material/signawel-material.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ReportGroupsRoutingModule } from './report-groups-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatGridListModule } from '@angular/material';
import { ConfirmationDialogComponent } from '../shared/components/confirmation-dialog/confirmation-dialog.component';

@NgModule({
  declarations: [AddReportGroupComponent],
  imports: [
    CommonModule,
    ReportGroupsRoutingModule,
    SignawelMaterialModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatGridListModule
  ],
  entryComponents:[
    ConfirmationDialogComponent
  ]
})
export class ReportGroupsModule { }
