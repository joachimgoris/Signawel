import {
  Component,
  OnInit,
  Input,
  ViewChild,
  ElementRef,
  Renderer2
} from "@angular/core";

@Component({
  selector: "bbar-button",
  templateUrl: "./button-bar-button.component.html",
  styleUrls: ["./button-bar-button.component.sass"]
})
export class ButtonBarButtonComponent implements OnInit {
  @ViewChild("btn", { static: true }) btnElement: ElementRef;
  @Input() text: string;

  private _color: string;
  private _colorValue: string;

  @Input()
  set color(value: string) {
    this._color = value;

    this.renderer.addClass(this.btnElement.nativeElement, this._color);
  }

  get color(): string {
    return this._color;
  }

  @Input()
  set colorValue(value: string) {
    this._colorValue = value;

    this.renderer.setStyle(
      this.btnElement.nativeElement,
      "background-color",
      this._colorValue
    );
  }

  get colorValue(): string {
    return this._colorValue;
  }

  constructor(private renderer: Renderer2) {}

  ngOnInit() {}
}
