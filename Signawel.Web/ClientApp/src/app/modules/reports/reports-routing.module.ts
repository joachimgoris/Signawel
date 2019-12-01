import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReportsOverviewComponent } from './components/reports-overview/reports-overview.component';

const routes: Routes = [{ path: '', component: ReportsOverviewComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportsRoutingModule { }
