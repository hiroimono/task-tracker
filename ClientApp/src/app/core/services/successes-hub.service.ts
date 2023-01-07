import { Injectable, isDevMode } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { Success } from '../models/success.model';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SuccessesHubService {

  private hubConnection!: signalR.HubConnection;

  /** stored variables */
  public successes = new Observable<Success[]>();
  private _successes = new BehaviorSubject<Success[]>([]);

  private dataStore: {
    successes: Success[];
  };

  constructor() {
    this.dataStore = {
      successes: []
    }
    this.successes = this._successes.asObservable();
    this.startConnection();
  }

  public startConnection = (): void => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.hubUrl}`)
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started.......!'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public listenSuccesses() {
    this.hubConnection.on('SendSuccessesToUser', (coming: Success[]) => {
      console.log('coming successes: ', coming);
      this.dataStore.successes = [...coming];
      this._successes.next([...this.dataStore.successes]);
    });
  }

  public stopListenningSuccesses() {
    this.hubConnection.off('SendSuccessesToUser')
  }
}
