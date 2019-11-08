import { Component, OnInit, Input } from "@angular/core";
import { DeterminationQuestionModel } from "src/app/models/determination-graph/determination-question.model.ts";

@Component({
  selector: "graph-question-node",
  templateUrl: "./question-node.component.html",
  styleUrls: ["./question-node.component.sass"]
})
export class QuestionNodeComponent implements OnInit {
  @Input() model: DeterminationQuestionModel;

  constructor() {}

  ngOnInit() {}
}
