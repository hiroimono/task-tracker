import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';

/** Services */
import { DataStoreService } from './data-store.service';
import { DataStore } from '../models/store.model';

/** Models */
import { User } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class UsersHubService {

  private hubConnection!: signalR.HubConnection

  constructor(
    private _store: DataStoreService
  ) {
    this.startHubConnection()
  }

  public startHubConnection = (): void => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.baseUrl}/users`)
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('User Connection started.......!'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public listenUsers() {
    this.hubConnection.on('SendUsersToUser', (coming: User[]) => {
      console.log('coming users: ', coming);

      const newStoreValue: DataStore = {
        users: coming
      } as DataStore

      this._store.updateStore(newStoreValue, 'users')
    });
  }

  public stopListenningUsers() {
    this.hubConnection.off('SendUsersToUser')
  }
}

