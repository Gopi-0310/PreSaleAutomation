import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-rolepopup',
  templateUrl: './rolepopup.component.html',
  styleUrls: ['./rolepopup.component.scss']
})
export class RolepopupComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<RolepopupComponent>, @Inject(MAT_DIALOG_DATA)
    public data: any,
  ) { }

  ngOnInit(): void {
    if (this.data && this.data.id != null) {
      this.setForm();
    }
  }

  //form group.................
  RoleForm = new FormGroup({
    code: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
  })
  get code() { return this.RoleForm.get('code') }
  get name() { return this.RoleForm.get('name') }

  setForm() {
    this.RoleForm.controls['code'].setValue(this.data.code);
    this.RoleForm.controls['name'].setValue(this.data.name);
  }

  close() { this.dialogRef.close(); }

  submit() {  if (!this.RoleForm.invalid) { this.dialogRef.close(this.RoleForm); } }
}

