import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import {ReportGroupCreationRequestModel} from '../../models/report-group-creation-request-model';
import {CityCreationRequestModel} from '../../models/city-creation-request-model';
import {EmailCreationRequestModel} from '../../models/email-creation-request-model';
import{ReportGroupService} from "../../services/report-group.service";
import { CityResponseModel } from '../../models/city-response-model';
import { FormControl, FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map, delay } from 'rxjs/operators';
import { ReportGroupResponseModel } from '../../models/report-group-response-model';
import { MatSnackBar } from '@angular/material';
import { MatDialog } from '@angular/material';
import { ConfirmationDialogComponent } from '../../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { EditReportGroupDialogComponent } from '../../dialogs/edit-report-group-dialog/edit-report-group-dialog.component';


@Component({
  selector: 'app-add-report-group',
  templateUrl: './add-report-group.component.html',
  styleUrls: ['./add-report-group.component.sass']
})
export class AddReportGroupComponent implements OnInit {

  cityControl = new FormControl();
  emailControl = new FormControl();
  filteredOptions: Observable<string[]>;

  selectedSearch: string;
  searchText: string;

  public reportGroupCreationRequest: ReportGroupCreationRequestModel = new ReportGroupCreationRequestModel();
  public reportGroupResponses: Array<ReportGroupResponseModel>;

  public cities: Array<CityResponseModel>;
  public cityNames: Array<string>;
  public newCityNames: Array<string>;
  private isLoading: boolean;
  private isRequestLoading: boolean;

  form: FormGroup;

  constructor(private reportGroupService: ReportGroupService,
    private cd: ChangeDetectorRef,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    private formBuilder: FormBuilder) { 
      
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      cityControls: new FormArray([]),
      emailControls: new FormArray([])
    });

    this.addCity();
    this.addEmail();

    this.selectedSearch = "city";
    this.isLoading = true;

    this.reportGroupService.getCities().subscribe(result => {
      this.cities = result;
      this.cities.sort((a,b) => a.name.localeCompare(b.name));
      this.cityNames = this.cities.map(c=>c.name);
    },
    error=>{
      if(error.status == 0){
        this.snackBar.open("Error, kan server niet bereiken","",{
          duration: 2500
        });
      }
      if(error.status == 401){
        this.snackBar.open("Error, Gelieve opnieuw in te loggen","",{
          duration: 2500
        });
      }
    }).add(()=> {
      this.isLoading = false;
      this.filteredOptions = this.cityControl.valueChanges.pipe(
        startWith(''),
        delay(0),
        map(value => this._filter(value))
      );
    });
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.cityNames.filter(cn => cn.toLowerCase().indexOf(filterValue) === 0);
  }

  addCity(){
    const control = new FormControl([], [Validators.required]);
    (this.form.controls.cityControls as FormArray).push(control);
  }

  addEmail(){
    const control = new FormControl([], [Validators.required, Validators.email]);
    (this.form.controls.emailControls as FormArray).push(control);
  }

  RemoveCityFromForm(index: any){
    (this.form.controls.cityControls as FormArray).removeAt(index);
  }

  RemoveEmailFromForm(index: any){
    (this.form.controls.emailControls as FormArray).removeAt(index);
  }

  editReportGroup(index: any){
    const dialogRef = this.dialog.open(EditReportGroupDialogComponent, {
      width: '450px',
      data: {reportGroup: this.reportGroupResponses[index],cities: this.cities}
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.isRequestLoading = true;

        this.reportGroupService.modifyReportGroup(result.id,result.model).subscribe(data=>{
          this.reportGroupResponses[index] = data;
          this.snackBar.open("Melding groep is aangepast","",{
            duration: 2000
          });
        },
        errors=>{
          this.snackBar.open("Error, melding groep is niet aangepast","",{
            duration: 2000
          });
        })
        .add(()=>{
          this.isRequestLoading = false;
        });
    }})
      }

  deleteReportGroup(index: any){
    var id = this.reportGroupResponses[index].id;

    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      data: "Weet u zeker dat u deze meldingsgroep wilt verwijderen?"
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.reportGroupService.deleteReport(id).subscribe(data=>{
          this.snackBar.open("Meldingsgroep verwijderd","",{
            duration: 2000
          });
          this.reportGroupResponses.splice(index,1);
      },
      error=>{
        this.snackBar.open("Error, meldingsgroep niet verwijderd!","",{
          duration: 2500
        });
      })
      }
    });
  }

  GetReportGroups(){
    this.reportGroupResponses = new Array<ReportGroupResponseModel>();

    var cityOrNull = null;
    var mailOrNull = null;
    if(this.searchText){
    if(this.selectedSearch == "email"){
      mailOrNull = this.searchText;
    }
    if(this.selectedSearch == "city"){
      cityOrNull = this.searchText;
    }
  }
    this.isRequestLoading = true;
    this.reportGroupService.getReportGroups(cityOrNull,mailOrNull).subscribe(result => {
      this.reportGroupResponses = result;
    })
    .add(()=>{
      this.isRequestLoading = false;
      if(this.reportGroupResponses.length == 0){
        this.snackBar.open("Geen meldingsgroepen gevonden","",{
          duration: 2000
        });
      }
    });

  }

  checkForDuplicates(): boolean{
    let findDuplicates = arr => arr.filter((item, index) => arr.indexOf(item) != index)

    this.newCityNames = new Array<string>();
    let cityLength = (this.form.controls.cityControls as FormArray).length;

    for(let i = 0;i<cityLength;i++){
      let formControl = (this.form.controls.cityControls as FormArray).get(i.toString());
      this.newCityNames.push(formControl.value);
    }

    let duplicates = findDuplicates(this.newCityNames);
    return duplicates.length > 0;
  }

  checkIfCitiesExist(): boolean{
    var response = false;
    this.newCityNames.forEach(city => {
      if(!this.cityNames.includes(city)){
        response =  true;
      }
    });
    return response;
  }

  onSubmit(){
    if(this.checkForDuplicates()){
      this.snackBar.open("Er zijn één of meerdere steden dubbel toegevoegd","",{
        duration: 3000
      });
      return;
    }

    if(this.checkIfCitiesExist()){
      this.snackBar.open("Er zijn één of meerdere onbestaande steden toegevoegd","",{
        duration: 3000
      });
      return;
    }

    let reportGroupCreation = new ReportGroupCreationRequestModel();

    let cityLength = (this.form.controls.cityControls as FormArray).length;
    let emailLength = (this.form.controls.emailControls as FormArray).length;

    for(let i = 0;i<cityLength;i++){
      let formControl = (this.form.controls.cityControls as FormArray).get(i.toString());
      let city = new CityCreationRequestModel();
      city.name = formControl.value;
      reportGroupCreation.cityReportGroups.push(city);
    }
    for(let i = 0;i<emailLength;i++){
      let formControl = (this.form.controls.emailControls as FormArray).get(i.toString());
      let email = new EmailCreationRequestModel();
      email.emailAddress = formControl.value;
      reportGroupCreation.emailReportGroups.push(email);
    }

    this.isLoading = true;
    this.reportGroupService.setReportGroup(reportGroupCreation)
    .subscribe(response=>{
    this.snackBar.open("Niewe meldingsgroep is aangemaakt","",{
      duration: 2000
    });
    },
    error=>{
      if(error.status == 0){
        this.snackBar.open("Error, server onbereikbaar, meldingsgroep niet opgeslagen","",{
        duration: 2500
      });
    }
    if(error.status == 404){
      this.snackBar.open("Meldingsgroep niet opgeslagen, meldingsgroep bestaat al","",{
        duration: 2500
      });
    }
    })
    .add(()=>{
        this.isLoading = false;
        (this.form.controls.emailControls as FormArray).clear();
        (this.form.controls.cityControls as FormArray).clear();
        this.addCity();
        this.addEmail();
    });
  }

}

