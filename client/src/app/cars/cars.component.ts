import { Component, Injector } from '@angular/core';
import { CarsGenerated } from './cars-generated.component';

@Component({
  selector: 'page-cars',
  templateUrl: './cars.component.html'
})
export class CarsComponent extends CarsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
