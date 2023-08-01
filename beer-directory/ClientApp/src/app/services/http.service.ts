import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment'
import { HttpClient, HttpHeaders } from '@angular/common/http'


@Injectable({
  providedIn: 'root'
})
export abstract class HttpService {

  constructor(protected httpClient: HttpClient) { }

  protected url(endpoint: string): string {
    return `${environment.serverProtocol}${environment.serverAddress}${endpoint}`
  }
}
