import { Component, OnInit } from "@angular/core";
import { BladeModalService } from "src/app/services/shared/blade-modal.service";
import { ModalCloseEvent } from "../shared/blade-modal/modal-close-event";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.sass"]
})
export class HomeComponent implements OnInit {
  constructor(private modalService: BladeModalService) {}

  ngOnInit() {}

  openModal(id: string) {
    this.modalService.open(id);
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  onClose(event: ModalCloseEvent) {
    if (event.reason == "background") {
      event.preventDefault();
      console.log("prevented background");
    } else {
      console.log(`close with reason: ${event.reason}`);
    }
  }
}
