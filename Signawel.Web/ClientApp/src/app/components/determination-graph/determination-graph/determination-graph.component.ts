import { Component, OnInit } from "@angular/core";
import { DeterminationGraphService } from "src/app/services/determination-graph/determination-graph.service";
import { DeterminationGraphModel } from "src/app/models/determination-graph/determination-graph.model";
import { DeterminationAnswerModel } from "src/app/models/determination-graph/determination-answer.model";

@Component({
  selector: "app-determination-graph",
  templateUrl: "./determination-graph.component.html",
  styleUrls: ["./determination-graph.component.sass"]
})
export class DeterminationGraphComponent implements OnInit {
  private determinationGraph: DeterminationGraphModel;
  private savedDeterminationGraph: DeterminationGraphModel;

  public editing: boolean = false;
  public loading: boolean = true;

  constructor(private determinationGraphService: DeterminationGraphService) {}

  ngOnInit() {
    this.loading = true;
    this.determinationGraphService.getGraph().subscribe(result => {
      this.savedDeterminationGraph = result;
      this.determinationGraph = JSON.parse(
        JSON.stringify(this.savedDeterminationGraph)
      ); // easy deep copy

      this.loading = false;
    });
  }

  onEdit() {
    this.editing = true;
  }

  onSave() {
    this.editing = false;
    this.loading = true;
    this.determinationGraphService
      .setGraph(this.determinationGraph)
      .subscribe(result => {
        this.savedDeterminationGraph = result;
        this.determinationGraph = JSON.parse(
          JSON.stringify(this.savedDeterminationGraph)
        ); // easy deep copy

        this.loading = false;
      });
  }

  onCancel() {
    this.editing = false;
    this.determinationGraph = this.savedDeterminationGraph;
  }
}
