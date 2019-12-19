import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ReportGroupResponseModel } from '../../models/report-group-response-model';
import { CityResponseModel } from '../../models/city-response-model';
import { EmailResponseModel } from '../../models/email-response-model';
import { FormGroup, FormBuilder, FormArray, FormControl, Validators } from '@angular/forms';
import { ReportGroupCreationRequestModel } from '../../models/report-group-creation-request-model';
import { CityCreationRequestModel } from '../../models/city-creation-request-model';
import { EmailCreationRequestModel } from '../../models/email-creation-request-model';
import { UserResponseModel } from '../../models/user-response-model';
import { UserCreationRequestModel } from '../../models/user-creation-request-model';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { ReportGroupService } from '../../services/report-group.service';
@Component({
  selector: 'app-edit-report-group-dialog',
  templateUrl: './edit-report-group-dialog.component.html',
  styleUrls: ['./edit-report-group-dialog.component.sass']
})
export class EditReportGroupDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<EditReportGroupDialogComponent>,
    private formBuilder: FormBuilder,
    private reportGroupService: ReportGroupService,
    private snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  private reportGroup: ReportGroupResponseModel;
  private cities: Array<CityResponseModel>;
  private userIds: Array<string> = new Array<string>();

  private filteredUserOptions: Array<string[]> = new Array<string[]>();
  private isUserLoading: boolean;
  form: FormGroup;

  ngOnInit() {
    this.reportGroup = this.data.reportGroup;
    this.cities = this.data.cities;

    this.form = this.formBuilder.group({
      cityControls: new FormArray([]),
      emailControls: new FormArray([]),
      userControls: new FormArray([])
    });

    this.addData();
  }

  addData() {
    this.reportGroup.cityReportGroups.forEach((o, i) => {
      const control = new FormControl(o.name,[Validators.required]);
      (this.form.controls.cityControls as FormArray).push(control);
    });

    this.reportGroup.emailReportGroups.forEach((o, i) => {
      const control = new FormControl(o.emailAddress, [Validators.required, Validators.email]);
      (this.form.controls.emailControls as FormArray).push(control);
    });

    this.reportGroup.userReportGroups.forEach((o) => {
      this.addUser(o);
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  addCity() {
    const control = new FormControl([], [Validators.required]);
    (this.form.controls.cityControls as FormArray).push(control);
  }

  addEmail() {
    const control = new FormControl([], [Validators.required, Validators.email]);
    (this.form.controls.emailControls as FormArray).push(control);
  }

  addUser(model: UserResponseModel = null) {
    let control;
    let newIndex = (this.form.controls.userControls as FormArray).length;

    if(model == null){
    control = new FormControl([], [Validators.required]);
    }
    if(model != null){
    control = new FormControl(model.userName, [Validators.required]);
    this.userIds.push(model.id);
    this.filteredUserOptions.push([model.userName]);
    }

    (this.form.controls.userControls as FormArray).push(control);

    control.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(value => {
        if(value == ""){
          if(this.filteredUserOptions[newIndex]) {
            this.filteredUserOptions[newIndex] = [];
          } else {
            this.filteredUserOptions.push([]);
          }
          return;
        }
        this.isUserLoading = true;
        this.reportGroupService.getUsers(((value as string).toLowerCase())).subscribe(response => {
          let sortedResponse = response.sort((a, b) => a.userName.localeCompare(b.userName));
          let options = sortedResponse.map(u => u.userName);

          if(options.length == 1){
            if(this.userIds[newIndex]) {
              this.userIds[newIndex] = response[0].id;
            } else {
              this.userIds.push(response[0].id);
            }
          }
          
          if(this.filteredUserOptions[newIndex]) {
            this.filteredUserOptions[newIndex] = options;
          } else {
            this.filteredUserOptions.push(options);
          }
        },
          error => {
            this.snackBar.open("Error, something went wrong", "", {
              duration: 2500
            });
          }).add(() => {
            this.isUserLoading = false;
          });
      })
    ).subscribe();
  }

  RemoveCityFromForm(index: any) {
    (this.form.controls.cityControls as FormArray).removeAt(index);
  }

  RemoveEmailFromForm(index: any) {
    (this.form.controls.emailControls as FormArray).removeAt(index);
  }

  RemoveUserFromForm(index: any) {
    (this.form.controls.userControls as FormArray).removeAt(index);
    this.userIds.splice(index,1);
  }

  findDuplicates(arr: any){
    return arr.filter((item, index) => arr.indexOf(item) != index);
  }

  checkForDuplicateUsers():boolean{
    let userNames = new Array<string>();
    let userLength = (this.form.controls.userControls as FormArray).length;

    for (let i = 0; i < userLength; i++) {
      let formControl = (this.form.controls.userControls as FormArray).get(i.toString());
      userNames.push(formControl.value);
    }

    return this.findDuplicates(userNames).length > 0;
  }

  saveData(){
    let cityLength = (this.form.controls.cityControls as FormArray).length;
    let emailLength = (this.form.controls.emailControls as FormArray).length;
    let userLength = (this.form.controls.userControls as FormArray).length;

    if (this.checkForDuplicateUsers()) {
      this.snackBar.open("Er zijn één of meerdere users dubbel toegevoegd", "", {
        duration: 3000
      });
      return;
    }

    if(this.userIds.length != userLength){
      this.snackBar.open("Er zijn één of meerdere onbestaande users toegevoegd", "", {
        duration: 3000
      });
      return;
    }

    let updatedReportGroup = new ReportGroupCreationRequestModel();

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
    for(let i = 0;i<userLength;i++){
      let user = new UserCreationRequestModel();
      user.id = this.userIds[i];
      updatedReportGroup.userReportGroups.push(user);
    }
    
    this.dialogRef.close({model:updatedReportGroup, id:this.reportGroup.id});
  }
}
