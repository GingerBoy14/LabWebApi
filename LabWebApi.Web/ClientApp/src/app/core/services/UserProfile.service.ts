import { ProfilePassword } from './../models/profile/profilePassword';
import { password } from '@rxweb/reactive-form-validators';
import { Observable, of } from 'rxjs';
import {
  uploadUserAvatarUrl,
  userProfileUrl,
  userProfileChangePasswordUrl,
} from './../../configs/userController-endpoints';

import { AlertService } from './Alert.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserProfile } from './../models/admin/UserProfile';
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class UserProfileService {
  constructor(private http: HttpClient, private alertService: AlertService) {}
  getUserInfo(): Observable<any> {
    return this.http.get<any>(userProfileUrl);
  }
  getUserImage(): Observable<File> {
    const options = { responseType: 'Blob' as 'json' };
    return this.http.get<File>(uploadUserAvatarUrl, options);
  }
  updateUserImage(image: File): Observable<void> {
    const formData = new FormData();
    formData.append('image', image, image.name);
    return this.http.post<void>(uploadUserAvatarUrl, formData).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err.error, 'Failed!');
        return of<void>();
      })
    );
  }
  editUser(user: UserProfile): Observable<UserProfile> {
    return this.http.put<UserProfile>(userProfileUrl, user).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err, 'Edit User Failed!');
        return of<UserProfile>();
      })
    );
  }
  deleteUser(): Observable<any> {
    return this.http.delete<any>(userProfileUrl).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err.error, 'Delete User Failed!');
        return of();
      })
    );
  }
  changePassword(password: ProfilePassword): Observable<any> {
    return this.http.post<any>(userProfileChangePasswordUrl, password).pipe(
      catchError((err) => {

        this.alertService.errorAlert(err().error.error, 'Change password Failed!');
        return of();
      })
    );
  }
}
