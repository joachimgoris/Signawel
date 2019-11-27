import { BoundingBox } from "./boundingbox.model";

export class RoadworkSchemaModel {
  position: number;

  id: string;
  name: string;
  imageId: string;
  boundingBoxes: BoundingBox[];

  image: File;

  constructor() {
    this.boundingBoxes = new Array<BoundingBox>();
  }
}
