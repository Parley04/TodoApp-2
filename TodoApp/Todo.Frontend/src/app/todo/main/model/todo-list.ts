import { User } from "../../login/model/user";
import { Tag } from "../../tag/model/tag";
import { Todo } from "./todo";

export class TodoList extends Todo{
    tags:Tag[]=[];
    user:User=new User();
}