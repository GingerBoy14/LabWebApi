import { CanActivate, Router } from '@angular/router';

import { AuthenticationService } from '../services/Authentication.service';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AdminRoleGuard implements CanActivate {
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) {}
  canActivate(): boolean {
    if (this.authenticationService.currentUser.role == 'Admin') {
      return true;
    }
    this.router.navigate(['home']);
    return false;
  }
}
