import { Component } from '@angular/core';

/** PrimeNg */
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {

  constructor(
    private primengConfig: PrimeNGConfig
  ) {
    this.primengConfig.ripple = true;
  }

  ngOnInit(): void { }
}
