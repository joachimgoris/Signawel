import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { GraphComponent } from "./components/graph/graph.component";
import { SchemaListComponent } from "./components/schema-list/schema-list.component";

const routes: Routes = [
  {
    path: "graph",
    component: GraphComponent
  },
  {
    path: "schemas",
    component: SchemaListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DeterminationRoutingModule {}
