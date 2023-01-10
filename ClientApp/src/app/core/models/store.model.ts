import { Success } from "./success.model";
import { User } from "./user.model";

export interface DataStore {
  successes: Success[];
  users: User[];
}
