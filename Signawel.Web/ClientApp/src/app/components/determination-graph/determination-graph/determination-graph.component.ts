import { Component, OnInit } from "@angular/core";
import { DeterminationGraphService } from "src/app/services/determination-graph/determination-graph.service";
import { DeterminationGraphModel } from "src/app/models/determination-graph/determination-graph.model";
import { DeterminationAnswerModel } from "src/app/models/determination-graph/determination-answer.model";
import { RoadworkSchemasService } from "src/app/services/roadwork-schemas/roadwork-schemas.service";
import { RoadworkSchemaModel } from "src/app/models/RoadworkSchema.model";

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
  roadworkSchemas: RoadworkSchemaModel[];

  constructor(
    private determinationGraphService: DeterminationGraphService,
    private roadworkSchemaService: RoadworkSchemasService
  ) {}

  ngOnInit() {
    this.loading = true;
    this.determinationGraphService.getGraph().subscribe(result => {
      this.savedDeterminationGraph = result;
      this.determinationGraph = JSON.parse(
        JSON.stringify(this.savedDeterminationGraph)
      ); // easy deep copy

      this.roadworkSchemaService
        .searchRoadworkSchemas("", "", 0, 0)
        .subscribe(result => {
          this.roadworkSchemas = result.schemas;
          this.loading = false;
        });
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
