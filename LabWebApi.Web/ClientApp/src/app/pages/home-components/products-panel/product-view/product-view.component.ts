import { ProductService } from './../../../../core/services/Product.service';
import { ProductInfo } from './../../../../core/models/products/ProductInfo';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
@Component({
    selector: 'product-view',
    templateUrl: './product-view.component.html',
    styleUrls: ['./product-view.component.css'],
  })
  export class ProductsViewComponent implements OnInit {
    product: ProductInfo;
    constructor(
        private route: ActivatedRoute,
        private productService: ProductService,
    ) {}
    async ngOnInit() {
        const id = this.route.snapshot.paramMap.get('id')
        if(id){
            this.productService.getProductById(id).subscribe((data: ProductInfo) => {
                this.product = data;
              });
        }
        
    }
}