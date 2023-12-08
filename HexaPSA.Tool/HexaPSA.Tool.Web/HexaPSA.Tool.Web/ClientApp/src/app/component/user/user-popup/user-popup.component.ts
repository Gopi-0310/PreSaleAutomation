import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Shared } from 'src/app/shared/shared';
import { UserList } from '../../../entity/user-list.model';
import { RoleService } from '../../../services/role-services';

@Component({
  selector: 'app-user-popup',
  templateUrl: './user-popup.component.html',
  styleUrls: ['./user-popup.component.scss']
})
export class UserPopupComponent {

  formModelName !: string;
  UserListmodel !: UserList;
  roleList       : any;

  constructor(
    private dialogRef  : MatDialogRef<UserPopupComponent>, @Inject(MAT_DIALOG_DATA)
    public data        : any,
    private shared     : Shared,
    private roleService: RoleService
  ) {
    this.shared.formModel.subscribe(data => { this.formModelName = data });
  }

  userData = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    fullName: new FormControl('', [Validators.required]),
    roleId: new FormControl('', [Validators.required]),
    eMail: new FormControl('', [Validators.required]),
  })

  get userName() { return this.userData.get('userName') }
  get fullName() { return this.userData.get('fullName') }
  get roleId() { return this.userData.get('roleId') }
  get eMail() { return this.userData.get('eMail') }

  ngOnInit(): void {
    this.getRoleData();
  //  if (this.data.id)
  //    this.upateForm();
  }

  upateForm() {
    this.userData.controls['userName'].setValue(this.data.userName);
    this.userData.controls['fullName'].setValue(this.data.fullName);
    this.userData.controls['roleId'].setValue(this.data.roleId);
    this.userData.controls['eMail'].setValue(this.data.name);
  }

  // getting role data
  getRoleData() {
    this.roleService.getAll().subscribe((res: any) => {
      this.roleList = res;
    });
  }

  onChangesRole(role:any) {
    console.log(role)
  }

  submit() {

    if (this.userData) {
      let roleVale = this.userData.value.roleId;
      for (var i = 0; i < this.roleList.length; i++) {
        if (this.roleList[i].name == roleVale) {
          this.userData.controls['roleId'].setValue(this.roleList[i].id);
          if (!this.userData.invalid) { this.dialogRef.close(this.userData); }
        }
      }
    }
  }

  close() { this.dialogRef.close(); }
}
