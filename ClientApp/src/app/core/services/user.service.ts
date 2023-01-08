import { Inject, Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment'

/** Models */
import { User } from '../models/user.model'

/** RxJs */
import { Observable } from 'rxjs/internal/Observable'
import { map } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl: string

  constructor(
    private http: HttpClient,
  ) {
    this.baseUrl = environment.baseUrl
  }

  /** GET api/users */
  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/api/users`)
      .pipe(
        map((users: User[]) => users)
      )
  }

  /** POST: /api/user { user } */
  addUser(user: User): Observable<User[]> {
    return this.http.post<User[]>(`${this.baseUrl}/users`, user)
      .pipe(
        map((users: User[]) => users)
      )
  }
}
