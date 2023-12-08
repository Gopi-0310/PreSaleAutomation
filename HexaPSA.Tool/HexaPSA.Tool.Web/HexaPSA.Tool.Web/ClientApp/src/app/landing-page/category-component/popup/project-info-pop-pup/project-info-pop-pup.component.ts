import { LiveAnnouncer } from '@angular/cdk/a11y';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { DatePipe } from '@angular/common';
import { Component, ElementRef, Inject, ViewChild, inject } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators, FormArray } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TooltipPosition } from 'chart.js';
import { Observable } from 'rxjs';
import { ProjectPopupComponent } from 'src/app/component/home/project-popup/project-popup.component';
import { EstimationDashboard } from 'src/app/entity/estimation-dashboard.model';
import { Technology } from 'src/app/entity/technology.model';
import { capacityUtilizationCard } from 'src/app/model/capacity-utilization-card';
import { ResourceRateCard } from 'src/app/model/resource-rate-card';
import { TeamConfigurationCard } from 'src/app/model/team-configuration-card';
import { ApiIntractionsService } from 'src/app/services/api-intractions.service';
import { ProjectTypeService } from 'src/app/services/project-type.service';
import { RoleService } from 'src/app/services/role-services';
import { TechnologyService } from 'src/app/services/technology.service';
import { UserListService } from 'src/app/services/user-list.service';
import { Shared } from 'src/app/shared/shared';

@Component({
  selector: 'app-project-info-pop-pup',
  templateUrl: './project-info-pop-pup.component.html',
  styleUrls: ['./project-info-pop-pup.component.scss']
})
export class ProjectInfoPopPupComponent {
 //Entity declarations.....
 model                  !: ResourceRateCard;
 dashBoardModel         !: EstimationDashboard;
 capacitymodel          !: capacityUtilizationCard;
 teamConfigurationmodel !: TeamConfigurationCard;
 //common variables.............
 heading           !: string;
 formModelName     !: string;
 selectedRoleValue !: string;
 estimatedForm     !: FormGroup;
 addestimationdashbord!:EstimationDashboard;
 dashBoardData: any;
 expanded_accordion: boolean = true;
 clickAdd: boolean = false;
 showOptions = false;
 panelOpenState = false;
 dateArray: string[] = [];
 userRole: boolean = true;
//  positionOptions: TooltipPosition = "left";
 technologys: boolean = true;
 technologyDropdown !: string;
 technologyIndex    !: string;
 roleList: any;
 @ViewChild('techInput') techInput !: ElementRef<HTMLInputElement>;
 separatorKeysCodes: number[] = [ENTER, COMMA];
 projectTypeList: any;
 userList: any;
 

 //technology list variables
 filteredTechnologies: Observable<Technology[]> | undefined;
 technologyList: any[] = [];
 newTecnoList:any[] =[];
 technologyarray:any[] = [];
 uniqueId = 0;
 projectId:any =null;
 technology = new FormControl(['']);
 announcer = inject(LiveAnnouncer);

 constructor(
   private dialogRef: MatDialogRef<ProjectInfoPopPupComponent>, @Inject(MAT_DIALOG_DATA)
   public data: any,
   private shared: Shared,
   private api: ApiIntractionsService,
   private formBuilder: FormBuilder,
   private date: DatePipe,
   private roleService: RoleService,
   private projectTypeIdService: ProjectTypeService,
   private userListService: UserListService,
   private technologyService:TechnologyService
   
   
 ) {
   this.shared.formModel.subscribe(data => { this.formModelName = data });
   this.newEstimateForm();
  
 }

 // TODO we need to access once the backend original api is ready
 ngAfterViewInit() { }
 ngOnInit(): void {
   this.updateProjectData();
   this.resorceTableData();
   this.getProjectType();
   this.getUserList();
   this.getTechnology();
 }

 getTechnology() {
   this.technologyService.getAll().subscribe((res) => {
     if (Array.isArray(res)) {
       for (let i = 0; i < res.length; i++) {
         this.technologyList.push(res[i]);
       }
       console.log('Technology added:', this.technologyList);
     } else {
       console.error('Response is not an array:', res);
     }
   });
 }

 resorceTableData() {
   this.roleService.getAll().subscribe(res => {
     this.roleList = res;
   })
 }

 getProjectType() {
   this.projectTypeIdService.getAll().subscribe(res => {
     this.projectTypeList = res;
   })
 }

 getUserList() {
   this.userListService.getAll().subscribe(res => {
     this.userList = res;
   })
 }

//  updateRolerate(roledata: any) {
//    if (roledata) {
//      setTimeout(() => {
//        let findRate = this.roleList.filter((x: any) => x.name == roledata);
//        this.CapacityUtilization.controls['defaultrate'].setValue(findRate[0].defaultrate);
//      }, 200);
//    }
//  }


 //form group EstimationDashboard.................
 newEstimateForm() {
   this.estimatedForm = this.formBuilder.group({
     name: ['', [Validators.required]],
     projectTypeId: ['', [Validators.required]],  
     technology:  this.technologyList,
     effectiveStartDate: ['', [Validators.required]],
     effectiveEndDate: ['', [Validators.required]],
     estimatedUsers: this.formBuilder.array([
       this.formBuilder.group({
         userId: ['', [Validators.required]],
         roleId: ['', [Validators.required]],
       })
     ])
   })
   console.log('Estimated Form:', this.estimatedForm);
 }
 get estimatedUsers() { return this.estimatedForm.get("estimatedUsers") as FormArray; }

 // add Dynamic Array Form...........
 addAnotherUser() {
   if (!this.estimatedUsers.invalid) {
     let control = <FormArray>this.estimatedForm.controls["estimatedUsers"];
     control.push(
       this.formBuilder.group({
         userId: ['', [Validators.required]],
         roleId: ['', [Validators.required]],
       })
     );
   }
 }

 editAnotherUser() {
   for (let i = 0; i < this.data.estimatedUsers.length; i++) {
     let control = <FormArray>this.estimatedForm.controls["estimatedUsers"];
     control.push(
       this.formBuilder.group({
         userId: this.data.estimatedUsers[i].userName,
         roleId: this.data.estimatedUsers[i].roles,
       })
     );
   }
   this.removePlayer(0);
 }
 // Remove Form.............
 removePlayer(index: number) {
   let control = <FormArray>this.estimatedForm.controls["estimatedUsers"];
   control.removeAt(index)
 }

 
 //pop-up action methods............
 submit(data: string) {
   if (!this.estimatedForm.invalid) {
     let startDate = this.date.transform((this.estimatedForm.value.effectiveStartDate), 'yyyy-MM-ddTHH:mm:ss.SSS\'Z\'', 'en-US');
     let endDate = this.date.transform((this.estimatedForm.value.effectiveEndDate), 'yyyy-MM-ddTHH:mm:ss.SSS\'Z\'', 'en-US');
     this.addestimationdashbord = this.estimatedForm.value;
     var formData = Object.assign({}, this.addestimationdashbord);
     formData.effectiveStartDate = startDate;
     formData.effectiveEndDate   = endDate;
     formData.technologies       = this.technologyList;
     if(this.projectId !=null){
       formData.id=this.projectId;
     }
     console.log('formData', formData)
     this.dialogRef.close(formData);
   }
 }
 close() { this.dialogRef.close(); }
 // Estimation DashBoard Method..............
 //Angular Material - AutoComplete 
 

 // filter the value..........................
 applyFilter(event: Event) {
   const fillter_Value = (event.target as HTMLInputElement).value
   this.dashBoardData.filter = fillter_Value.trim().toLocaleLowerCase();
 }

 //All Component Editable Data
 updateProjectData() {
   if (this.data) {
     console.log('updateProject',this.data);
     this.projectId = this.data.id;
     this.estimatedForm.get('name')?.setValue(this.data.name);
     const projectTypeId = this.data.projectTypes.length > 0 ? this.data.projectTypes[0].id : null;
     this.estimatedForm.get('projectTypeId')?.setValue(projectTypeId);
     this.estimatedForm.get('technology')?.setValue(this.data.technology.name);
     // this.estimatedForm.controls['technology'].setValue(this.data.technology.name);
     this.estimatedForm.get('effectiveStartDate')?.setValue(this.data.effectiveStartDate);
     this.estimatedForm.get('effectiveEndDate')?.setValue(this.data.effectiveEndDate);

     this.clearEstimatedUsers();
     this.data.estimatedUsers.forEach((item: any) => {
       var user = item.user;
       var role = item.role;
 
       var estimatedUserFormGroup = this.formBuilder.group({
         userId: [user.id, Validators.required],
         roleId: [role.id, Validators.required]
       });
 
       this.estimatedUsers.push(estimatedUserFormGroup);
     });
   }
 }
 
//removing the user array 
 clearEstimatedUsers() {
   while (this.estimatedUsers.length !== 0) {
     this.estimatedUsers.removeAt(0);
   } 
 }
 
 selected(event: MatAutocompleteSelectedEvent): void {
   this.technologyarray.push(event.option.viewValue);
   this.techInput.nativeElement.value = '';
   this.technology.setValue(null);
 }

 //when we can click the technology input thta time it will be access.
 showInput() {
   this.technologys = false;
 }


 

 removeKeyword(keyword: any) {
   const index = this.technologyList.indexOf(keyword);
   if (index >= 0) {
     this.technologyList.splice(index, 1);
     // this.updateTechnologyFormControl();
     this.announcer.announce(`removed ${keyword}`);
   }
 }

 add(event: MatChipInputEvent): void {
   const value = (event.value || '').trim();

   // Add our keyword
   if (value) {
     this.technologyList.push(value);
   }

   // Clear the input value
   event.chipInput!.clear();
 }

}
