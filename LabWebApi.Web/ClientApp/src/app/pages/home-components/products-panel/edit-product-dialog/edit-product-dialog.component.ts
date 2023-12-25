import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { FormGroup } from '@angular/forms';
import { InputValidationService } from 'src/app/core/services/InputValidation.service';
import { RxFormBuilder } from '@rxweb/reactive-form-validators';
import { SimpleProductInfo } from '../../../../core/models/products/SimpleProductInfo';

@Component({
  selector: 'app-edit-product-dialog',
  templateUrl: './edit-product-dialog.component.html',
  styleUrls: ['./edit-product-dialog.component.css'],
})
export class EditProductDialogComponent implements OnInit {
  formGroup: FormGroup;
  constructor(
    private dialogRef: MatDialogRef<EditProductDialogComponent>,
    public validationService: InputValidationService,
    @Inject(MAT_DIALOG_DATA) public data: SimpleProductInfo,
    private formBuilder: RxFormBuilder
  ) {}

  ngOnInit() {
    this.formGroup = this.formBuilder.formGroup( new SimpleProductInfo());
    this.formGroup.patchValue(this.data);
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
