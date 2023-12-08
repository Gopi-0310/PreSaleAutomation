import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Shared } from './shared/shared';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Presale';
  heading: any;

  constructor(protected shared: Shared) {
    this.shared.heading.subscribe(res => { this.heading = res })
  }

}

