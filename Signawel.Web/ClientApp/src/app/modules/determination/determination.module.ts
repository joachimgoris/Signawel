import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

import { DeterminationRoutingModule } from "./determination-routing.module";
import { GraphComponent } from "./components/graph/graph.component";
import { GraphNodeComponent } from "./components/graph-node/graph-node.component";
import { SchemaListComponent } from "./components/schema-list/schema-list.component";
import { SchemaDetailComponent } from "./components/schema-detail/schema-detail.component";
import { BoundingboxListComponent } from "./components/schema-detail/boundingbox-list/boundingbox-list.component";
import { SharedModule } from "../shared/shared.module";
import { SignawelMaterialModule } from "../signawel-material/signawel-material.module";

@NgModule({
  declarations: [
    GraphComponent,
    GraphNodeComponent,
    SchemaListComponent,
    SchemaDetailComponent,
    BoundingboxListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    DeterminationRoutingModule,
    SharedModule,
    SignawelMaterialModule
  ]
})
export class DeterminationModule {}
