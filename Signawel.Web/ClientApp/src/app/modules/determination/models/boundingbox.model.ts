import { Point } from "./point.model";

export class BoundingBox {
  public id: string;
  public name: string;
  public points: Array<Point> = new Array<Point>();
}
