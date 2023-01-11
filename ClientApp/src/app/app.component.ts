import { Component } from '@angular/core';

/** PrimeNg */
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public title = 'SignalRClient';

  constructor(
    private primengConfig: PrimeNGConfig,
  ) { }

  ngOnInit(): void {
    this.activateRipple();
  }

  private activateRipple(): void {
    this.primengConfig.ripple = true;
  }
}
