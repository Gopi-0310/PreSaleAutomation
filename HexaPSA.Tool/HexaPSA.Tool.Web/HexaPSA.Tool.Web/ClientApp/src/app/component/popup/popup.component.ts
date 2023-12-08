import { Component, ElementRef, Inject, Input, OnInit, Pipe, ViewChild, inject } from '@angular/core';
import { FormArray, FormBuilder, FormControl,FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';
import {LiveAnnouncer} from '@angular/cdk/a11y';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { Observable, map, startWith } from 'rxjs';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiIntractionsService } from 'src/app/services/api-intractions.service';
import { Shared } from 'src/app/shared/shared';
import { AppConstants } from 'src/environments/AppConstants';
import { environment } from 'src/environments/environment';
import { capacityUtilizationCard } from 'src/app/model/capacity-utilization-card';
import { TeamConfigurationCard } from 'src/app/model/team-configuration-card';
import { TooltipPosition } from '@angular/material/tooltip';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import {MatSelectModule} from '@angular/material/select';
import { EstimationDashboard } from 'src/app/model/EstimationDashboard';
import { elements } from 'chart.js';
import { ResourceRatecard } from 'src/app/entity/resource-ratecard.model';

@Component({
  selector: 'app-popup',
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.scss']
})
export class PopupComponent implements OnInit{

 //Entity declarations.....
  model                  !: ResourceRatecard;
  dashBoardModel         !: EstimationDashboard;
  
  //common variables.............
   heading           !: string;
   formModelName     !: string;
   selectedRoleValue !: string;
   estimatedForm     !: FormGroup;
   dashBoardData      : any;
   expanded_accordion : boolean = true ;
   clickAdd           : boolean = false ;
   showOptions        = false;
   panelOpenState     = false;
   dateArray          : string[] = [];
   userRole           : boolean = true;
   positionOptions    : TooltipPosition = "left";
   technologys        : boolean = true;
   technologyDropdown !: string;
   technologyIndex    !: string;
   roleList           : any;
   roleId             !: string;
   announcer            = inject(LiveAnnouncer);
   separatorKeysCodes   : number[] = [ENTER, COMMA];
   
  constructor( 
    private dialogRef  : MatDialogRef<PopupComponent>,@Inject(MAT_DIALOG_DATA)
    public data        : any,
    private shared     : Shared,
    private api        : ApiIntractionsService,
    private formBuilder: FormBuilder,
    private  date      : DatePipe
    ){
      this.shared.formModel.subscribe(data =>{ this.formModelName = data});
     }

   // TODO we need to access once the backend original api is ready
   ngAfterViewInit() { this.getRoleData();}

   ngOnInit(): void { 
    this.updateProjectData();
  }

  onChanges(res: any) {
    this.ResourceForm.controls['code'].setValue("");
    this.ResourceForm.controls['code'].setValue(res.code);
    this.ResourceForm.controls['code'].disable();
    this.roleId = res.id;
  }


  getRoleData(){
    this.api
    .getMethod(environment.ratecard_endpoint_url)
    .subscribe((res: any) => {
      this.roleList = res;
    });
   }

  //form group.................
  ResourceForm = new FormGroup({
    code       : new FormControl ('',[Validators.required]),
    role       : new FormControl ('',[Validators.required]),
    rate       : new FormControl ('',[Validators.required])
  })
   get code()     { return this.ResourceForm.get('code')}
   get role()     { return this.ResourceForm.get('role')}
   get rate()     { return this.ResourceForm.get('rate')}


//pop-up action methods............
submit(data: string) {
 if(this.ResourceForm.valid){
  var formData = Object.assign({} , this.ResourceForm.value);
  formData.code =  this.roleId;  
  this.dialogRef.close(formData); 
  };
} 

close(){
  this.dialogRef.close();
}
 
//All Component Editable Data
  updateProjectData(){
    if(this.data != null && this.formModelName == AppConstants.rateCard){ 
     // this.ResourceForm.setValue(this.data); 
      this.ResourceForm.controls['code'].setValue(this.data.role.code);
      this.ResourceForm.controls['role'].setValue(this.data.role.name);
      this.ResourceForm.controls['rate'].setValue(this.data.rate);
      this.ResourceForm.controls['code'].disable();
      this.roleId = this.data.role.id;
    } 
   }

  
}
 
