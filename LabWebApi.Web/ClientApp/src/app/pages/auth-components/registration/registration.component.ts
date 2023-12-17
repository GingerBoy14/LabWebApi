import { Component, OnInit } from '@angular/core';

import { AlertService } from './../../../core/services/Alert.service';
import { AuthenticationService } from './../../../core/services/Authentication.service';
import { AuthorizationRoles } from './../../../configs/auth-roles';
import { EventEmitterService } from 'src/app/core/services/EventEmitter.service';
import { FormGroup } from '@angular/forms';
import { InputValidationService } from 'src/app/core/services/InputValidation.service';
import { Router } from '@angular/router';
import { RxFormBuilder } from '@rxweb/reactive-form-validators';
import { UserRegistration } from '../../../core/models/auth/UserRegistration';

interface Food {
  value: string;
  viewValue: string;
}
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})

export class RegistrationComponent implements OnInit {
  formGroup: FormGroup;
  roles=  Object.keys(AuthorizationRoles).filter((role)=>  !Number(role)).map(
    (role) => ({ viewValue: role, value: (AuthorizationRoles as any)[role] })
  );
  constructor(
    private formBuilder: RxFormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private eventEmitterService: EventEmitterService,
    private alertService: AlertService,
    public validationService: InputValidationService
  ) {}
   ngOnInit() {
     let userRegistration = new UserRegistration();
     this.formGroup = this.formBuilder.formGroup(userRegistration);
   }
  async register() {
    this.authenticationService.registration(this.formGroup.value).subscribe(() => {
      this.alertService.successAlert('Successful', 'Sign Up');
      this.router.navigate(['user-home']);
      this.eventEmitterService.onComponentInvoke();
    });
  }
  getControl(controlName: string): any {

    return this.formGroup.get(controlName);
  }
}
