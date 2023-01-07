import { Success } from 'src/app/core/models/success.model';
import { Component } from '@angular/core';

/** Services */
import { SuccessesHubService } from 'src/app/core/services/successes-hub.service';
import { Subject, Subscription, takeUntil } from 'rxjs';

@Component({
  selector: 'app-workshop-component',
  templateUrl: './workshop.component.html'
})
export class WorkshopComponent {
  private compDestroyed$: Subject<boolean> = new Subject();
  public successes: Success[] = [];
  public subs!: Subscription;
  private count = 0;

  constructor(
    private _successesHub: SuccessesHubService
  ) {
  }

  ngOnInit() {
    this.successes = []
    this._successesHub.listenSuccesses()

    this.subs = this._successesHub.successes
      .pipe(takeUntil(this.compDestroyed$))
      .subscribe(
        successes => {
          this.count++
          console.log('this.count: ', this.count);
          this.successes = [...this.successes, ...successes]
        }
      )
  }

  ngOnDestroy() {
    this.compDestroyed$.next(true)
    this.compDestroyed$.complete()
    this._successesHub.stopListenningSuccesses()
    this.count = 0
    console.log('this.count: ', this.count)
  }
}
