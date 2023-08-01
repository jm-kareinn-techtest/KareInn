import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { HttpService } from './http.service'
import { Observable } from 'rxjs';
import { Beer } from '../data/beer';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BeerService extends HttpService {
  constructor(httpClient: HttpClient) {
    super(httpClient)
  }

  getBeers(search: string = '', style: number = -1, start: number = 0, take: number = 10): Observable<Beer[]> {
    return this.httpClient
      .get<Beer[]>(`${this.url(environment.apiRoutes.beers)}?style=${style}&skip=${start}&take=${take}&searchTerms=${encodeURIComponent(search)}`)
  }
}
