import { Router } from '@angular/router';
import { EditProductDialogComponent } from './../edit-product-dialog/edit-product-dialog.component';
import { ProductInfo } from './../../../../core/models/products/ProductInfo';
import { SimpleProductInfo } from './../../../../core/models/products/SimpleProductInfo';
import { CreateProductDialogComponent } from './../create-product-dialog/create-product-dialog.component';
import { Component, OnInit, EventEmitter } from '@angular/core';

import { AlertService } from '../../../../core/services/Alert.service';
import { AuthenticationService } from './../../../../core/services/Authentication.service';
import { AuthorizationRoles } from './../../../../configs/auth-roles';
import { MatDialog } from '@angular/material/dialog';
import { ProductService } from './../../../../core/services/Product.service';
import { } from '@swimlane/ngx-datatable'
@Component({
  selector: 'products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductsListComponent implements OnInit {
  products: ProductInfo[];
  filteredProduct: ProductInfo[];
  searchQuery: string;
  constructor(
    private productService: ProductService,
    private alertService: AlertService,
    private dialog: MatDialog,
    private router: Router,
    private authService: AuthenticationService
  ) {}
  async ngOnInit() {
    this.productService.getProducts().subscribe((data: ProductInfo[]) => {
      this.products = data;
    });
  }
  onRowClick(data:any){
    if(data.type==='click'){
      this.router.navigate(['products/'+data.row.id]);
    }
  }

  isCurrentUserAdmin(): boolean {
    return this.authService.currentUser.role == AuthorizationRoles.Admin;
  }
  isPossibleToEdit(product: ProductInfo): boolean {
    return (
      this.isCurrentUserAdmin() ||
      product.userWhoCreated.id === this.authService.currentUser.id
    );
  }

  onSearchByName() {
    if (!this.searchQuery) {
      this.filteredProduct = [];
    } else {
      this.filteredProduct = this.products.filter((product: ProductInfo) =>
        product.name
          .toLocaleLowerCase()
          .startsWith(this.searchQuery.toLocaleLowerCase())
      );
    }
  }

  async editProduct(product: ProductInfo) {
    console.log(product);
    const dialogRef = this.dialog.open(EditProductDialogComponent, {
      data: { ...product },
    });
    dialogRef.afterClosed().subscribe((result: SimpleProductInfo) => {
      if (result) {
        this.productService
          .editProduct({
            ...result,
            id: product.id,
            publicationDate: product.publicationDate,
          })
          .subscribe((data: any) => {
            let modelIndex = this.products.findIndex(
              (x: any) => x.id == data.id
            );
            if (modelIndex !== -1) {
              this.products.splice(modelIndex, 1, data);
              this.products = [...this.products];
            } else {
              this.alertService.errorAlert('Update Product', 'Failed!');
            }
          });
      }
    });
  }
  async deleteProduct(productId: any) {
    const confirmed = await this.alertService.okCancalAlert(
      `Do you really want to delete this product?`
    );
    if (confirmed) {
      this.productService.deleteProduct(productId).subscribe(() => {
        const newList = this.products.filter((item) => item.id != productId);
        this.products = newList;
      });
    }
  }

  async createProduct() {
    const dialogRef = this.dialog.open(CreateProductDialogComponent);
    dialogRef.afterClosed().subscribe((result: SimpleProductInfo) => {
      if (result) {
        this.productService
          .createProduct({ ...result, publicationDate: new Date() })
          .subscribe((data: any) => {
            this.products = [...this.products, data];
          });
      }
    });
  }
}
