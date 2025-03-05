import { Todo } from "../../main/model/todo";
import { Tag } from "../../tag/model/tag";
import { TodoTag } from "./todoTag";

export class TodoTagList extends TodoTag{
    tag:Tag=new Tag();
    todo:Todo=new Todo();
}