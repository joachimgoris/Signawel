import {
  Component,
  OnInit,
  Renderer2,
  ViewChild,
  ElementRef,
  Input
} from "@angular/core";
import { ImageService } from "../../services/image/image.service";
import { BoundingBox } from "../../models/boundingbox.model";
import { RoadworkSchemaModel } from "../../models/roadwork-schema.model";
import { Point } from "../../models/point.model";
import { IMAGES } from "src/app/constants/api.constants";

@Component({
  selector: "determination-schema-detail",
  templateUrl: "./schema-detail.component.html",
  styleUrls: ["./schema-detail.component.sass"]
})
export class SchemaDetailComponent implements OnInit {
  @ViewChild("pointOutput", { static: true }) pointOutput: ElementRef;
  @ViewChild("linesOutput", { static: true }) linesOutput: ElementRef;
  @ViewChild("schema", { static: true }) schema: ElementRef;

  @Input() roadworkSchema: RoadworkSchemaModel;

  private currentBoundingBox: BoundingBox;
  private currentSelectedPoint: Point;
  private dragPointListener: () => void;

  constructor(
    private renderer: Renderer2,
    private imageService: ImageService
  ) {}

  ngOnInit() {}

  ngAfterViewInit(): void {
    this.render();
  }

  clicked(event: MouseEvent) {
    var rectangle = (<HTMLImageElement>event.target).getBoundingClientRect();
    var xCoord = event.clientX - rectangle.left;
    var yCoord = event.clientY - rectangle.top;

    if (this.currentBoundingBox == null) {
      this.currentBoundingBox = new BoundingBox();
    }

    let order =
      this.currentBoundingBox.points.length == 0
        ? 0
        : this.getPointsOrdened(this.currentBoundingBox)[
            this.currentBoundingBox.points.length - 1
          ].order + 1;

    this.currentBoundingBox.points.push(
      new Point(
        this.getPercentageXCord(xCoord),
        this.getPercentageYCord(yCoord),
        order
      )
    );
    this.render();

    console.log(`new point -> x: ${xCoord}, y: ${yCoord}, order: ${order}`);
  }

  render() {
    this.clear();

    this.roadworkSchema.boundingBoxes.forEach(boundingBox =>
      this.renderBoundingBox(boundingBox)
    );

    if (this.currentBoundingBox) {
      this.renderBoundingBox(this.currentBoundingBox, false);
    }
  }

  clear() {
    while (this.pointOutput.nativeElement.firstChild) {
      this.pointOutput.nativeElement.removeChild(
        this.pointOutput.nativeElement.firstChild
      );

      while (this.linesOutput.nativeElement.firstChild) {
        this.linesOutput.nativeElement.removeChild(
          this.linesOutput.nativeElement.firstChild
        );
      }
    }
  }

  renderBoundingBox(boundingbox: BoundingBox, finished: Boolean = true) {
    let previousPoint: Point = null;

    this.getPointsOrdened(boundingbox).forEach(point => {
      this.drawPoint(point);

      if (previousPoint) {
        this.drawLine(previousPoint, point);
      }

      previousPoint = point;
    });

    if (finished) {
      const firstPoint = this.getPointsOrdened(boundingbox)[0];
      this.drawNumber(
        this.getXCord(firstPoint.x),
        this.getYCord(firstPoint.y),
        this.roadworkSchema.boundingBoxes.indexOf(boundingbox)
      );
      const lastPoint = this.getPointsOrdened(boundingbox)[
        boundingbox.points.length - 1
      ];
      this.drawLine(firstPoint, lastPoint);
    }
  }

  drawNumber(xCoord: number, yCoord: number, index: number) {
    const numberParagraph = this.renderer.createElement("p");
    this.renderer.addClass(numberParagraph, "number");
    this.renderer.setProperty(numberParagraph, "innerHTML", `Box ${index + 1}`);
    this.renderer.setStyle(numberParagraph, "left", `${xCoord}px`);
    this.renderer.setStyle(numberParagraph, "top", `${yCoord - 30}px`);

    this.renderer.appendChild(this.pointOutput.nativeElement, numberParagraph);
  }

  drawPoint(point: Point) {
    const pointDiv = this.renderer.createElement("div");
    this.renderer.addClass(pointDiv, "point");
    this.renderer.setStyle(pointDiv, "left", `${this.getXCord(point.x)}px`);
    this.renderer.setStyle(pointDiv, "top", `${this.getYCord(point.y)}px`);

    this.addPointsListeners(pointDiv, point);

    this.renderer.appendChild(this.pointOutput.nativeElement, pointDiv);
  }

  addPointsListeners(pointDiv: ElementRef, point: Point) {
    this.renderer.listen(pointDiv, "click", event => this.pointClick(event));
    this.renderer.listen(pointDiv, "mousedown", event =>
      this.pointMouseDown(event, point)
    );
  }

  pointClick(event) {
    event.stopPropagation();

    if (this.currentBoundingBox) {
      var startPoint = this.getPointsOrdened(this.currentBoundingBox)[0];

      if (
        this.getXCord(startPoint.x).toFixed(3) ==
          parseFloat(event.target.style.left).toFixed(3) &&
        this.getYCord(startPoint.y).toFixed(3) ==
          parseFloat(event.target.style.top).toFixed(3) &&
        this.currentBoundingBox.points.length > 2
      ) {
        this.roadworkSchema.boundingBoxes.push(this.currentBoundingBox);
        this.currentBoundingBox = null;
        this.render();
        this.unregisterCurrentSelectedPoint(startPoint);
        console.log("bounding box finished");
      }
    }
  }

  pointMouseDown(event: MouseEvent, point: Point) {
    switch (event.which) {
      case 1:
        if (this.currentSelectedPoint) {
          this.unregisterCurrentSelectedPoint(point);
          console.log("unregistered selected point to start dragging");
        } else {
          this.registerCurrentSelectedPoint(point, event);
          console.log("registered selected point to start dragging");
        }
        break;
      case 3:
        this.removePoint(point);
        break;
    }
  }

  onMouseMove(event: MouseEvent, point: Point) {
    var rectangle = (<HTMLImageElement>event.target).getBoundingClientRect();
    point.x = this.getPercentageXCord(event.clientX - rectangle.left);
    point.y = this.getPercentageYCord(event.clientY - rectangle.top);
    this.render();
  }

  registerCurrentSelectedPoint(point: Point, event: MouseEvent) {
    this.currentSelectedPoint = point;
    this.dragPointListener = this.renderer.listen(
      this.linesOutput.nativeElement,
      "mousemove",
      event => this.onMouseMove(event, point)
    );
  }

  unregisterCurrentSelectedPoint(point: Point) {
    this.currentSelectedPoint = null;
    this.dragPointListener();
  }

  removePoint(point: Point) {
    if (this.currentBoundingBox != null) {
      let index = this.getPointsOrdened(this.currentBoundingBox).indexOf(point);
      if (index != -1) {
        this.getPointsOrdened(this.currentBoundingBox).splice(index, 1);
      }

      if (this.currentBoundingBox.points.length <= 0) {
        this.currentBoundingBox = null;
      }
    } else {
      this.roadworkSchema.boundingBoxes.forEach(boundingBox => {
        if (boundingBox.points.includes(point)) {
          this.currentBoundingBox = boundingBox;
          this.roadworkSchema.boundingBoxes.splice(
            this.roadworkSchema.boundingBoxes.indexOf(boundingBox),
            1
          );

          let index = boundingBox.points.indexOf(point);
          if (index != -1) {
            boundingBox.points.splice(index, 1);
          }
        }
      });
    }
    this.render();
  }

  drawLine(firstPoint: Point, secondPoint: Point) {
    let line = this.renderer.createElement(
      "line",
      "http://www.w3.org/2000/svg"
    );

    this.renderer.setAttribute(line, "stroke", "red");
    this.renderer.setAttribute(line, "style", "width: 3px");
    this.renderer.setAttribute(line, "x1", `${this.getXCord(firstPoint.x)}`);
    this.renderer.setAttribute(line, "y1", `${this.getYCord(firstPoint.y)}`);
    this.renderer.setAttribute(line, "x2", `${this.getXCord(secondPoint.x)}`);
    this.renderer.setAttribute(line, "y2", `${this.getYCord(secondPoint.y)}`);

    this.renderer.appendChild(this.linesOutput.nativeElement, line);
  }

  removeBoundingBox(event) {
    this.roadworkSchema.boundingBoxes.splice(event, 1);
    this.render();
  }

  getPointsOrdened(bbox: BoundingBox): Point[] {
    return bbox.points.sort((a, b) => a.order - b.order);
  }

  imageChanged(event) {
    var file = event.target.files[0] as File;

    this.imageService.postImage(file).subscribe(res => {
      this.roadworkSchema.imageId = res.id;
      this.render();
    });
  }

  getImageUrl(id: string) {
    return IMAGES + `/${id}`;
  }

  getXCord(percentage: number) {
    return percentage * this.schema.nativeElement.clientWidth;
  }

  getYCord(percentage: number) {
    return percentage * this.schema.nativeElement.clientHeight;
  }

  getPercentageXCord(x: number) {
    return x / this.schema.nativeElement.clientWidth;
  }

  getPercentageYCord(y: number) {
    return y / this.schema.nativeElement.clientHeight;
  }
}
