import { Success } from 'src/app/core/models/success.model'
import { Component } from '@angular/core'

/** Services */
import { DataStoreService } from 'src/app/core/services/data-store.service'
import { SuccessesHubService } from 'src/app/core/services/successes-hub.service'
import { UsersHubService } from 'src/app/core/services/users-hub.service'

/** RxJs */
import { Subject, Subscription, takeUntil } from 'rxjs'
import { User } from 'src/app/core/models/user.model'

@Component({
  selector: 'app-workshop-component',
  templateUrl: './workshop.component.html'
})
export class WorkshopComponent {
  private destroy$: Subject<boolean> = new Subject()

  public successes: Success[] = []
  public users: User[] = []

  private countSuccesses = 0
  private countUsers = 0

  constructor(
    private _store: DataStoreService,
    private _successesHub: SuccessesHubService,
    private _usersHub: UsersHubService
  ) { }

  ngOnInit() {
    this._successesHub.listenSuccesses()
    this._usersHub.listenUsers()

    this._store.successes
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        successes => {
          this.countSuccesses++
          console.log('this.countSuccess: ', this.countSuccesses);
          this.successes = [...successes]
          console.log('this.successes: ', this.successes);
        }
      )

    this._store.users
      .pipe(takeUntil(this.destroy$))
      .subscribe(
        users => {
          this.countUsers++
          console.log('this.countUser: ', this.countUsers);
          this.users = [...users]
          console.log('this.users: ', this.users);
        }
      )
  }

  ngOnDestroy() {
    this.destroy$.next(true)
    this.destroy$.complete()

    this._successesHub.stopListenningSuccesses()
    this._usersHub.stopListenningUsers()

    this.countSuccesses = 0
    console.log('this.countSuccesses: ', this.countSuccesses)

    this.countUsers = 0
    console.log('this.countUsers: ', this.countUsers);
  }
}
