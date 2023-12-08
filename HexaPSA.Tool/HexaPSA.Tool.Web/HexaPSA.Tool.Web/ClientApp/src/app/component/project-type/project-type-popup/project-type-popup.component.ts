import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-project-type-popup',
  templateUrl: './project-type-popup.component.html',
  styleUrls: ['./project-type-popup.component.scss']
})
export class ProjectTypePopupComponent {

  constructor(
    private dialogRef: MatDialogRef<ProjectTypePopupComponent>, @Inject(MAT_DIALOG_DATA)
    public data: any,
  ) { }

  ngOnInit(): void {
    if (this.data && this.data.id != null) {
      this.setForm();
    }
  }

  //form group.................
  RoleForm = new FormGroup({ name: new FormControl('', [Validators.required]),})
  
  get name() { return this.RoleForm.get('name') }

  setForm() { this.RoleForm.controls['name'].setValue(this.data.name);}

  close() { this.dialogRef.close(); }

  submit() { if (!this.RoleForm.invalid) { this.dialogRef.close(this.RoleForm); }}
}
