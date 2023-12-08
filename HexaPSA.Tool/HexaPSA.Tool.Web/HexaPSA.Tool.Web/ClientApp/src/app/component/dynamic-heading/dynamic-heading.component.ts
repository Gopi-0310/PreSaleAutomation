import { NgSwitch } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Shared } from 'src/app/shared/shared';

@Component({
  selector: 'app-dynamic-heading',
  templateUrl: './dynamic-heading.component.html',
  styleUrls: ['./dynamic-heading.component.scss']
})
export class DynamicHeadingComponent implements OnInit {
  heading: any;
  
  constructor(private shared: Shared) {
    this.shared.heading.subscribe(data => {
      this.heading = data;
    })
  }

  ngOnInit(): void {}

}
