import { Component } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';

/** Services */
import { UserService } from 'src/app/core/services/user.service';

/** PrimeNg */
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';

/** Models */
import { User } from 'src/app/core/models/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  public registerForm: FormGroup;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public fb: FormBuilder,
    private _user: UserService
  ) {
    this.registerForm = this.initiateForm()
  }

  // this.ref.close(car);
  // id: this.config.id

  ngOnInit() {
    this.registerForm.valueChanges.subscribe(value => console.log('value: ', value))
  }

  public initiateForm(): FormGroup {
    return this.fb.group({
      id: 0,
      isAdmin: false,
      nickname: ['', Validators.required],
      avatar: ''
    })
  }

  public onSubmit() {
    const userFormValue: User = Object.assign({}, this.registerForm.value);
    console.log('userFormValue: ', userFormValue);

    this._user.addUser(userFormValue)
      .subscribe(savedUser => console.log('savedUser: ', savedUser))
  }

  public setAvatar(event: any) {
    console.log('event: ', event);
    if (event) {
      this.registerForm.patchValue({
        avatar: 'https://robohash.org/' + event + '?set=set3'
      })

      console.log('this.registerForm: ', this.registerForm.value);
    }
  }
}
