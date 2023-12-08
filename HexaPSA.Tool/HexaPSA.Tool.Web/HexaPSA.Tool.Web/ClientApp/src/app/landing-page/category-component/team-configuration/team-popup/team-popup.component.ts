import { Component, Inject} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Shared } from 'src/app/shared/shared';
import { TeamConfiguration } from '../../../../entity/team-configuration.model';
import { UserList } from '../../../../entity/user-list.model';
import { RoleService } from '../../../../services/role-services';
import { UserListService } from '../../../../services/user-list.service';

@Component({
  selector: 'app-team-popup',
  templateUrl: './team-popup.component.html',
  styleUrls: ['./team-popup.component.scss']
})
export class TeamPopupComponent {

  formModelName             !: string;
  teamConfigurationmodel !: TeamConfiguration;
  userList: any;
  roleList: any;
  projectId?: string; 

  constructor(
    private dialogRef: MatDialogRef<TeamPopupComponent>, @Inject(MAT_DIALOG_DATA)
    public data: any,
    private shared: Shared,
    private roleService: RoleService,
    private userListService: UserListService,
  ) {
    this.shared.formModel.subscribe(data => { this.formModelName = data });
  }

  teamconfiguration = new FormGroup({
    role: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
  })

  get name() { return this.teamconfiguration.get('name') }
  get role() { return this.teamconfiguration.get('role') }

  ngOnInit(): void {
    this.getRoleData();
    this.getUserData();
    if (this.data.id) 
    {
      this.upateForm();
      this.projectId = this.data.projects.id;
  }else{
      this.projectId = this.data
}}

  // getting role data
  getRoleData() {
    this.roleService.getAll().subscribe((res: any) => {
      this.roleList = res;
    });
  }

  getUserData() {
    this.userListService.getAll().subscribe((res: UserList) => {
      this.userList = res
    });
  }

  upateForm() {
    this.teamconfiguration.controls['name'].setValue(this.data.user.id);
    this.teamconfiguration.controls['role'].setValue(this.data.role.id);
  }

  submit() {
    var result = {
      userId: this.teamconfiguration.value.name,
      projectId: this.projectId,
      roleId: this.teamconfiguration.value.role,
    }
    if (!this.teamconfiguration.invalid) { this.dialogRef.close(result); }
    }
   
  close() { this.dialogRef.close(); }
}
