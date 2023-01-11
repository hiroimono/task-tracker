import { User } from "./user.model";
import { Task } from "./task.model";

export interface Success {
  id?: number,
  taskId: number,
  task?: Task,
  userId: number,
  user?: User,
  isDone: boolean
}
