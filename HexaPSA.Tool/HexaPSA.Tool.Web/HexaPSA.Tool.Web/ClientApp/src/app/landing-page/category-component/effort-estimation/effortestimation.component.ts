import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Shared } from 'src/app/shared/shared';
import { AppConstants } from 'src/environments/AppConstants';
import { ApiIntractionsService } from 'src/app/services/api-intractions.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { Location } from '@angular/common';

// importing all nested modules
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';

//Model
import { workStream } from '../../../entity/workstream-activity';
import { environment } from '../../../../environments/environment';
import { RoleService } from '../../../services/role-services';
import { Role } from '../../../entity/role.model';
import { EffortEstimationService } from '../../../services/effort-estimation.service';
import { WorkStreamActivityService } from '../../../services/workstreamactivity.service';
import { workStreamItems } from '../../../entity/workstream-items.model';
import { ProjectService } from '../../../services/project.service';
import { WorkStreamService } from '../../../services/workstream.service';
import { ToastrService } from 'ngx-toastr';
import { CapacityUtilizationService } from '../../../services/capacity-utilization.service';


@Component({
  selector: 'app-effortestimation',
  templateUrl: './effortestimation.component.html',
  styleUrls: ['./effortestimation.component.scss']
})
export class EffortestimationComponent implements OnInit {

  //Todo 
  // - Effort Estimation Model
  // - When clicking Add workstream the last accordion need to be focused
  // - When opening Accordion the accordion body need to be focused

  // Variables
  projectTitle: any;
  workstreamData: any;
  workstreamChildData: any;
  // hours: number = 844;
  // hoursPerWeek: number = 40;
  // weeks: number = this.hours / this.hoursPerWeek;
  enableWorkstreamInput: boolean = false;
  roleList: any;
  workStreamActivityId: number = 0;

  treeControl = new NestedTreeControl<workStream>((node) => node.children);
  /*  dataSource = new MatTreeNestedDataSource<workStreamItems>();*/
  dataSource: any;
  parentId!: number;
  isVisible: boolean = true;
  isLoading: boolean = true;
  project: any;
  treeData: any[] | undefined;
  calculateWeek: number = 0;
  noOfWeeks: any[] = [];
  totalWeeksAndHours !: any;
  totalHoursAndCost: any = 0;
 
  constructor(
    private router: Router,
    private shared: Shared,
    private api: ApiIntractionsService,
    private roleService: RoleService,
    private projectService: ProjectService,
    private effortEstimationService: EffortEstimationService,
    private workStreamActivityService: WorkStreamActivityService,
    private workStreamService: WorkStreamService, 
    private _location: Location,
    private capacityUtilizationService: CapacityUtilizationService,
    private toastrService: ToastrService

  ) {
    this.shared.setTitle(AppConstants.effortEstimation);
    this.projectTitle = this.api.getItem("projectDetails");
    let currentStateExtras = this.router.getCurrentNavigation()?.extras.state;
    var data: any = currentStateExtras;
    this.project = data;

  }

  ngOnInit(): void {
    this.getWorkstream();
    this.getWorkstreamItems();
    this.getRole();
    this.getProjectDetails();
    this.getWorkStreamCalculated();
  }

  // Decorators
  @ViewChild('workstreamRef') workstreamRef!: ElementRef;

  //FormControl
  WorkStreamForm = new FormGroup({
    workstream: new FormControl('', [Validators.required]),
  })

  get workstream() { return this.WorkStreamForm.get('workstream') }


  //form group.................
  addChildWorkstream = new FormGroup({
    activity: new FormControl('', [Validators.required]),
    roles: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    eta: new FormControl('', [Validators.required]),
    week: new FormControl('', [Validators.required]),
  });
  get activity() {
    return this.addChildWorkstream.get('activity');
  }
  get roles() {
    return this.addChildWorkstream.get('roles');
  }
  get description() {
    return this.addChildWorkstream.get('description');
  }
  get eta() {
    return this.addChildWorkstream.get('eta');
  }
  get week() {
    return this.addChildWorkstream.get('week');
  }

 getWorkStreamCalculated(){
  this.workStreamService.getByIdTotalWeeks(this.project.projectId).subscribe(res =>{
   console.log("total calculations",res);
   this.totalWeeksAndHours = res;
  });
 }


  getProjectDetails() {
    this.projectService.getById(this.project.projectId).subscribe((res) => {
      this.calculateWeek = this.weeksBetween(
        new Date(res.effectiveStartDate), new Date(res.effectiveEndDate)
      );

      if (this.calculateWeek > 0) {
        for (var week = 1; week < this.calculateWeek; week++) {
          this.noOfWeeks.push(week);
        }
      }
   
    })
  }

  weeksBetween(startDate: any, endDate: any) {
    const msInWeek = 1000 * 60 * 60 * 24 * 7;
    return Math.round(Math.abs(endDate - startDate) / msInWeek);
  }

  getWorkstreamItems() {
    this.workStreamActivityService.getById(this.project.projectId).subscribe({
      next: (res: any) => {
        this.dataSource = this.convertToTree(res, null);
        console.log('this.dataSource', this.dataSource)
        // this.aggregateDataByWorkStreamId(res);
        if (this.dataSource.length>0) {
          // this.aggregateDataByWorkStreamId(res);
        }
      },
      error: () => {
      this.toastrService.error(AppConstants.ErrorMessage)
      }
    });
  }

  //getting total cost and weeks
 

  getWorkstream() {
    this.workStreamService.getById(this.project.projectId).subscribe({
      next: (res) => {
        this.workstreamData = res;
      },
      error: () => {
        this.toastrService.error(AppConstants.ErrorMessage)
      }
    });
  }

  getRole() {
    this.capacityUtilizationService.getAll(this.project.projectId)
      .subscribe((res) => {
        this.roleList = res;
      });
  }

  //Add a Workstream 
  add_cancel_Workstream() {
    this.WorkStreamForm.reset();
    this.enableWorkstreamInput = !this.enableWorkstreamInput;
    setTimeout(() => { //It is initialized after ViewInit
      this.workstreamRef.nativeElement.focus();
    });
  }

  addWorkstreamInput() {
    let form = this.WorkStreamForm.value;
    var addWorkstream =
    {
      "name": form.workstream,
      "projectId": this.project.projectId
    }
    this.api.postMethod('https://localhost:44450/api/WorkStream', addWorkstream).subscribe({
      next: () => {
        this.getWorkstream();
        setTimeout(() => {
          this.enableWorkstreamInput = false;
          this.WorkStreamForm.reset();
          this.toastrService.success(AppConstants.ProjectSucessMessage)
        });
      },
      error: () => {
        this.toastrService.error(AppConstants.ErrorMessage)
      }
    })
  }
  cancelWorkstreamInput() {
    this.WorkStreamForm.reset();
  }

  deleteWorkstream(item: any) {
    this.workStreamService.remove(item.id).subscribe((res) => {
      this.ngOnInit();
      this.toastrService.info(AppConstants.DeleteMessage);
    });
  }

  hasChild = (_: number, node: workStream) =>
    !!node.children && node.children.length > 0;


  createItem(parentNode: workStream) {
    this.addChildWorkstream.reset();
    this.workStreamActivityId = parentNode.id;
    let findId = 'create' + parentNode.id;
    let createForm = document.getElementById(findId) as HTMLTextAreaElement;
    createForm.style.display = 'block';
  }

  addWorkstream(parentNode: any, activity: string) {
    this.WorkStreamForm.reset();
    this.addChildWorkstream.reset();
    let findId = 'Location' + parentNode.id;
    let removeId = 'remove' + parentNode.id;
    let createForm = document.getElementById(findId) as HTMLTextAreaElement;
    let removeForm = document.getElementById(removeId) as HTMLTextAreaElement;
    if (activity == 'add') {
      createForm.style.display = 'block';
      removeForm.style.display = 'block';
      this.parentId = parentNode.parentId == 0 ? 0 : parentNode.id;
      this.isVisible = false;
      this.workStreamActivityId = parentNode.workStream.id;
    } else {
      createForm.style.display = 'none';
      removeForm.style.display = 'none';
      this.parentId = 0;
      this.isVisible = true;
    }
  }

  editWorkstream(parentNode: workStream) {
    let eta = parentNode.eta.toString();
    this.addChildWorkstream.controls['activity'].setValue(parentNode.activity);
    this.addChildWorkstream.controls['eta'].setValue(eta);
    this.addChildWorkstream.controls['week'].setValue(parentNode.weeks);
    this.addChildWorkstream.controls['description'].setValue(parentNode.description);
    this.addChildWorkstream.controls['roles'].setValue(parentNode.roles);
    this.addWorkstream(parentNode, 'add')
  }

  remove(node: workStream) {
    console.log('node', node);
    this.workStreamActivityService.remove(node.id).subscribe((res) => {
      console.log(res);
      this.ngOnInit();
      this.toastrService.info(AppConstants.DeleteMessage);
    });

  }

  save() {
    let formValue = this.addChildWorkstream.value;
    let week = formValue.week;
    let hour = formValue.eta

    let dummyworkStream =
    {
      activity: formValue.activity,
      parentId: this.parentId ? this.parentId : null,
      hours: hour ? parseInt(hour) : null,
      week: week ? parseInt(week) : null,
      roleId: formValue.roles,
      description: formValue.description,
      workStreamActivityId: this.workStreamActivityId
    };
    this.workStreamActivityService.save(dummyworkStream).subscribe((res) => {
      this.WorkStreamForm.reset();
      this.ngOnInit();
    });
  }

  // converting parent to child tree formate...........

  convertToTree(data: any[], parentId: null) {
    const tree: any[] = [];
    data.forEach((item:any) => {
      if (item.parentId === parentId) {
        const children = this.convertToTree(data, item.id);
        if (children.length) {
          item.children = children;
        }
        tree.push(item);
      }
    });
    return tree;
  }

  backClicked() {
    this._location.back();
  }

}
