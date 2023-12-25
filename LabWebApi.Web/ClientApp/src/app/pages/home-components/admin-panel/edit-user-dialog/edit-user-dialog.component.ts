import { RxFormBuilder } from '@rxweb/reactive-form-validators';
import { FormGroup } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserInfo } from 'src/app/core/models/admin/UserInfo';
import { InputValidationService } from 'src/app/core/services/InputValidation.service';
import { AuthorizationRoles } from 'src/app/configs/auth-roles';
import { DatePipe } from "@angular/common";

@Component({
  selector: 'app-edit-user-dialog',
  templateUrl: './edit-user-dialog.component.html',
  styleUrls: ['./edit-user-dialog.component.css'],
  providers: [DatePipe]
})
export class EditUserDialogComponent implements OnInit {
  formGroup: FormGroup;
  constructor(
    private dialogRef: MatDialogRef<EditUserDialogComponent>,
    public validationService: InputValidationService,
    @Inject(MAT_DIALOG_DATA) public data: UserInfo,
    private formBuilder: RxFormBuilder,
    private datePipe: DatePipe

  ) {}
  get userRoles(): typeof AuthorizationRoles {
    return AuthorizationRoles;
  }
  ngOnInit() {
    this.formGroup = this.formBuilder.formGroup(new UserInfo());
  const formData = {...this.data, birthday: this.datePipe.transform(this.data.birthday, 'yyyy-MM-dd')}

    this.formGroup.patchValue(formData);
  }
  onCancelClick(): void {
    this.dialogRef.close();
  }
  getControl(controlName: string): any {
    return this.formGroup.get(controlName);
  }
  editUser() {
    this.dialogRef.close(this.formGroup.value);
  }
}
