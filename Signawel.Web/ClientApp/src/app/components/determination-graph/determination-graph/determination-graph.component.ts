import { Component, OnInit } from "@angular/core";
import { DeterminationGraphService } from "src/app/services/determination-graph/determination-graph.service";
import { DeterminationGraphModel } from "src/app/models/determination-graph/determination-graph.model";

@Component({
  selector: "app-determination-graph",
  templateUrl: "./determination-graph.component.html",
  styleUrls: ["./determination-graph.component.sass"]
})
export class DeterminationGraphComponent implements OnInit {
  private determinationGraph: DeterminationGraphModel;

  constructor(private determinationGraphService: DeterminationGraphService) {}

  ngOnInit() {
    this.determinationGraphService
      .getSchema()
      .subscribe(result => (this.determinationGraph = result));
  }

  editClicked() {
    console.log("edit clicked");
  }
}
