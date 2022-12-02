import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/Customer';
import { Order } from '../models/Order';
import { Product } from '../models/Product';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  readonly baseUrl: string = "http://huss.somee.com";

  constructor(private _http: HttpClient) { }

  GetOrders(): Observable<Order[]>{
    return this._http.get<Order[]>(this.baseUrl + "/order")
  }

  GetCustomers(): Observable<Customer[]>{
    return this._http.get<Customer[]>(this.baseUrl + "/customer")
  }

  GetProducts(): Observable<Product[]>{
    return this._http.get<Product[]>(this.baseUrl + "/product")
  }

  newOrder(order: Order): Observable<string> {
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(order);
    return this._http.put<string>(this.baseUrl + '/order', body, { 'headers': headers })
  }

  deleteOrder(order:Order):Observable<any>{
    const headers = { 'content-type': 'application/json' };
    const body = JSON.stringify(order);
    return this._http.delete<any>(this.baseUrl + '/order', { body: body, 'headers':headers })
  }
}