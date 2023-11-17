/* Service that utilizes an interceptor to display a loading animation between pages*/

import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy() { //turns on spinner
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'line-scale-party',
      bdColor: 'rgba(255, 255, 255, 0)', //background color
      color: '#333333', //spinner color
    })
  }

  idle() { //turns off spinner
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerService.hide(); //hide the spinner
    }
  }
}
