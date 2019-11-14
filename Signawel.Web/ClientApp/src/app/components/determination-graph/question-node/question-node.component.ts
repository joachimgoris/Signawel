import { Component, OnInit, Input, AfterViewInit } from "@angular/core";
import { DeterminationNodeModel } from "src/app/models/determination-graph/determination-node.model";
import { ModalCloseEvent } from "../../shared/blade-modal/modal-close-event";
import { BladeModalService } from "src/app/services/shared/blade-modal.service";
import { DeterminationAnswerModel } from "src/app/models/determination-graph/determination-answer.model";
import { RoadworkSchemaModel } from "src/app/models/RoadworkSchema.model";
import { RoadworkSchemasService } from "src/app/services/roadwork-schemas/roadwork-schemas.service";

@Component({
  selector: "graph-question-node",
  templateUrl: "./question-node.component.html",
  styleUrls: ["./question-node.component.sass"]
})
export class QuestionNodeComponent implements OnInit {
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

  onModalClose(event: ModalCloseEvent) {}

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
