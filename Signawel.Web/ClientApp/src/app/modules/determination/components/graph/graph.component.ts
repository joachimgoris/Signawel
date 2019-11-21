import { Component, OnInit } from "@angular/core";
import { DeterminationGraphService } from "../../services/determination-graph/determination-graph.service";
import { RoadworkSchemasService } from "../../services/roadwork-schemas/roadwork-schemas.service";
import { DeterminationGraphModel } from "../../models/determination-graph.model";
import { DeterminationAnswerModel } from "../../models/determination-answer.model";
import { RoadworkSchemaModel } from "../../models/roadwork-schema.model";

@Component({
  selector: "determination-graph",
  templateUrl: "./graph.component.html",
  styleUrls: ["./graph.component.sass"]
})
export class GraphComponent implements OnInit {
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
