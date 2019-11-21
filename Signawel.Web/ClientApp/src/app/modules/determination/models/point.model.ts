export class Point {
  id: string;
  x: number;
  y: number;
  order: number;

  constructor(x: number, y: number, order: number) {
    this.x = x;
    this.y = y;
    this.order = order;
  }
}
