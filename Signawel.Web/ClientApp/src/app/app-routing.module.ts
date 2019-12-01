import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LayoutComponent } from "./components/layout/layout.component";
import { HomeComponent } from "./components/home/home.component";
import { AuthenticationGuard } from "./modules/authentication/guards/auth.guard";

const routes: Routes = [
  {
    path: "",
    component: LayoutComponent,
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: "",
        pathMatch: "full",
        component: HomeComponent
      },
      {
        path: "determination",
        loadChildren: () =>
          import("./modules/determination/determination.module").then(
            m => m.DeterminationModule
          )
      },
      {
        path: "report-groups",
        loadChildren: () =>
          import("./modules/report-groups/report-groups.module").then(
            m => m.ReportGroupsModule
          )
      },
      {
        path: "reports",
        loadChildren: () =>
          import("./modules/reports/reports.module").then(
            m => m.ReportsModule
          )
      },
      {
        path: "settings",
        loadChildren: () =>
          import("./modules/settings/settings.module").then(
            m => m.SettingsModule
          )
      }
    ]
  },
  {
    path: "authentication",
    loadChildren: () =>
      import("./modules/authentication/authentication.module").then(
        m => m.AuthenticationModule
      )
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
