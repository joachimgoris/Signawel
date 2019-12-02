import { Component, OnInit, Input } from "@angular/core";
import { ReportModel } from "../../models/report.model";
import { AngularMaterialImageOverlayService } from "angular-material-image-overlay";

@Component({
  selector: "report-detail",
  templateUrl: "./report-detail.component.html",
  styleUrls: ["./report-detail.component.sass"]
})
export class ReportDetailComponent implements OnInit {
  @Input() report: ReportModel;
  editMode: boolean = false;

  constructor(
    private imageOverlayService: AngularMaterialImageOverlayService
  ) {}

  ngOnInit() {}

  onZoomIn(imagePath: string) {
    let imagePaths = this.report.images.map(
      reportImage => reportImage.imagePath
    );
    this.imageOverlayService.open(imagePaths, imagePath);
  }
}
