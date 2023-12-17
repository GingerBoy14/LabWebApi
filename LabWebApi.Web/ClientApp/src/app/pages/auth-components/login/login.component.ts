import { Component, OnInit } from '@angular/core';

import { AlertService } from './../../../core/services/Alert.service';
import { AuthenticationService } from './../../../core/services/Authentication.service';
import { EventEmitterService } from 'src/app/core/services/EventEmitter.service';
import { FormGroup } from '@angular/forms';
import { InputValidationService } from 'src/app/core/services/InputValidation.service';
import { Router } from '@angular/router';
import { RxFormBuilder } from '@rxweb/reactive-form-validators';
import { UserLogin } from '../../../core/models/auth/UserLogin';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  formGroup: FormGroup;
  constructor(
    private formBuilder: RxFormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private eventEmitterService: EventEmitterService,
    private alertService: AlertService,
    public validationService: InputValidationService
  ) {}
   ngOnInit() {
     let userLogin = new UserLogin();
     this.formGroup = this.formBuilder.formGroup(userLogin);
   }
  async login() {
    this.authenticationService.login(this.formGroup.value).subscribe(() => {
      this.alertService.successAlert('Successful', 'Sign In');
      this.router.navigate(['user-home']);
      this.eventEmitterService.onComponentInvoke();
    });
  }
  getControl(controlName: string): any {
    return this.formGroup.get(controlName);
  }
}
