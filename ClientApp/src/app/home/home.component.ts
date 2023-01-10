import { Component } from '@angular/core';
import { RegisterComponent } from './../shared/components/register/register.component';

/** Models */
import { User } from '../core/models/user.model';

/** Services */
import { UserService } from 'src/app/core/services/user.service';

/** PrimeNg */
import { PrimeNGConfig } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [DialogService]
})
export class HomeComponent {

  constructor(
    private primengConfig: PrimeNGConfig,
    private _user: UserService,
    public dialogService: DialogService
  ) {
    this.primengConfig.ripple = true;
  }

  ngOnInit(): void {
    this._user.getAllUsers().subscribe(
      (users: User[]) => console.log('users: ', users)
    )
  }

  public showDialog() {
    const ref = this.dialogService.open(RegisterComponent, {
      // data: {
      //   id: '51gF3'
      // },
      width: '400px',
    });
  }
}
