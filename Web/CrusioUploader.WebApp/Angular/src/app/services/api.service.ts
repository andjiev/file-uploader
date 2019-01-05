import { HttpClient } from '@angular/common/http';
import { IFileDto } from '../models/models';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class ApiService {
  constructor(private http: HttpClient) {}

  public getFiles(): Observable<IFileDto[]> {
    const url = `${environment.apiUrl}/api/files`;
    return this.http.get<IFileDto[]>(url);
  }
}
