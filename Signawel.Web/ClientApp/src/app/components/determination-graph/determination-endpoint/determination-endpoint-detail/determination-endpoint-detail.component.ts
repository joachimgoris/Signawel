import { Component, OnInit, Renderer2, ViewChild, ElementRef } from '@angular/core';
import { BoundingBox } from './models/boundingbox.model';
import { Point } from './models/point.model';

@Component({
  selector: 'app-determination-endpoint-detail',
  templateUrl: './determination-endpoint-detail.component.html',
  styleUrls: ['./determination-endpoint-detail.component.sass'],
  providers: []
})
export class DeterminationEndpointDetailComponent implements OnInit {
  @ViewChild("pointOutput", { static: true }) pointOutput: ElementRef;
  @ViewChild("linesOutput", { static: true }) linesOutput: ElementRef;
  @ViewChild("schema", { static: true }) schema: ElementRef;
  private currentBoundingBox: BoundingBox;
  private finishedBoundingBoxes: Array<BoundingBox> = new Array<BoundingBox>();
  private currentSelectedPoint: Point;
  private dragPointListener: () => void;

  constructor(private renderer: Renderer2) { }

  ngOnInit() {
  }

  clicked(event: MouseEvent) {
    var rectangle = (<HTMLImageElement>event.target).getBoundingClientRect();
    var xCoord = event.clientX - rectangle.left;
    var yCoord = event.clientY - rectangle.top;

    if (this.currentBoundingBox == null) {
      this.currentBoundingBox = new BoundingBox();
    }

    this.currentBoundingBox.addPoint(new Point(xCoord, yCoord));
    this.render();

    console.log(`new point -> x: ${xCoord}, y: ${yCoord}`);
  }

  render() {
    this.clear();

    this.finishedBoundingBoxes.forEach(boundingBox => this.renderBoundingBox(boundingBox));

    if (this.currentBoundingBox) {
      this.renderBoundingBox(this.currentBoundingBox, false)
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

    boundingbox.getPoints().forEach(point => {
      this.drawPoint(point);

      if (previousPoint) {
        this.drawLine(previousPoint, point);
      }

      previousPoint = point;
    });

    if (finished) {
      const firstPoint = boundingbox.getPoints()[0];
      this.drawNumber(firstPoint.xCoord, firstPoint.yCoord, this.finishedBoundingBoxes.indexOf(boundingbox));
      const lastPoint = boundingbox.getPoints()[boundingbox.getPoints().length - 1];
      this.drawLine(firstPoint, lastPoint);
    }
  }

  drawNumber(xCoord: number, yCoord: number, index: number) {
    const numberParagraph = this.renderer.createElement('p');
    this.renderer.addClass(numberParagraph, 'number');
    this.renderer.setProperty(numberParagraph, 'innerHTML', `Box ${index + 1}`)
    this.renderer.setStyle(numberParagraph, 'left', `${xCoord}px`);
    this.renderer.setStyle(numberParagraph, 'top', `${yCoord - 30}px`);

    this.renderer.appendChild(this.pointOutput.nativeElement, numberParagraph);
  }

  drawPoint(point: Point) {
    const pointDiv = this.renderer.createElement("div");
    this.renderer.addClass(pointDiv, 'point');
    this.renderer.setStyle(pointDiv, 'left', `${point.xCoord}px`);
    this.renderer.setStyle(pointDiv, 'top', `${point.yCoord}px`);

    this.addPointsListeners(pointDiv, point);

    this.renderer.appendChild(this.pointOutput.nativeElement, pointDiv);
  }

  addPointsListeners(pointDiv: ElementRef, point: Point) {
    this.renderer.listen(pointDiv, 'click', event => this.pointClick(event));
    this.renderer.listen(pointDiv, 'mousedown', event => this.pointMouseDown(event, point));
  }

  pointClick(event) {
    event.stopPropagation();
    if (this.currentBoundingBox) {
      var startPoint = this.currentBoundingBox.getPoints()[0];
      if (startPoint.xCoord == parseFloat(event.target.style.left) &&
        startPoint.yCoord == parseFloat(event.target.style.top) &&
        this.currentBoundingBox.getPoints().length > 1) {
        this.finishedBoundingBoxes.push(this.currentBoundingBox);
        this.currentBoundingBox = null;
        this.render();
        this.unregisterCurrentSelectedPoint(startPoint);
        console.log('bounding box finished');
      }
    }
  }

  pointMouseDown(event: MouseEvent, point: Point) {
    switch (event.which) {
      case 1:
        if(this.currentSelectedPoint) {
          this.unregisterCurrentSelectedPoint(point);
          console.log('unregistered selected point to start dragging');
        } else {
          this.registerCurrentSelectedPoint(point, event);
          console.log('registered selected point to start dragging');
        }
        break;
      case 3:
        this.removePoint(point);
        break;
    }
  }

  onMouseMove(event: MouseEvent, point: Point) {
    var rectangle = (<HTMLImageElement>event.target).getBoundingClientRect();
    point.xCoord = event.clientX - rectangle.left;
    point.yCoord = event.clientY - rectangle.top;
    this.render();
  }

  registerCurrentSelectedPoint(point: Point, event: MouseEvent) {
    this.currentSelectedPoint = point;
    this.dragPointListener = this.renderer.listen(this.linesOutput.nativeElement, 'mousemove', event => this.onMouseMove(event, point));
  }

  unregisterCurrentSelectedPoint(point: Point) {
    this.currentSelectedPoint = null;
    this.dragPointListener();
  }

  removePoint(point: Point) {
    if (this.currentBoundingBox != null) {
      this.currentBoundingBox.removePoint(point);
      if (this.currentBoundingBox.getPoints().length <= 0) {
        this.currentBoundingBox = null;
      }
    } else {
      this.finishedBoundingBoxes.forEach(boundingBox => {
        if (boundingBox.getPoints().includes(point)) {
          this.currentBoundingBox = boundingBox;
          this.finishedBoundingBoxes.splice(this.finishedBoundingBoxes.indexOf(boundingBox), 1);
          boundingBox.removePoint(point);
        }
      });
    }
    this.render();
  }

  drawLine(firstPoint: Point, secondPoint: Point) {
    let line = this.renderer.createElement('line', 'http://www.w3.org/2000/svg');

    this.renderer.setAttribute(line, 'stroke', 'red');
    this.renderer.setAttribute(line, 'style', 'width: 3px')
    this.renderer.setAttribute(line, 'x1', `${firstPoint.xCoord}`);
    this.renderer.setAttribute(line, 'y1', `${firstPoint.yCoord}`);
    this.renderer.setAttribute(line, 'x2', `${secondPoint.xCoord}`);
    this.renderer.setAttribute(line, 'y2', `${secondPoint.yCoord}`);

    this.renderer.appendChild(this.linesOutput.nativeElement, line);
  }

  onSave() {
    const boundingBoxesWithPointPercentages = this.convertBoundingBoxPointstoPercentages();
  }

  convertBoundingBoxPointstoPercentages() : Array<BoundingBox> {
    let finishedBoundingBoxes  = this.finishedBoundingBoxes.slice();

    finishedBoundingBoxes.forEach(boundingBox => {
      boundingBox.getPoints().slice().forEach(point => {
        point.xCoord = point.xCoord / this.schema.nativeElement.clientWidth;
        point.yCoord = point.yCoord / this.schema.nativeElement.clientHeight;
      });
    });

    return finishedBoundingBoxes;
  }

  removeBoundingBox(event) {
    this.finishedBoundingBoxes.splice(event, 1);
    this.render();
  }
}
