import { BoundingBox } from "../components/determination-graph/determination-endpoint/determination-endpoint-detail/models/boundingbox.model";

export class RoadworkSchemaModel {
  position: number;
  id: string;
  name: string;
  imageId: string;

  boundingBoxes: BoundingBox[];

  constructor() {
    this.boundingBoxes = new Array<BoundingBox>();
  }
}
