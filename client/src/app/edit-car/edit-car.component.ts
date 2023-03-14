import { Component, Injector } from '@angular/core';
import { EditCarGenerated } from './edit-car-generated.component';

@Component({
  selector: 'page-edit-car',
  templateUrl: './edit-car.component.html'
})
export class EditCarComponent extends EditCarGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
