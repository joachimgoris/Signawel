import {
  Component,
  OnInit,
  Input,
  ElementRef,
  OnDestroy,
  Output,
  EventEmitter
} from "@angular/core";
import { ModalCloseEvent } from "./modal-close-event";
import { BladeModalService } from "../../services/blade-modal.service";

@Component({
  selector: "blade-modal",
  templateUrl: "./blade-modal.component.html",
  styleUrls: ["./blade-modal.component.sass"]
})
export class BladeModalComponent implements OnInit, OnDestroy {
  @Input() id: string;
  @Input() modalTitle: string;
  @Input() closeButton: boolean = true;
  @Output() modalClose: EventEmitter<ModalCloseEvent> = new EventEmitter();

  private element: any;

  constructor(private modalService: BladeModalService, private el: ElementRef) {
    this.element = el.nativeElement;
  }

  ngOnInit() {
    if (!this.id) {
      console.error("modal must have an id");
      return;
    }

    document.body.appendChild(this.element);
    this.element.addEventListener("click", e => {
      if (e.target.className === "blade-modal-background") {
        this.close("background");
      }
    });

    this.modalService.add(this);
  }

  ngOnDestroy(): void {
    this.modalService.remove(this.id);
    this.element.remove();
  }

  open(): void {
    this.element.style.display = "block";
    document.body.classList.add("blade-modal-open");
  }

  close(reason: string): boolean {
    let modalCloseEvent = new ModalCloseEvent(reason);

    this.modalClose.emit(modalCloseEvent);

    if (modalCloseEvent.isDefaultPrevented()) {
      return false;
    }

    this.element.style.display = "none";
    document.body.classList.remove("blade-modal-open");
    return true;
  }
}
