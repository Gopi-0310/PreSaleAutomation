import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Shared } from 'src/app/shared/shared';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  heading !: string;
   constructor(protected shared: Shared, private router:Router) {
    this.shared.heading.subscribe(res => { this.heading = res })
  }
}
