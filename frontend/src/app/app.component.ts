import { Component } from '@angular/core';
import { Customer } from './models/Customer';
import { Order } from './models/Order';
import { Product } from './models/Product';
import { OrdersService } from './services/order.api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = '[DipIT] Supply Chain App';
  orders: Order[] = [];
  customers: Customer[] = [];
  products: Product[] = [];
  newOrder!: Order;
  selectedCustomer!: Customer;
  selectedProduct!: Product;
  selectedQty: number = 0;
  selectedShipMode!: string;
  selectedOrderToDelete!: Order;
  selectedOrderToUpdate!: Order;
  updatedOrder!: Order;
  loaded: boolean = false;

  constructor(public _orderService: OrdersService) {
    this.onInit();
  }

  onInit() {
    this._orderService.GetOrders().subscribe(
      (orderList) => (this.orders = orderList),
      null,
      () => {
        this._orderService.GetCustomers().subscribe(
          (custList) => (this.customers = custList),
          null,
          () => {
            this._orderService.GetProducts().subscribe(
              (prodList) => (this.products = prodList),
              null,
              () => {
                this.loaded = true;
                this.selectedOrderToUpdate = this.orders[0];
                this.updatedOrder = { ...this.selectedOrderToUpdate };
              }
            );
          }
        );
      }
    );
  }

  createOrder() {
    this.newOrder = {
      custID: this.selectedCustomer.custID,
      prod: this.selectedProduct,
      orderDate: new Date().toString(),
      shipDate: '',
      quantity: this.selectedQty,
      shipMode: this.selectedShipMode,
    };
    this._orderService.newOrder(this.newOrder).subscribe(null, null, () => {
      alert('Order Created');
      this.onInit();
    });
  }

  updateOrder() {
    this._orderService
      .deleteOrder(this.selectedOrderToUpdate)
      .subscribe(null, null, () => {
        this._orderService
          .newOrder(this.updatedOrder)
          .subscribe(null, null, () => {
            alert('Order Updated');
            this.onInit();
          });
      });
  }

  changedUpdate() {
    this.updatedOrder = { ...this.selectedOrderToUpdate };
  }

  deleteOrder() {
    this._orderService
      .deleteOrder(this.selectedOrderToDelete)
      .subscribe(null, null, () => {
        alert('Order Deleted');
        this.onInit();
      });
  }


}
