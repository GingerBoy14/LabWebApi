import { Component, OnInit } from '@angular/core';

import { AuthenticationService } from 'src/app/core/services/Authentication.service';
import { EventEmitterService } from 'src/app/core/services/EventEmitter.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  title = 'TestApp';
  isAuthenticated: boolean = false;
  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private eventEmitterService: EventEmitterService
  ) {
    if (this.eventEmitterService.subsVar == undefined) {
      this.eventEmitterService.subsVar = this.eventEmitterService.invokeComponentFunction.subscribe(
        () => {
          this.ngOnInit();
        }
      );
    }
  }
  async ngOnInit() {
    if (await this.authService.isAuthenticatedWithRefreshToken()) {
      this.isAuthenticated = true;
    }
  }
  logout() {
    this.authService.logout().subscribe(() => {
      this.isAuthenticated = false;
      this.router.navigate(['login']);
    });
  }
}
