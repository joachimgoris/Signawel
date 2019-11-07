import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "bbar-button",
  templateUrl: "./button-bar-button.component.html",
  styleUrls: ["./button-bar-button.component.sass"]
})
export class ButtonBarButtonComponent implements OnInit {
  @Input() text: string;

  constructor() {}

  ngOnInit() {}
}
