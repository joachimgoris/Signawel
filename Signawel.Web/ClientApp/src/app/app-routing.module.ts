import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { DeterminationGraphComponent } from "./components/determination-graph/determination-graph/determination-graph.component";
import { DeterminationEndpointDetailComponent } from "./components/determination-graph/determination-endpoint/determination-endpoint-detail/determination-endpoint-detail.component";
import { LayoutComponent } from "./components/layout/layout.component";
import { HomeComponent } from "./components/home/home.component";
import { DeterminationEndpointListComponent } from "./components/determination-graph/determination-endpoint/determination-endpoint-list/determination-endpoint-list.component";

const routes: Routes = [
  {
    path: "",
    component: LayoutComponent,
    children: [
      {
        path: "",
        pathMatch: "full",
        component: HomeComponent
      },
      {
        path: 'determination-graph/determination-endpoint/determination-endpoint-detail',
        pathMatch: 'full',
        component: DeterminationEndpointDetailComponent
      },
      {
        path: "determination-graph",
        component: DeterminationGraphComponent
      },
      {
        path: "determination-endpoints",
        component: DeterminationEndpointListComponent
      },
      {
        path: "determination-endpoints/:id",
        component: DeterminationEndpointDetailComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
