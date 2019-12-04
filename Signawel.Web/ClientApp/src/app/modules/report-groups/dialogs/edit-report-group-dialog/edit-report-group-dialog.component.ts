import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ReportGroupResponseModel } from '../../models/report-group-response-model';
import { CityResponseModel } from '../../models/city-response-model';
import { EmailResponseModel } from '../../models/email-response-model';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { ReportGroupCreationRequestModel } from '../../models/report-group-creation-request-model';
import { CityCreationRequestModel } from '../../models/city-creation-request-model';
import { EmailCreationRequestModel } from '../../models/email-creation-request-model';
@Component({
  selector: 'app-edit-report-group-dialog',
  templateUrl: './edit-report-group-dialog.component.html',
  styleUrls: ['./edit-report-group-dialog.component.sass']
})
export class EditReportGroupDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<EditReportGroupDialogComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  private reportGroup: ReportGroupResponseModel;
  private cities: Array<CityResponseModel>;
  form: FormGroup;

  ngOnInit() {
    this.reportGroup = this.data.reportGroup;
    this.cities = this.data.cities;

    this.form = this.formBuilder.group({
      cityControls: new FormArray([]),
      emailControls: new FormArray([])
    });

    this.addData();
  }

  addData() {
    this.reportGroup.cityReportGroups.forEach((o, i) => {
      const control = new FormControl(o.name);
      (this.form.controls.cityControls as FormArray).push(control);
    });

    this.reportGroup.emailReportGroups.forEach((o, i) => {
      const control = new FormControl(o.emailAddress);
      (this.form.controls.emailControls as FormArray).push(control);
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  addCity() {
    const control = new FormControl();
    (this.form.controls.cityControls as FormArray).push(control);
  }

  addEmail() {
    const control = new FormControl();
    (this.form.controls.emailControls as FormArray).push(control);
  }

  RemoveCityFromForm(index: any) {
    (this.form.controls.cityControls as FormArray).removeAt(index);
  }

  RemoveEmailFromForm(index: any) {
    (this.form.controls.emailControls as FormArray).removeAt(index);
  }

  saveData(){
    let updatedReportGroup = new ReportGroupCreationRequestModel();

    let cityLength = (this.form.controls.cityControls as FormArray).length;
    let emailLength = (this.form.controls.emailControls as FormArray).length;

    for(let i = 0;i<cityLength;i++){
      let formControl = (this.form.controls.cityControls as FormArray).get(i.toString());
      let city = new CityCreationRequestModel();
      city.name = formControl.value;
      updatedReportGroup.cityReportGroups.push(city);
    }
    for(let i = 0;i<emailLength;i++){
      let formControl = (this.form.controls.emailControls as FormArray).get(i.toString());
      let email = new EmailCreationRequestModel();
      email.emailAddress = formControl.value;
      updatedReportGroup.emailReportGroups.push(email);
    }
    
    this.dialogRef.close({model:updatedReportGroup, id:this.reportGroup.id});
  }
}
