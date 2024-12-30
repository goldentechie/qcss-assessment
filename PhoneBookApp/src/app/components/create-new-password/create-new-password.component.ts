import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { APIResponseModel } from 'src/app/models/api-response-model';
import { UserModel } from 'src/app/models/user/user.model';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ConfirmPasswordValidator } from 'src/app/validators/confirm-password.validator';

@Component({
  selector: 'app-create-new-password',
  templateUrl: './create-new-password.component.html',
  styleUrls: ['./create-new-password.component.scss']
})
export class CreateNewPasswordComponent implements OnInit {

  apiResponse: APIResponseModel<UserModel>;
  isLoading: boolean = false;

  createPasswordForm = this.fb.group({
    currentPassword: new FormControl('', [Validators.required]),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required)
  },
  {
    validator: ConfirmPasswordValidator("password", "confirmPassword")
  });

  constructor(private authService: AuthenticationService, private router: Router, private fb: FormBuilder) {}
  
  ngOnInit(): void {
    
    let userDetails = this.authService.getUserDetails();
    if (!userDetails) {
      this.router.navigate(['/login']);
    }
  }

  create(): void {
    if (this.createPasswordForm.invalid)
      return;

    this.isLoading = true;

    let currentPassword = this.createPasswordForm.get('currentPassword')?.value ?? "";
    let password = this.createPasswordForm.get('password')?.value ?? "";

    this.authService.createNewPassword(currentPassword, password).subscribe((value: APIResponseModel<UserModel>) => {
      this.isLoading = false;
      
      this.apiResponse = value;
      if(!this.apiResponse.isError) {
        this.router.navigate(['/login']);
      }
    });
  }
}
