import { Component, OnInit, Input } from "@angular/core";
import { DeterminationNodeModel } from "../../models/determination-node.model";
import { BladeModalService } from "../../../shared/services/blade-modal.service";
import { DeterminationAnswerModel } from "../../models/determination-answer.model";
import { RoadworkSchemaModel } from "../../models/roadwork-schema.model";
import { RoadworkSchemasService } from "../../services/roadwork-schemas/roadwork-schemas.service";

@Component({
  selector: "determination-graph-node",
  templateUrl: "./graph-node.component.html",
  styleUrls: ["./graph-node.component.sass"]
})
export class GraphNodeComponent implements OnInit {
  @Input() model: DeterminationNodeModel;
  @Input() editing: boolean;
  @Input() roadworkSchemas: RoadworkSchemaModel[];

  modalId: string;

  constructor(
    private modalService: BladeModalService,
    private roadworkSchemaService: RoadworkSchemasService
  ) {
    this.modalId = "blade-modal-" + Math.random() * 500;
  }

  ngOnInit() {
    // this.roadworkSchemas = this.roadworkSchemaService.getAll();
    // this.roadworkSchemaForm = this.fb.group({
    //   roadworkSchemaInput: null
    // });
    // this.roadworkSchemaForm
    //   .get("roadworkSchemaInput")
    //   .valueChanges
    //   .pipe(
    //     debounceTime(300),
    //     tap(() => (this.isLoadingRoadworkSchemas = true)),
    //     switchMap(value =>
    //       this.roadworkSchemaService
    //         .searchRoadworkSchemas(value, "", 0, 0)
    //         .pipe(finalize(() => (this.isLoadingRoadworkSchemas = false)))
    //     )
    //   )
    //   .subscribe(result => (this.filteredRoadworkSchemas = result.schemas));
  }

  edit() {
    this.modalService.open(this.modalId);
  }

  modalAddAnswerClicked() {
    var newAnswer = new DeterminationAnswerModel();
    newAnswer.node = new DeterminationNodeModel();

    if (!this.model.answers) {
      this.model.answers = new Array<DeterminationAnswerModel>();
    }

    this.model.answers.push(newAnswer);
  }

  modalDeleteAnswerClicked(index: number) {
    this.model.answers.splice(index, 1);
  }

  displayRoadworkSchemaName(id: string) {
    if (!this.roadworkSchemas) {
      return "loading";
    }

    var result = this.roadworkSchemas.find(rws => {
      return rws.id === id;
    });

    if (result) return result.name;
    return "deleted";
  }
}
