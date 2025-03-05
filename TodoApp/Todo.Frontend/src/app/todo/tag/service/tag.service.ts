import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tag } from '../model/tag';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  constructor(
    @Inject("apiUrl") private apiUrl: string,
    private httpClient: HttpClient,
  ) { }

  getList(id: string): Observable<Tag[]> {
    const params = new HttpParams().set('id', id);
    return this.httpClient.get<Tag[]>(`${this.apiUrl}Tag/GetList`, { params });
  }
  getListOfUnchosenTags(userId: string, todoId: string): Observable<Tag[]> {
    const params = new HttpParams().set('userId', userId).set('todoId', todoId);
    return this.httpClient.get<Tag[]>(`${this.apiUrl}Tag/GetListOfUnchosenTags`, { params });
  }

  getById(id: string): Observable<Tag> {
    const params = { id: id };
    return this.httpClient.get<Tag>(this.apiUrl + "Tag/GetById", { params });
  }

  add(tag: Tag): Observable<Tag> {
    return this.httpClient.post<Tag>(this.apiUrl + "Tag/Create", tag);
  }

  update(tag: Tag): Observable<Tag> {
    return this.httpClient.post<Tag>(this.apiUrl + "Tag/Update", tag);
  }

  delete(tag: Tag): Observable<Tag> {
    return this.httpClient.post<Tag>(this.apiUrl + "Tag/Delete", tag);
  }



}
