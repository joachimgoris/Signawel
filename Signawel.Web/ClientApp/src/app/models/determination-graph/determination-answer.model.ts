import { DeterminationQuestionModel } from "./determination-question.model.ts";
import { DeterminationEndModel } from "./determination-end.model";

export class DeterminationAnswerModel {
  public answer: string;
  public next: DeterminationQuestionModel;
  public end: DeterminationEndModel;
}
