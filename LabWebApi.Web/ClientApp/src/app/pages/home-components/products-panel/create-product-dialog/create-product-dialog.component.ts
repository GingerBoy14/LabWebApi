import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { AuthorizationRoles } from 'src/app/configs/auth-roles';
import { FormGroup } from '@angular/forms';
import { InputValidationService } from 'src/app/core/services/InputValidation.service';
import { RxFormBuilder } from '@rxweb/reactive-form-validators';
import { SimpleProductInfo } from './../../../../core/models/products/SimpleProductInfo';

@Component({
  selector: 'app-create-product-dialog',
  templateUrl: './create-product-dialog.component.html',
  styleUrls: ['./create-product-dialog.component.css'],
})
export class CreateProductDialogComponent implements OnInit {
  formGroup: FormGroup;
  constructor(
    private dialogRef: MatDialogRef<CreateProductDialogComponent>,
    public validationService: InputValidationService,
    private formBuilder: RxFormBuilder
  ) {}

  ngOnInit() {
    this.formGroup = this.formBuilder.formGroup( new SimpleProductInfo());
  }
  onCancelClick(): void {
    this.dialogRef.close();
  }
  getControl(controlName: string): any {
    return this.formGroup.get(controlName);
  }
  createProduct() {
    this.dialogRef.close(this.formGroup.value);
  }
}
