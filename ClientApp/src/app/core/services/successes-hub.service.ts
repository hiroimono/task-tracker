import { Injectable, isDevMode } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';

/** Models */
import { Success } from '../models/success.model';
import { DataStore } from '../models/store.model';

@Injectable({
  providedIn: 'root'
})
export class SuccessesHubService {

  private hubConnection!: signalR.HubConnection

  /** Local Store Object in this service */
  private __dataStore: DataStore = {} as DataStore

  /** stored variables */
  public dataStore = new Observable<DataStore>()
  private _dataStore = new BehaviorSubject<DataStore>({} as DataStore)

  public successes = new Observable<Success[]>()
  private _successes = new BehaviorSubject<Success[]>([])

  constructor() {
    this.dataStore = this._dataStore.asObservable()
    this.successes = this._successes.asObservable()

    this.initiateStore()
    this.startHubConnection()
  }

  public initiateStore() {
    const initial: DataStore = {
      successes: []
    }

    this.updateStore(initial)
  }

  private updateStore(newStoreValue: DataStore) {
    const prev = JSON.stringify(this.__dataStore)
    const curr = JSON.stringify(newStoreValue)

    if (prev !== curr) {
      this.__dataStore = newStoreValue
      this._dataStore.next(this.__dataStore)
      this._successes.next(this.__dataStore.successes)
    } else console.log('__NO_CHANGE__')
  }

  public startHubConnection = (): void => {
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

      const newStoreValue: DataStore = {
        successes: coming
      }

      this.updateStore(newStoreValue)
    });
  }

  public stopListenningSuccesses() {
    this.hubConnection.off('SendSuccessesToUser')
  }
}
