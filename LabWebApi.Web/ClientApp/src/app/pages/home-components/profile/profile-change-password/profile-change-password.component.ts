import { ProfilePassword } from './../../../../core/models/profile/profilePassword';
import { Component, OnInit } from '@angular/core';
import {  FormGroup } from '@angular/forms';

import { AlertService } from './../../../../core/services/Alert.service';

import { InputValidationService } from './../../../../core/services/InputValidation.service';

import { RxFormBuilder, ReactiveFormConfig } from '@rxweb/reactive-form-validators';
import { UserProfileService } from './../../../../core/services/UserProfile.service';

@Component({
  selector: 'profile-password-change',
  templateUrl: './profile-change-password.component.html',
  styleUrls: ['./profile-change-password.component.css']
})
export class ProfileChangePasswordComponent implements OnInit {
  formGroup: FormGroup;
  newPasswordHide = true;
  currentPasswordHide = true;
  constructor(
    private userProfileService: UserProfileService,
    private alertService: AlertService,
    public validationService: InputValidationService,
    private formBuilder: RxFormBuilder,
  ) {}
  ngOnInit() {
    ReactiveFormConfig.set({"validationMessage":{"different":"Password should be different!"}});
    this.formGroup = this.formBuilder.formGroup(new ProfilePassword());
  }
 
  getControl(controlName: string): any {
    return this.formGroup.get(controlName);
  }

async changePassword(){
  this.userProfileService.changePassword(this.formGroup.value).subscribe(() => {
    this.alertService.successAlert('Successful', 'Changed password');
  });
}
}
