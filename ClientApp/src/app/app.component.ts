import { Component } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

/** PrimeNg */
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private hubConnectionBuilder!: HubConnection;
  public title = 'SignalRClient';
  public successes: boolean[] = [];

  constructor(
    private primengConfig: PrimeNGConfig
  ) { }

  ngOnInit(): void {
    this.initiateHub();
    this.activateRipple();
  }

  private initiateHub(): void {
    this.hubConnectionBuilder = new HubConnectionBuilder()
      .withUrl('https://localhost:7275/successes')
      .configureLogging(LogLevel.Information).build();

    this.hubConnectionBuilder.start()
      .then(() => console.log('Connection started.......!'))
      .catch(err => console.log('Error while connect with server'));

    this.hubConnectionBuilder.on('SendSuccessesToUser', (result: boolean[]) => {
      console.log('result: ', result);
      this.successes = [...this.successes, ...result];
    });
  }

  private activateRipple(): void {
    this.primengConfig.ripple = true;
  }
}
