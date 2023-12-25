import { CommentInfo } from './../models/comment/CommentDTO';
import { productCommentsUrl } from './../../configs/productController-endpoints';
import { Observable, of } from 'rxjs';

import { AlertService } from './Alert.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductInfo } from '../models/products/ProductInfo';
import { SimpleProductInfo } from './../models/products/SimpleProductInfo';
import { catchError } from 'rxjs/operators';
import {productsUrl,productsBaseUrl} from 'src/app/configs/productController-endpoints';

@Injectable({ providedIn: 'root' })
export class ProductService {
  constructor(private http: HttpClient, private alertService: AlertService) {}
  getProducts(): Observable<ProductInfo[]> {
    return this.http.get<ProductInfo[]>(productsUrl).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err.error, 'Get Users Failed!');
        return of<ProductInfo[]>();
      })
    ); 
  }
  deleteProduct(productId: string): Observable<any> {
    return this.http.delete<any>(productsUrl + `/${productId}`).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err.error, 'Delete Product Failed!');
        return of();
      })
    );
  }
  editProduct(product: SimpleProductInfo): Observable<ProductInfo> {

    return this.http.put<ProductInfo>(productsUrl, product).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err, 'Edit Product Failed!');
        return of<ProductInfo>();
      })
    );
  }
  createProduct(product: SimpleProductInfo): Observable<null> {
    return this.http.post<null>(productsBaseUrl, product).pipe(
      catchError((err) => {
        console.log(err())
        this.alertService.errorAlert(err, 'Create Product Failed!');
        return of<null>();
      })
    );
  }
  getProductById(productId: string): Observable<ProductInfo> {
    return this.http.get<ProductInfo>(productsUrl + `/${productId}`).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err.error, 'Get Product Failed!');
        return of<ProductInfo>();
      })
    );
  }
  getProductComments(productId: string): Observable<CommentInfo[]> {
    return this.http.get<CommentInfo[]>(productCommentsUrl(productId)).pipe(
      catchError((err) => {
        this.alertService.errorAlert(err.error, 'Get Product Comments Failed!');
        return of<CommentInfo[]>();
      })
    );
  }
}
