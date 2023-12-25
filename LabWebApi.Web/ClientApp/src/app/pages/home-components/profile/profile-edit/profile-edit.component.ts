import { UserProfile } from './../../../../core/models/admin/UserProfile';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import {  FormGroup } from '@angular/forms';

import { AlertService } from './../../../../core/services/Alert.service';
import { AuthenticationService } from 'src/app/core/services/Authentication.service';
import { DatePipe } from '@angular/common';
import { InputValidationService } from './../../../../core/services/InputValidation.service';
import { Router } from '@angular/router';
import { RxFormBuilder } from '@rxweb/reactive-form-validators';
import { UserInfo } from './../../../../core/models/admin/UserInfo';
import { UserProfileService } from './../../../../core/services/UserProfile.service';

@Component({
  selector: 'profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css'],
  providers: [DatePipe],
})
export class ProfileEditComponent implements OnInit {
  formGroup: FormGroup;
  user: UserProfile;
  @Output()
  onEdit=new EventEmitter<UserProfile>()
  constructor(
    private userProfileService: UserProfileService,
    private alertService: AlertService,
    private authService: AuthenticationService,
    private router: Router,
    public validationService: InputValidationService,
    private formBuilder: RxFormBuilder,
    private datePipe: DatePipe
  ) {}
  ngOnInit() {
    this.formGroup = this.formBuilder.formGroup(new UserProfile());

  
    this.userProfileService.getUserInfo().subscribe((data: UserProfile) => {
      this.user = data;
      const formData = {
        ...data,
        birthday: this.datePipe.transform(data.birthday, 'yyyy-MM-dd'),
      };

      console.log(formData)
      this.formGroup.patchValue(formData);
    });
  }
  getControl(controlName: string): any {
    return this.formGroup.get(controlName);
  }

  async editUser() {
    this.userProfileService
      .editUser(this.formGroup.value)
      .subscribe((data: any) => {
        this.alertService.successAlert('Successful', 'Data saved');
        this.user = { ...this.user, ...data };
        this.onEdit.emit(this.user)
      });
  }
  async deleteUser() {
    const confirmed = await this.alertService.okCancalAlert(
      `Do you really want to delete your profile?`
    );
    if (confirmed) {
      this.userProfileService.deleteUser().subscribe(() => {
        this.authService.logout().subscribe(() => {
         
          this.router.navigate(['login']);
        });
      });
    }
  }

}
