import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Shared } from 'src/app/shared/shared';
import { ApiIntractionsService } from '../../../../services/api-intractions.service';
import { environment } from '../../../../../environments/environment';
import { CapacityUtilizationMapping } from '../../../../entity/capacity-utilization-mapping.model';
import { ResourceRatecardService } from 'src/app/services/resource-ratecard.service';
import { CapacityUtilization } from 'src/app/entity/capacity-utilization.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-capacity-popup',
  templateUrl: './capacity-popup.component.html',
  styleUrls: ['./capacity-popup.component.scss']
})
export class CapacityPopupComponent {

  roleList           : any;
  formModelName     !: string;
  selectedRoleValue !: string;
  capacitymodel     !: CapacityUtilization;
  capacityMappingModel !: CapacityUtilizationMapping;
  roleId            !: string;
  resourceData       : any;
  datas              : any;
  projectId           !: any;
  defaultValue        !: string;
  locationArray       : any[] = [{name:'OnSite'},{name:"OffShore"}]
  constructor(
    private dialogRef  : MatDialogRef<CapacityPopupComponent>, @Inject(MAT_DIALOG_DATA)
    public data        : any,
    private shared     : Shared,
    private api        : ApiIntractionsService,
    private resourceRateService: ResourceRatecardService,
    private router: Router,

  ) {
    this.shared.formModel.subscribe(data => { this.formModelName = data });
    let currentStateExtras = this.router.getCurrentNavigation()?.extras.state;
    this.projectId = currentStateExtras;
  }

  CapacityUtilizationForm = new FormGroup({
    role         : new FormControl('',[Validators.required]),
    code         : new FormControl('',[Validators.required]),
    defaultRate  : new FormControl('',[Validators.required]),
    assignRate   : new FormControl(),
    hours        : new FormControl(),
    location     : new FormControl(),
  })
  
  get role()        { return this.CapacityUtilizationForm.get('role') }
  get code()        { return this.CapacityUtilizationForm.get('code')}
  get defaultRate() { return this.CapacityUtilizationForm.get('defaultRate') }
  get assignRate()  { return this.CapacityUtilizationForm.get('assignRate') }
  get hours()       { return this.CapacityUtilizationForm.get('hours') }
  get location()    {return this.CapacityUtilizationForm.get('location')}

  ngOnInit(): void {
    this.getRoleData();
    this.updateRolerate();
    this.getresourceRateService();
  }



  getresourceRateService() {
    this.resourceRateService.getAll().subscribe(res => {
      this.resourceData = res;
    })
  }


  submit(status ?: string) {
    if (!this.CapacityUtilizationForm.invalid) { 
      if (this.data.role == undefined || this.data.role ==null) {
        this.capacitymodel ={
          roleId     : this.roleId,
          projectId: this.data,
          hours: this.CapacityUtilizationForm.value.hours,
          rate: this.CapacityUtilizationForm.value.assignRate == null ? this.defaultValue : this.CapacityUtilizationForm.value.assignRate,
          location : this.CapacityUtilizationForm.value.location
         }
         this.dialogRef.close(this.capacitymodel);
      }
       else{
        this.capacityMappingModel ={
          roleId     : this.data.role,
          projectId: this.data.projectId,
          hours      : this.CapacityUtilizationForm.value.hours,
          rate: this.CapacityUtilizationForm.value.assignRate == null ? this.defaultValue : this.CapacityUtilizationForm.value.assignRate,
          location : this.CapacityUtilizationForm.value.location
         }
         this.dialogRef.close(this.capacityMappingModel);
       }

     }
    
  }

  close() { this.dialogRef.close(); }

  onChanges(changesData: any){
     this.roleId  = changesData.id;
    for (let i = 0; i < this.resourceData.length; i++) {
        if(changesData.id == this.resourceData[i].role.id && this.resourceData[i].projectId == null)
        {
          this.defaultValue = "";
          this.CapacityUtilizationForm.controls['defaultRate'].setValue("");
          this.CapacityUtilizationForm.controls['code'].setValue("");
          this.CapacityUtilizationForm.controls['defaultRate'].setValue(this.resourceData[i].rate);
          this.CapacityUtilizationForm.controls['code'].setValue(changesData.code);
          this.CapacityUtilizationForm.controls['defaultRate'].disable();
          this.CapacityUtilizationForm.controls['code'].disable();
          this.defaultValue = this.resourceData[i].rate;
        }
      }
   }

   // getting role data
   getRoleData(){
    this.api.getMethod(environment.ratecard_endpoint_url).subscribe((res: any) => {
      this.roleList = res;
    });
   }



   //getMethod for accessing initial table value...
  resourceTableData(){
   return this.resourceRateService.getAll().subscribe(res=>{
     this.resourceData = res;
     })
  }

  updateRolerate() {
    if (this.data.role)
     {
      this.CapacityUtilizationForm.controls['code'].setValue(this.data.role.code);
      this.CapacityUtilizationForm.controls['role'].setValue(this.data.role.name);
      this.CapacityUtilizationForm.controls['defaultRate'].setValue("Access to change Role");
      this.CapacityUtilizationForm.controls['assignRate'].setValue(this.data.resourceRate.rate);
       this.CapacityUtilizationForm.controls['hours'].setValue(this.data.hours);
       this.CapacityUtilizationForm.controls['location'].setValue(this.data.location)
       this.CapacityUtilizationForm.controls['defaultRate'].disable();
       this.CapacityUtilizationForm.controls['code'].disable();
     }
  }
}
