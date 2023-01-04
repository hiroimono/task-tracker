import { Success } from 'src/app/core/models/success.model';
import { Component } from '@angular/core';

/** Services */
import { SuccessesHubService } from 'src/app/core/services/successes-hub.service';

@Component({
  selector: 'app-workshop-component',
  templateUrl: './workshop.component.html'
})
export class WorkshopComponent {
  public successes: Success[] = [];
  public testSuccess: Success = {
    "id": 3,
    "taskId": 1,
    "task": {
      "id": 1,
      "taskName": "Add Controllers",
      "duration": 60,
      "isActiv": false,
      "isBreak": false
    },
    "userId": 3,
    "user": {
      "id": 3,
      "isAdmin": false,
      "nickname": "Niky",
      "avatar": "https://robohash.org/nicky?set=set2"
    },
    "isDone": true
  }

  constructor(private _successesHub: SuccessesHubService) { }

  ngOnInit() {
    this._successesHub.successes.subscribe(
      successes => this.successes = successes
    );
  }
}
