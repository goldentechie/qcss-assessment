import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-main-navigation',
  templateUrl: './main-navigation.component.html',
  styleUrls: ['./main-navigation.component.scss']
})
export class MainNavigationComponent implements OnInit {

  userRole: string;
  userOrganization: string | undefined;
  notifications: any[] = [];

  constructor(private authService: AuthenticationService, private router: Router) {
    this.userRole = authService.getRole();
  }

  ngOnInit(): void {
  }

}
