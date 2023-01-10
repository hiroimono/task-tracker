import { Injectable } from '@angular/core';

/** RxJs */
import { BehaviorSubject, Observable } from 'rxjs';

/** Models */
import { DataStore } from '../models/store.model';
import { Success } from '../models/success.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class DataStoreService {

  /** Local Store Object in this service */
  private __dataStore: DataStore = {} as DataStore

  /** stored variables */
  public dataStore = new Observable<DataStore>()
  private _dataStore = new BehaviorSubject<DataStore>({} as DataStore)

  public successes = new Observable<Success[]>()
  private _successes = new BehaviorSubject<Success[]>([])

  public users = new Observable<User[]>()
  private _users = new BehaviorSubject<User[]>([])

  constructor() {
    this.dataStore = this._dataStore.asObservable()
    this.successes = this._successes.asObservable()
    this.users = this._users.asObservable()

    this.initiateStore()
  }

  public initiateStore() {
    const initial: DataStore = {
      successes: [],
      users: []
    }

    this.updateStore(initial)
  }

  public updateStore(newStoreValue: { [key: string]: any }, ...props: string[]) {
    if (props.length) {
      const prev = JSON.stringify(this.__dataStore)

      for (const key in newStoreValue) {
        if (Object.prototype.hasOwnProperty.call(newStoreValue, key)) {

          props.forEach(prop => {

            if (prop === key) {
              const copy = JSON.parse(JSON.stringify(this.__dataStore))
              copy[key] = newStoreValue[key]
              const next = JSON.stringify(copy)

              if (prev !== next) {
                this.__dataStore = copy
                this._dataStore.next(this.__dataStore)
                this._successes.next(this.__dataStore.successes)
                this._users.next(this.__dataStore.users)
              } else console.log('__NO_CHANGE__1')
            }

          })
        }
      }
    } else {
      const prev = JSON.stringify(this.__dataStore)
      const next = JSON.stringify(newStoreValue)

      if (prev !== next) {
        this.__dataStore = newStoreValue as DataStore
        this._dataStore.next(this.__dataStore)
        this._successes.next(this.__dataStore.successes)
        this._users.next(this.__dataStore.users)
      } else console.log('__NO_CHANGE__')
    }
  }
}
