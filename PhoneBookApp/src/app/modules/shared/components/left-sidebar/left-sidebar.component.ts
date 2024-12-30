import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'div[app-left-sidebar]',
  templateUrl: './left-sidebar.component.html',
  styleUrls: ['./left-sidebar.component.scss']
})
export class LeftSidebarComponent {
  
  userName: string;
  userRole: string;

  constructor(private authService: AuthenticationService, private router: Router) {
    this.userName = authService.getUserName();
    this.userRole = authService.getRole();
  }

  onLogout() {
    this.authService.logout();
    this.router.navigateByUrl("/");
  }
}
