import { Component, OnInit, Input } from "@angular/core";
import { ReportModel } from "../../models/report.model";
import { AngularMaterialImageOverlayService } from "angular-material-image-overlay";
import { IMAGES } from 'src/app/constants/api.constants';

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

  getImageUrl(imageId: string) {

    let reportImage = this.report.images.find(image=> image.imageId === imageId);

    if(reportImage) {
      return IMAGES + '/' + reportImage.imageId;
    }

    return "";
  }

  onZoomIn(imagePath: string) {
    let imagePaths = this.report.images.map(
      reportImage => this.getImageUrl(reportImage.imageId)
    );
    this.imageOverlayService.open(imagePaths, imagePath);
  }
}
