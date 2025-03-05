import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoList } from '../model/todo-list';
import { Todo } from '../model/todo';

@Injectable({
  providedIn: 'root'
})
export class MainService {

  constructor(
    @Inject("apiUrl") private apiUrl: string,
    private httpClient: HttpClient,
  ) { }

  getList(id: string):Observable<TodoList[]> {
    const params = new HttpParams().set('id', id);
    return this.httpClient.get<TodoList[]>(`${this.apiUrl}Todo/GetList`, { params });
  }
  getById(id: string):Observable<Todo> {
    const params = { id: id };
    return this.httpClient.get<Todo>(this.apiUrl+"Todo/GetById", { params });
  }
  add(todo:Todo):Observable<Todo> {
    return this.httpClient.post<Todo>(this.apiUrl+"Todo/Create", todo);
  }
  update(todo:Todo):Observable<Todo> {
    return this.httpClient.post<Todo>(this.apiUrl+"Todo/Update", todo);
  }
  delete(todo:Todo):Observable<Todo> {
    return this.httpClient.post<Todo>(this.apiUrl+"Todo/Delete", todo);
  }
}
