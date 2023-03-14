import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './radzen-rentacar.model';

@Injectable()
export class RadzenRentacarService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('radzenRentacar');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getAvailableCars(endDate: string | null, startDate: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/AvailableCarsFunc(EndDate='${encodeURIComponent(endDate).replace(/'/g, "''")}',StartDate='${encodeURIComponent(startDate).replace(/'/g, "''")}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCars(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Cars`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCar(car: models.Car | null) : Observable<any> {
    return this.odata.post(`/Cars`, car, {  }, ['Category']);
  }

  deleteCar(id: number | null) : Observable<any> {
    return this.odata.delete(`/Cars(${id})`, item => !(item.Id == id));
  }

  getCarById(id: number | null) : Observable<any> {
    return this.odata.getById(`/Cars(${id})`, {  });
  }

  updateCar(id: number | null, car: models.Car | null) : Observable<any> {
    return this.odata.patch(`/Cars(${id})`, car, item => item.Id == id, {  }, ['Category']);
  }

  getCategories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Categories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCategory(category: models.Category | null) : Observable<any> {
    return this.odata.post(`/Categories`, category, {  }, []);
  }

  deleteCategory(id: number | null) : Observable<any> {
    return this.odata.delete(`/Categories(${id})`, item => !(item.Id == id));
  }

  getCategoryById(id: number | null) : Observable<any> {
    return this.odata.getById(`/Categories(${id})`, {  });
  }

  updateCategory(id: number | null, category: models.Category | null) : Observable<any> {
    return this.odata.patch(`/Categories(${id})`, category, item => item.Id == id, {  }, []);
  }

  getOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Orders`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOrder(order: models.Order | null) : Observable<any> {
    return this.odata.post(`/Orders`, order, {  }, ['Car']);
  }

  deleteOrder(id: number | null) : Observable<any> {
    return this.odata.delete(`/Orders(${id})`, item => !(item.Id == id));
  }

  getOrderById(id: number | null) : Observable<any> {
    return this.odata.getById(`/Orders(${id})`, {  });
  }

  updateOrder(id: number | null, order: models.Order | null) : Observable<any> {
    return this.odata.patch(`/Orders(${id})`, order, item => item.Id == id, {  }, ['Car']);
  }
}
