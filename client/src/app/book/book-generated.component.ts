/*
  This file is automatically generated. Any changes will be overwritten.
  Modify book.component.ts instead.
*/
import { LOCALE_ID, ChangeDetectorRef, ViewChild, AfterViewInit, Injector, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Subscription } from 'rxjs';

import { DialogService, DIALOG_PARAMETERS, DialogRef } from '@radzen/angular/dist/dialog';
import { NotificationService } from '@radzen/angular/dist/notification';
import { ContentComponent } from '@radzen/angular/dist/content';
import { HeadingComponent } from '@radzen/angular/dist/heading';
import { LabelComponent } from '@radzen/angular/dist/label';
import { ButtonComponent } from '@radzen/angular/dist/button';
import { TextBoxComponent } from '@radzen/angular/dist/textbox';
import { FieldsetComponent } from '@radzen/angular/dist/fieldset';
import { HtmlComponent } from '@radzen/angular/dist/html';

import { ConfigService } from '../config.service';

import { RadzenRentacarService } from '../radzen-rentacar.service';
import { SecurityService } from '../security.service';

export class BookGenerated implements AfterViewInit, OnInit, OnDestroy {
  // Components
  @ViewChild('content1') content1: ContentComponent;
  @ViewChild('pageTitle') pageTitle: HeadingComponent;
  @ViewChild('label0') label0: LabelComponent;
  @ViewChild('label1') label1: LabelComponent;
  @ViewChild('label2') label2: LabelComponent;
  @ViewChild('label3') label3: LabelComponent;
  @ViewChild('button0') button0: ButtonComponent;
  @ViewChild('label4') label4: LabelComponent;
  @ViewChild('textbox0') textbox0: TextBoxComponent;
  @ViewChild('label5') label5: LabelComponent;
  @ViewChild('fieldset0') fieldset0: FieldsetComponent;
  @ViewChild('html0') html0: HtmlComponent;
  @ViewChild('html1') html1: HtmlComponent;

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
  start: any;
  end: any;
  days: any;
  Customer: any;
  Car: any;
  total: any;
  parameters: any;
  submit: any;

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
    this.start = new Date(this.parameters.start).toLocaleDateString();

    this.end = new Date(this.parameters.end).toLocaleDateString();

    this.days = Math.floor((Date.parse(this.parameters.end) - Date.parse(this.parameters.start)) / 86400000);

    this.Customer = "";

    this.radzenRentacar.getCars(`Id eq ${this.parameters.CarId}`, null, null, null, null, `Category`, null, null)
    .subscribe((result: any) => {
      this.Car = result.value[0];

      this.total = this.days * this.Car.Category.DailyRate;
    }, (result: any) => {

    });
  }

  button0Click(event: any) {
    this.submit = true;

    if (this.Customer != "") {
          this.radzenRentacar.createOrder(<any>{CarId: this.Car.Id, OrderDate: new Date(), StartDate: new Date(this.start), EndDate: new Date(this.end), Description: this.Customer + '/' + this.Car.Make + ' ' + this.Car.Model})
      .subscribe((result: any) => {
          if (this.dialogRef) {
        this.dialogRef.close();
      } else {
        this._location.back();
      }

      setTimeout(() => window.location.reload(true), 500);
      }, (result: any) => {
    
      });
    }
  }
}
