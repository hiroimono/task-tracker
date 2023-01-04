import { Component } from '@angular/core';
import { SuccessesHubService } from './core/services/successes-hub.service';

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
    private _successHub: SuccessesHubService,
    private primengConfig: PrimeNGConfig
  ) { }

  ngOnInit(): void {
    this._successHub.startConnection();
    this._successHub.listenSuccesses();

    this.activateRipple();
  }

  private activateRipple(): void {
    this.primengConfig.ripple = true;
  }
}
