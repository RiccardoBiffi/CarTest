import { Component, Injector } from '@angular/core';
import { FindACarGenerated } from './find-a-car-generated.component';

@Component({
  selector: 'page-find-a-car',
  templateUrl: './find-a-car.component.html'
})
export class FindACarComponent extends FindACarGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
