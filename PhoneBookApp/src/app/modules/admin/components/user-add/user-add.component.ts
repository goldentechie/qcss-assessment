import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterUser } from 'src/app/models/user/register-user.model';
import { UsersService } from 'src/app/services/users.service';
import { entityExistsValidator } from 'src/app/validators/entity-exists.validator';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.scss']
})
export class UserAddComponent implements OnInit {

  isLoading: boolean = false;
  userId: string | null;
  public userData: RegisterUser = new RegisterUser();

  ngOnInit() {
    
    this.userId = this.route.snapshot.paramMap.get('id');

    if (this.userId) {

      this.isLoading = true;
      this.userService.getById(this.userId).subscribe(value => {

        this.isLoading = false;

        if (!value.isError) {
          this.userData.email = value.data.email;
          this.userData.firstName = value.data.firstName;
          this.userData.lastName = value.data.lastName;
          this.userData.phoneNumber = value.data.phoneNumber;
        }

        

        this.registerForm = new FormGroup({
          email: new FormControl(this.userData.email, { validators: [Validators.email, Validators.required]}),
          firstName: new FormControl(this.userData.firstName, [Validators.required]),
          lastName: new FormControl(this.userData.lastName, [Validators.required]),
          phoneNumber: new FormControl(this.userData.phoneNumber),
        });
      });
    }
    
      this.registerForm = new FormGroup({
        email: new FormControl(this.userData.email, { validators: [Validators.email, Validators.required], asyncValidators: [entityExistsValidator(this.userService)], updateOn: 'blur'}),
        firstName: new FormControl(this.userData.firstName, [Validators.required]),
        lastName: new FormControl(this.userData.lastName, [Validators.required]),
        phoneNumber: new FormControl(this.userData.phoneNumber),
      });
    

    
  }

  registerForm: FormGroup;

  constructor(private userService: UsersService, private route: ActivatedRoute, private router: Router) { }

  onSubmit() {
    

    if (this.userId) {
      this.isLoading = true;
      this.userService.update(this.userId, this.registerForm.value).subscribe(() => {
        this.isLoading = false;
        this.router.navigate(['/admin/user-list']);
      });
    }
    else {
      this.isLoading = true;
      this.userService.add(this.registerForm.value).subscribe(() => {
        this.isLoading = false;
        this.router.navigate(['/admin/user-list']);
      });
    }
  }
}
