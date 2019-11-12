import { Component, OnInit, Input } from "@angular/core";
import { DeterminationNodeModel } from "src/app/models/determination-graph/determination-node.model";
import { ModalCloseEvent } from "../../shared/blade-modal/modal-close-event";
import { BladeModalService } from "src/app/services/shared/blade-modal.service";
import { DeterminationAnswerModel } from "src/app/models/determination-graph/determination-answer.model";
import { TouchSequence } from "selenium-webdriver";

@Component({
  selector: "graph-question-node",
  templateUrl: "./question-node.component.html",
  styleUrls: ["./question-node.component.sass"]
})
export class QuestionNodeComponent implements OnInit {
  @Input() model: DeterminationNodeModel;
  @Input() editing: boolean;

  modalId: string;

  constructor(private modalService: BladeModalService) {
    this.modalId = "blade-modal-" + Math.random() * 500;
  }

  ngOnInit() {}

  edit() {
    this.modalService.open(this.modalId);
  }

  onModalClose(event: ModalCloseEvent) {
    if (event.reason == "background") {
      event.preventDefault();
      return;
    }
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
}
