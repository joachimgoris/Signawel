import { Point } from "./point.model";

export class BoundingBox {
  public id: string;
  public points: Array<Point> = new Array<Point>();
}
