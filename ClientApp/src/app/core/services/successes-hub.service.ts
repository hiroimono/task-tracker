import { Injectable, isDevMode } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

/** Services */
import { DataStoreService } from './data-store.service';
import { Success } from '../models/success.model';
import { DataStore } from '../models/store.model';

@Injectable({
  providedIn: 'root'
})
export class SuccessesHubService {

  private hubConnection!: signalR.HubConnection

  constructor(
    private _store: DataStoreService
  ) {
    this.startHubConnection()
  }

  public startHubConnection = (): void => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.baseUrl}/successes`)
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Successes Connection started.......!'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public listenSuccesses() {
    this.hubConnection.on('SendSuccessesToUser', (coming: Success[]) => {
      console.log('coming successes: ', coming);

      const newStoreValue: DataStore = {
        successes: coming
      } as DataStore

      this._store.updateStore(newStoreValue, 'successes')
    });
  }

  public stopListenningSuccesses() {
    this.hubConnection.off('SendSuccessesToUser')
  }
}
