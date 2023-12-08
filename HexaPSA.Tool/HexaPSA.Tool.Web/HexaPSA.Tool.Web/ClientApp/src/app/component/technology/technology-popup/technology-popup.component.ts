import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';


@Component({
  selector: 'app-technology-popup',
  templateUrl: './technology-popup.component.html',
  styleUrls: ['./technology-popup.component.scss']
})
export class TechnologyPopupComponent {

  constructor(
    private dialogRef: MatDialogRef<TechnologyPopupComponent>, @Inject(MAT_DIALOG_DATA)
    public data: any,
  ) { }

  ngOnInit(): void {
    if (this.data && this.data.id != null) {
      this.setForm();
    }
  }

  //form group.................
  RoleForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
  })
  get name() { return this.RoleForm.get('name') }

  setForm() {
    this.RoleForm.controls['name'].setValue(this.data.name);
  }

  close() { this.dialogRef.close(); }

  submit() {
    if (!this.RoleForm.invalid) { this.dialogRef.close(this.RoleForm); }
  }
}
