import { DeterminationAnswerModel } from "./determination-answer.model";

export class DeterminationNodeModel {
  public type: string = "na";

  public question: string;
  public answers: Array<DeterminationAnswerModel>;
  public schemaId: string;
}
