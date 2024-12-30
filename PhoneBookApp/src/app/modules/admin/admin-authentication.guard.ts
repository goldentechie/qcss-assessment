import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthenticationGuard implements CanActivate, CanActivateChild {

  
  constructor(private authService: AuthenticationService, private router: Router) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let url = state.url;
    return this.authenticate(route, url);
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let url = state.url;
    return this.authenticate(childRoute, url);
  }

  private authenticate(route: ActivatedRouteSnapshot, url: any): boolean {
    

    if (this.authService.isUserLoggedIn()) {
      const userRole = this.authService.getRole();
      if (userRole == "Admin") {
        return true;
      }
      else {
        // will add navigation for other roles
      }
    }

    this.router.navigate(['/admin/']);
    return false;
  }
  
}
