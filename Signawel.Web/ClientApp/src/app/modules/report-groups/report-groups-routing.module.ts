import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AddReportGroupComponent } from "./components/add-report-group/add-report-group.component";

const routes: Routes = [{ path: "", component: AddReportGroupComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportGroupsRoutingModule { }
