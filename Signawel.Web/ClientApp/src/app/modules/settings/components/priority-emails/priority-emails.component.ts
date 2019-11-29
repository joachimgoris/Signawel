import { Component, OnInit } from "@angular/core";
import { PriorityEmailsService } from "../../services/priority-emails/priority-emails.service";
import { PriorityEmailModel } from "../../models/priority-email.model";
import { MatSnackBar } from "@angular/material";
import { ApiError } from "src/app/modules/shared/models/api-error.model";

@Component({
  selector: "settings-priority-emails",
  templateUrl: "./priority-emails.component.html",
  styleUrls: ["./priority-emails.component.sass"]
})
export class PriorityEmailsComponent implements OnInit {
  newEmail: string;
  priorityEmails: PriorityEmailModel[];
  loading: boolean;

  constructor(
    private priorityService: PriorityEmailsService,
    private matSnackbar: MatSnackBar
  ) {}

  ngOnInit() {
    this.loading = true;
    this.priorityService.getAllPriorityEmails().subscribe(response => {
      this.priorityEmails = response;
      this.loading = false;
    });
  }

  addPriorityEmail() {
    if (!this.newEmail || this.newEmail === "") {
      return;
    }

    if (this.newEmail.includes("@")) {
      this.newEmail = this.newEmail.split("@")[1];
    }

    if (!this.newEmail.includes(".")) {
      this.matSnackbar.open(
        "Het ingegeven email (domain) is niet geldig.",
        null,
        { duration: 3000 }
      );
      return;
    }

    this.priorityService.addPriorityEmail(this.newEmail).subscribe(
      response => {
        this.priorityEmails.push(response);
        this.newEmail = "";
      },
      error => {
        if (
          error.error.some(
            element =>
              element.value === "EmailSuffix is already a priority email."
          )
        ) {
          this.matSnackbar.open(
            "Het ingegeven email (domain) is al een prioriteits email.",
            null,
            { duration: 3000 }
          );
        }
      }
    );
  }

  removePriorityEmail(id: string) {
    this.priorityService.removePriorityEmail(id).subscribe(() => {
      this.priorityEmails = this.priorityEmails.filter(pr => pr.id != id);
    });
  }
}
