import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { APIResponseModel } from 'src/app/models/api-response-model';
import { UserModel } from 'src/app/models/user/user.model';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  loginRepsonse: APIResponseModel<UserModel>;
  isLoading: boolean = false;

  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  });

  constructor(private authService: AuthenticationService, private router: Router) {}

  ngOnInit(): void { 
    if (this.authService.isUserLoggedIn()) {
      this.redirection();
    }
  }

  login(): void {

    if (this.loginForm.invalid)
      return;

    this.isLoading = true;

    let username = this.loginForm.get('username')?.value ?? "";
    let password = this.loginForm.get('password')?.value ?? "";

    this.authService.login(username, password).subscribe((value: APIResponseModel<UserModel>) => {
      this.isLoading = false;
      
      this.loginRepsonse = value;
      if(!this.loginRepsonse.isError) {
        this.redirection();
      }
    });

  }

  redirection() {
    let userDetails = this.authService.getUserDetails();

        if (userDetails) {
          if (userDetails.requirePasswordReset) {
            this.router.navigate(['/create-new-password']);
          }
          else {
            if (userDetails.roles[0] === "Admin" || userDetails.roles[0] === "SystemUser") {
              this.router.navigate(['/admin']);
            }
            else {
              // will add routing for other user types
            }
          }
        }
  }

}
