import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoTag } from '../model/todoTag';

@Injectable({
  providedIn: 'root'
})
export class TodoTagService {

  constructor(
    @Inject("apiUrl") private apiUrl: string,
    private httpClient: HttpClient,
  ) { }

  getById(id: string): Observable<TodoTag> {
    const params = { id: id };
    return this.httpClient.get<TodoTag>(this.apiUrl + "TodoTag/GetById", { params });
  }

  add(todoTag: TodoTag): Observable<TodoTag> {
    return this.httpClient.post<TodoTag>(this.apiUrl + "TodoTag/Create", todoTag);
  }

  update(todoTag: TodoTag): Observable<TodoTag> {
    return this.httpClient.post<TodoTag>(this.apiUrl + "TodoTag/Update", todoTag);
  }

  delete(todoTag: TodoTag): Observable<TodoTag> {
    return this.httpClient.post<TodoTag>(this.apiUrl + "TodoTag/Delete/", todoTag);
  }
  deleteWithItems(todoId: string, tagId: string): Observable<TodoTag> {
    const url = `${this.apiUrl}TodoTag/DeleteWithItems?todoId=${todoId}&tagId=${tagId}`;
    return this.httpClient.post<TodoTag>(url, {});
  }
  
  

}
