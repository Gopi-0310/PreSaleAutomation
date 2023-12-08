import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Shared } from 'src/app/shared/shared';
import { TimeTracker } from '../../../../entity/time-tracker.model';
import { UserListService } from '../../../../services/user-list.service';
import { UserList } from '../../../../entity/user-list.model';

@Component({
  selector: 'app-time-tracker-popup',
  templateUrl: './time-tracker-popup.component.html',
  styleUrls: ['./time-tracker-popup.component.scss']
})
export class TimeTrackerPopupComponent {

  formModelName             !: string;
  timeTrackermodel           !: TimeTracker;
  userList: any;
  isprojectId: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<TimeTrackerPopupComponent>, @Inject(MAT_DIALOG_DATA)
    public data: any,
    private shared: Shared,
    private userListService: UserListService,
  ) {
    this.shared.formModel.subscribe(data => {
      this.formModelName = data
    });
  }

  timetrackerform = new FormGroup({
    userId: new FormControl('', [Validators.required]),
    activity: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    hours: new FormControl('', [Validators.required]),
    date: new FormControl('', [Validators.required]),
  })

  get userId() { return this.timetrackerform.get('userId') }
  get activity() { return this.timetrackerform.get('activity') }
  get description() { return this.timetrackerform.get('description') }
  get hours() { return this.timetrackerform.get('hours') }
  get date() { return this.timetrackerform.get('date') }

  ngOnInit(): void {
    console.log(this.data)
    if (this.data.projectId != null) {
      this.upateForm();
      this.getUserData();
      this.isprojectId = true;
    }
  }

  getUserData() {
    this.userListService.getAll().subscribe((res: UserList) => {
     this.userList  = res
    });
  }

  upateForm() {
    if (this.data.id) {
      this.timetrackerform.controls['userId'].setValue(this.data.userId);
      this.timetrackerform.controls['activity'].setValue(this.data.activity);
      this.timetrackerform.controls['description'].setValue(this.data.description);
      this.timetrackerform.controls['hours'].setValue(this.data.hours);
      this.timetrackerform.controls['date'].setValue(this.data.activityDate);
      this.timetrackerform.controls['userId'].disable();
    }
  }

  submit() {
    if (!this.timetrackerform.invalid) {
      console.log(this.timetrackerform.value);
      var result = this.timetrackerform.value;
      var hours = Number(result.hours);
      var timeTrackermodel = {
        id: this.data.id ? this.data.id : '' ,
        userId: this.timetrackerform.value.userId ? this.timetrackerform.value.userId : this.data.userId,
        activity: result.activity,
        description: result.description,
        hours: hours ,
        projectId: this.data.projectId,
        activityDate: result.date
      }
      console.log('this.timetrackerform', timeTrackermodel);
      this.dialogRef.close(timeTrackermodel);
    }
  }

  close() { this.dialogRef.close(); }
}
