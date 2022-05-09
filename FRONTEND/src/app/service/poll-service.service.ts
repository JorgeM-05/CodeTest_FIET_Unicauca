import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PollServiceService {

  url = 'https://urlParaEnvioDeEncuesta/';
  

  constructor(private http:HttpClient) { }

  addPoll(poll) {
    console.log(poll);
    return this.http.post(this.url, poll);  
  }

}

