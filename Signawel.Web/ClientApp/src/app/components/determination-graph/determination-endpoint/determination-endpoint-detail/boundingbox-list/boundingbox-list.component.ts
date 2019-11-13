import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { BoundingBox } from '../models/boundingbox.model';

@Component({
  selector: 'app-boundingbox-list',
  templateUrl: './boundingbox-list.component.html',
  styleUrls: ['./boundingbox-list.component.sass']
})
export class BoundingboxListComponent implements OnInit {
  @Input() finishedBoundingBoxes: Array<BoundingBox>;
  @Output() onRemoved: EventEmitter<number> = new EventEmitter();
  
  constructor() { }

  ngOnInit() {
  }
  
  onRemove(boundingBoxId: number) {
    this.onRemoved.emit(boundingBoxId);
  }
}
