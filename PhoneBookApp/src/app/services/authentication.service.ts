import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { APIResponseModel } from '../models/api-response-model';
import { UserModel } from '../models/user/user.model';
import { UsersService } from './users.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private userService: UsersService) {}

  login(username: string, password: string): Subject<APIResponseModel<UserModel>> {
    
    let result = new Subject<APIResponseModel<UserModel>>();

    this.userService.authenticate(username, password).subscribe((value: APIResponseModel<UserModel>) => {
        if (!value.isError) {
          localStorage.setItem('userDetails', JSON.stringify(value.data));
        }
        return result.next(value);
      });

      return result;
  }

  createNewPassword(currentPassword: string, password: string): Subject<APIResponseModel<UserModel>> {
    
    let result = new Subject<APIResponseModel<UserModel>>();

    this.userService.createNewPassword(currentPassword, password)
      .subscribe((value: APIResponseModel<UserModel>) => {
        if (!value.isError) {
          localStorage.setItem('userDetails', JSON.stringify(value.data));
        }
        return result.next(value);
      });

      return result;
  }

  isUserLoggedIn() : boolean {
    if (localStorage.getItem('userDetails')) {
      return true;
    } 
    else {
      return false;
    }
  }

  getRole() : string {
    let userDetails = localStorage.getItem('userDetails')
    if (userDetails) {
      return JSON.parse(userDetails).roles[0];
    } 
    else {
      return "";
    }
  }

  getUserDetails(): UserModel | null {
    let userDetails = localStorage.getItem('userDetails')
    if (userDetails) {
      return JSON.parse(userDetails);
    }

    return null;
  }

  logout() {
    localStorage.removeItem('userDetails');
  }

  getUserToken() : string {
    var userDetails = localStorage.getItem('userDetails');
    
    if (userDetails) {
      return JSON.parse(userDetails).token;
    }

    return "";
  }

  getUserName() : string {
    var userDetails = localStorage.getItem('userDetails');
    
    if (userDetails) {
      return `${JSON.parse(userDetails).firstName} ${JSON.parse(userDetails).lastName}`;
    }

    return "";
  }

}
