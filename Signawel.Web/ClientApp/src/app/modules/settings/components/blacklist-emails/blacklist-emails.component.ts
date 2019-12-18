import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { BlacklistEmailModel } from "../../models/blacklist-email.model";
import { BlacklistEmailsService } from "../../services/blacklist-emails/blacklist-emails.service";

@Component({
  selector: 'settings-blacklist-emails',
  templateUrl: './blacklist-emails.component.html',
  styleUrls: ['./blacklist-emails.component.sass']
})
export class BlacklistEmailsComponent implements OnInit {
  newEmail: string;
  blacklistEmails: BlacklistEmailModel[];
  loading: boolean;

  constructor(
    private blacklistService: BlacklistEmailsService,
    private matSnackbar: MatSnackBar
  ) { }

  ngOnInit() {
    this.loading = true;
    this.blacklistService.getAllBlacklistEmails().subscribe(response => {
      this.blacklistEmails = response;
      this.loading = false;
    });
  }

  addBlacklistEmail() {
    if (!this.newEmail || this.newEmail === "") {
      return;
    }

    if (!this.newEmail.includes("@") || !this.newEmail.includes(".")) {
      this.matSnackbar.open(
        "Het ingegeven e-mail is ongeldig.",
        null,
        { duration: 3000 }
      );
      return;
    }

    this.blacklistService.addBlacklistEmail(this.newEmail).subscribe(
      response => {
        this.blacklistEmails.push(response);
        this.newEmail = "";
      },
      error => {
        if (error.error.some(
          element => element.value === (this.newEmail + " is already a blacklisted email")
        )) {
          this.matSnackbar.open(
            "Het ingegeven e-mail is al blacklisted.",
            null,
            { duration: 3000 }
          );
        }
      }
    );
  }

  removeBlacklistedEmail(id: string) {
    this.blacklistService.removeBlacklistEmail(id).subscribe(() => {
      this.blacklistEmails = this.blacklistEmails.filter(pr => pr.id != id);
    });
  }

}
