import { UserProfile } from './../../../../core/models/admin/UserProfile';
import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { FormGroup } from '@angular/forms';

import { AlertService } from './../../../../core/services/Alert.service';

import { InputValidationService } from './../../../../core/services/InputValidation.service';

import { UserInfo } from './../../../../core/models/admin/UserInfo';
import { UserProfileService } from './../../../../core/services/UserProfile.service';
import { environment } from './../../../../../environments/environment';

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  
})
export class ProfileComponent implements OnInit {
  changePasswordFormGroup: FormGroup;
  user: UserProfile;
  image: SafeUrl | null = null;
  defaultImage: string =
    'https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava1-bg.webp';
  file: File | null = null;
  constructor(
    private userProfileService: UserProfileService,
    private sanitizer: DomSanitizer,
    private alertService: AlertService,
    public validationService: InputValidationService
  ) {}
  ngOnInit() {
    this.userProfileService.getUserInfo().subscribe((data: UserProfile) => {
      this.user = data;
    });
    this.userProfileService.getUserImage().subscribe((data: Blob) => {
      const blob = new Blob([data], { type: data.type });
      const unsafeImg = URL.createObjectURL(blob);
      this.image = this.sanitizer.bypassSecurityTrustUrl(unsafeImg);
    });
  }
  getImageTypes(): string {
    let types: string = '';
    environment.imageSettings.imageTypes.forEach(function (x) {
      types += '.' + x + ',';
    });
    return types;
  }
  selectImage(event: any) {
    this.file = event.target.files[0] as File;
    if (this.file!.size > environment.imageSettings.maxSize * 1024 * 1024) {
      this.alertService.errorAlert(
        'Max size is ' + environment.imageSettings.maxSize + ' Mb',
        'Error'
      );
      this.file = null;
      return;
    }
    const unsafeImg = URL.createObjectURL(this.file);
    this.image = this.sanitizer.bypassSecurityTrustUrl(unsafeImg);
  }

  uploadImage() {
    if (this.file) {
      this.userProfileService.updateUserImage(this.file).subscribe(() => {
        this.file = null;
      });
    }
  }

  onUserEdit(user: UserProfile) {
    console.log(user)
    this.user = user;
  }
}
