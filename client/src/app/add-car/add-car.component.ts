import { Component, Injector } from '@angular/core';
import { AddCarGenerated } from './add-car-generated.component';

@Component({
  selector: 'page-add-car',
  templateUrl: './add-car.component.html'
})
export class AddCarComponent extends AddCarGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
