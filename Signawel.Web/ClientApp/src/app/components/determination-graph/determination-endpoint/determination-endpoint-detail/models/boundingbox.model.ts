import { Point } from './point.model';

export class BoundingBox {
    private points: Array<Point> = new Array<Point>();

    public addPoint(point: Point) {
        this.points.push(point);
    }

    public removePoint(point: Point) {
        const index = this.points.indexOf(point);

        if(index != -1) {
            this.points.splice(index, 1);
        }
    }

    public getPoints() : Array<Point> {
        return this.points;
    }
}