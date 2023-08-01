import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { HttpService } from './http.service'
import { Style } from '../data/style'
import { Observable, map } from 'rxjs'
import { environment } from 'src/environments/environment'


@Injectable({
  providedIn: 'root'
})
export class StyleService extends HttpService {
  constructor(httpClient: HttpClient) {
    super(httpClient)
  }

  getStyles(): Observable<Style[]> {
    return this.httpClient
      .get<Style[]>(this.url(environment.apiRoutes.styles))
  }
}
