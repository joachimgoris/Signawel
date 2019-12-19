import { Component, OnInit } from "@angular/core";
import { DefaultIssueModel } from "../../models/default-issue.model";
import { DefaultIssueService } from "../../services/default-issue/default-issue.service";
import { MatSnackBar } from "@angular/material";

@Component({
  selector: "settings-default-issues",
  templateUrl: "./default-issues.component.html",
  styleUrls: ["./default-issues.component.sass"]
})
export class DefaultIssuesComponent implements OnInit {
  newIssue: DefaultIssueModel;
  defaultIssues: DefaultIssueModel[];
  loading: boolean;

  constructor(
    private defaultIssueService: DefaultIssueService,
    private matSnackbar: MatSnackBar
  ) {
    this.newIssue = new DefaultIssueModel();
  }

  ngOnInit() {
    this.loading = true;
    this.defaultIssueService.getAllDefaultIssues().subscribe(response => {
      this.defaultIssues = response;
      this.loading = false;
    });
  }

  addDefaultIssue() {
    if (!this.newIssue || !this.newIssue.name || this.newIssue.name === "") {
      return;
    }

    this.defaultIssueService.addDefaultIssue(this.newIssue).subscribe(
      response => {
        this.defaultIssues.push(response);
        this.newIssue = new DefaultIssueModel();
      },
      error => {
        this.matSnackbar.open("Er ging iets mis bij het toevoegen.", null, {
          duration: 3000
        });
      }
    );
  }

  removeDefaultIssue(id: string) {
    this.defaultIssueService.removeDefaultIssue(id).subscribe(() => {
      this.defaultIssues = this.defaultIssues.filter(di => di.id != id);
    });
  }
}
