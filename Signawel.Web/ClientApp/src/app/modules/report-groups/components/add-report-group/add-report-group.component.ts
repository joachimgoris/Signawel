import { Component, OnInit, ChangeDetectorRef, ViewChild } from '@angular/core';
import {ReportGroupCreationRequestModel} from '../../models/report-group-creation-request-model';
import {CityCreationRequestModel} from '../../models/city-creation-request-model';
import {EmailCreationRequestModel} from '../../models/email-creation-request-model';
import{ReportGroupService} from "../../services/report-group.service";
import { CityResponseModel } from '../../models/city-response-model';
import { FormControl, Form } from '@angular/forms';
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

  @ViewChild('addReportGroup',{ static: false }) form;

  public reportGroupCreationRequest: ReportGroupCreationRequestModel = new ReportGroupCreationRequestModel();
  public reportGroupResponses: Array<ReportGroupResponseModel>;

  public cities: Array<CityResponseModel>;
  public cityNames: Array<string>;
  private isLoading: boolean;
  private isRequestLoading: boolean;

  constructor(private reportGroupService: ReportGroupService,
    private cd: ChangeDetectorRef,
    private snackBar: MatSnackBar,
    private dialog: MatDialog) { 
  }

  ngOnInit() {
    this.selectedSearch = "city";
    this.isLoading = true;

    this.reportGroupService.getCities().subscribe(result => {
      this.cities = result;
      this.cities.sort((a,b) => a.name.localeCompare(b.name));
      this.cityNames = this.cities.map(c=>c.name);
    }).add(()=> {
      if(this.cities == undefined){
        this.snackBar.open("Kan server niet bereiken","",{
          duration: 2000
        });
      }
      this.isLoading = false;
      this.filteredOptions = this.cityControl.valueChanges.pipe(
        startWith(''),
        delay(0),
        map(value => this._filter(value))
      );
    });

    this.reportGroupCreationRequest.cityReportGroups.push(new CityCreationRequestModel());
    this.reportGroupCreationRequest.emailReportGroups.push(new EmailCreationRequestModel());
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.cityNames.filter(cn => cn.toLowerCase().indexOf(filterValue) === 0);
  }

  addCity(){
    this.reportGroupCreationRequest.cityReportGroups[this.reportGroupCreationRequest.cityReportGroups.length - 1].name = this.cityControl.value;
    this.reportGroupCreationRequest.cityReportGroups.push(new CityCreationRequestModel());
    this.cityControl.setValue("", {emitModelToViewChange: false});
  }

  addEmail(){
    this.reportGroupCreationRequest.emailReportGroups[this.reportGroupCreationRequest.emailReportGroups.length - 1].emailAddress = this.emailControl.value;
    this.reportGroupCreationRequest.emailReportGroups.push(new EmailCreationRequestModel());
    this.emailControl.setValue("", {emitModelToViewChange: false});
  }

  RemoveCityFromForm(index: any){
    this.reportGroupCreationRequest.cityReportGroups.splice(index,1);
  }

  RemoveEmailFromForm(index: any){
    this.reportGroupCreationRequest.emailReportGroups.splice(index,1);
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
      data: "Weet u zeker dat u deze report groep wilt verwijderen?"
    });
    dialogRef.afterClosed().subscribe(result => {
      if(result) {
        this.reportGroupService.deleteReport(id).subscribe(data=>{
          this.snackBar.open("Report groep is verwijdert","",{
            duration: 2000
          });
          this.reportGroupResponses.splice(index,1);
      },
      error=>{
        this.snackBar.open("Error , report groep is niet verwijdert!","",{
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
        this.snackBar.open("Geen report groepen gevonden","",{
          duration: 2000
        });
      }
    });

  }

  onSubmit(){
    this.reportGroupCreationRequest.emailReportGroups[this.reportGroupCreationRequest.emailReportGroups.length - 1].emailAddress = this.emailControl.value;
    this.reportGroupCreationRequest.cityReportGroups[this.reportGroupCreationRequest.cityReportGroups.length - 1].name = this.cityControl.value;

    this.isLoading = true;
    this.reportGroupService.setReportGroup(this.reportGroupCreationRequest)
    .subscribe(response=>{
      this.reportGroupCreationRequest = new ReportGroupCreationRequestModel();
      this.reportGroupCreationRequest.cityReportGroups.push(new CityCreationRequestModel());
    this.reportGroupCreationRequest.emailReportGroups.push(new EmailCreationRequestModel());
    this.snackBar.open("Niew report groep is aangemaakt","",{
      duration: 2000
    });
    },
    error=>{
      if(error.status == 0){
      this.snackBar.open("Error, kan server niet bereiken, report groep is niet opgeslagen","",{
        duration: 2500
      });
    }
    if(error.status == 404){
      this.snackBar.open("report groep niet opgeslagen, de report groep bestaat al","",{
        duration: 2500
      });
    }
    })
    .add(()=>{
        this.isLoading = false;
        this.cityControl.setValue("", {emitModelToViewChange: false});
      this.emailControl.setValue("", {emitModelToViewChange: false});
    });
  }

}

