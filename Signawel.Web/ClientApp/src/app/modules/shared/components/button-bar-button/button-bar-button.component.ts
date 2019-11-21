import { Component, OnInit, Input, Renderer2 } from "@angular/core";

@Component({
  selector: "bbar-button",
  templateUrl: "./button-bar-button.component.html",
  styleUrls: ["./button-bar-button.component.sass"]
})
export class ButtonBarButtonComponent implements OnInit {
  @Input() text: string;
  @Input() color: string = "primary";

  constructor(private renderer: Renderer2) {}

  ngOnInit() {}
}
