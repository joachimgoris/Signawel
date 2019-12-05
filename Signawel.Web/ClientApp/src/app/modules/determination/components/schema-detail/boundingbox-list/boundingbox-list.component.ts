import { Component, OnInit, EventEmitter, Input, Output } from "@angular/core";
import { BoundingBox } from "../../../models/boundingbox.model";

@Component({
  selector: "determination-boundingbox-list",
  templateUrl: "./boundingbox-list.component.html",
  styleUrls: ["./boundingbox-list.component.sass"]
})
export class BoundingboxListComponent implements OnInit {
  @Input() finishedBoundingBoxes: Array<BoundingBox>;
  @Output() onRemoved: EventEmitter<number> = new EventEmitter();
  @Output() onNameChanged: EventEmitter<{ index: number, boundingBox: BoundingBox }> = new EventEmitter();

  constructor() {}

  ngOnInit() {}

  onRemove(boundingBoxId: number) {
    this.onRemoved.emit(boundingBoxId);
  }

  onNameChange(index: number, boundingbox: BoundingBox) {
    if(!boundingbox.name) {
      boundingbox.name = "";
    }
    this.onNameChanged.emit({ index: index, boundingBox: boundingbox});
  }

  onInputLeave(index: number, boundingbox: BoundingBox) {
    if(!boundingbox.name) {
      boundingbox.name = null;
    }
    this.onNameChanged.emit({ index: index, boundingBox: boundingbox});
  }
}
