import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from 'src/app/shared/models/brand';
import { IProduct } from 'src/app/shared/models/product';
import { IType } from 'src/app/shared/models/productType';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from 'src/app/shop/shop.service';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  @ViewChild('search', {static: true}) searchTerm: ElementRef;
  shopParams = new ShopParams();
  products: IProduct[];
  brands: IBrand[];
  types: IType[];  
  totalCount: number;
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
  }
  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber= response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
      console.log('nav-bar'+this.shopParams);
    }, error => {
      console.log(error);
    })
  }
  getBrands() {
    this.shopService.getBrands().subscribe(response => {
      // this.brands = response;
      this.brands = [{id: 0, name: 'All'}, ...response]
    }, error => {
      console.log(error);
    })
  }
  getTypes() {
    this.shopService.getTypes().subscribe(response => {
      // this.types = response;
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    })
  }
  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }
  onReset() {
    this.searchTerm.nativeElement.value = undefined;
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
