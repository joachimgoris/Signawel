import { DeterminationAnswerModel } from "./determination-answer.model";

export class DeterminationQuestionModel {
  public question: string;
  public answers: Array<DeterminationAnswerModel>;
}
