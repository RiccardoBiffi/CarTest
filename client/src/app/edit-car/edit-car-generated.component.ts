/*
  This file is automatically generated. Any changes will be overwritten.
  Modify edit-car.component.ts instead.
*/
import { LOCALE_ID, ChangeDetectorRef, ViewChild, AfterViewInit, Injector, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { DialogService, DIALOG_PARAMETERS, DialogRef } from '@radzen/angular/dist/dialog';
import { NotificationService } from '@radzen/angular/dist/notification';
import { ContentComponent } from '@radzen/angular/dist/content';
import { LabelComponent } from '@radzen/angular/dist/label';
import { ButtonComponent } from '@radzen/angular/dist/button';
import { FormComponent } from '@radzen/angular/dist/form';

import { ConfigService } from '../config.service';

import { RadzenRentacarService } from '../radzen-rentacar.service';
import { SecurityService } from '../security.service';

export class EditCarGenerated implements AfterViewInit, OnInit, OnDestroy {
  // Components
  @ViewChild('content1') content1: ContentComponent;
  @ViewChild('closeLabel') closeLabel: LabelComponent;
  @ViewChild('closeButton') closeButton: ButtonComponent;
  @ViewChild('form0') form0: FormComponent;

  router: Router;

  cd: ChangeDetectorRef;

  route: ActivatedRoute;

  notificationService: NotificationService;

  configService: ConfigService;

  dialogService: DialogService;

  dialogRef: DialogRef;

  httpClient: HttpClient;

  locale: string;

  _location: Location;

  _subscription: Subscription;

  radzenRentacar: RadzenRentacarService;

  security: SecurityService;
  canEdit: any;
  car: any;
  getCategoriesResult: any;
  parameters: any;

  constructor(private injector: Injector) {
  }

  ngOnInit() {
    this.notificationService = this.injector.get(NotificationService);

    this.configService = this.injector.get(ConfigService);

    this.dialogService = this.injector.get(DialogService);

    this.dialogRef = this.injector.get(DialogRef, null);

    this.locale = this.injector.get(LOCALE_ID);

    this.router = this.injector.get(Router);

    this.cd = this.injector.get(ChangeDetectorRef);

    this._location = this.injector.get(Location);

    this.route = this.injector.get(ActivatedRoute);

    this.httpClient = this.injector.get(HttpClient);

    this.radzenRentacar = this.injector.get(RadzenRentacarService);
    this.security = this.injector.get(SecurityService);
  }

  ngAfterViewInit() {
    this._subscription = this.route.params.subscribe(parameters => {
      if (this.dialogRef) {
        this.parameters = this.injector.get(DIALOG_PARAMETERS);
      } else {
        this.parameters = parameters;
      }
      this.load();
      this.cd.detectChanges();
    });
  }

  ngOnDestroy() {
    if (this._subscription) {
      this._subscription.unsubscribe();
    }
  }


  load() {
    this.canEdit = true;

    this.radzenRentacar.getCarById(this.parameters.Id)
    .subscribe((result: any) => {
      this.car = result;
    }, (result: any) => {
      this.canEdit = !(result.status == 400);
    });

    this.radzenRentacar.getCategories(null, null, null, null, null, null, null, null)
    .subscribe((result: any) => {
      this.getCategoriesResult = result.value;
    }, (result: any) => {

    });
  }

  closeButtonClick(event: any) {
    if (this.dialogRef) {
      this.dialogRef.close();
    } else {
      this._location.back();
    }
  }

  form0Cancel(event: any) {
    if (this.dialogRef) {
      this.dialogRef.close();
    } else {
      this._location.back();
    }
  }

  form0Submit(event: any) {
    this.radzenRentacar.updateCar(this.parameters.Id, event)
    .subscribe((result: any) => {
      if (this.dialogRef) {
        this.dialogRef.close();
      } else {
        this._location.back();
      }
    }, (result: any) => {
      this.canEdit = !(result.status == 400);

      this.notificationService.notify({ severity: "error", summary: `Error`, detail: `Unable to update Car` });
    });
  }
}