import { Component, Injector } from '@angular/core';
import { AboutGenerated } from './about-generated.component';

@Component({
  selector: 'page-about',
  templateUrl: './about.component.html'
})
export class AboutComponent extends AboutGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
