import { CookieService } from 'ngx-cookie-service';
import { CustomerLogin } from './../domain/customer';
import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';

import {ResponseLoginI } from '../domain/response.interface';
import {LoginI} from '../domain/login.interface';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  
  public urlLogin: string;
  public urlRegister: string;

  constructor(private http:HttpClient, private cookies: CookieService) { 
    this.urlLogin = "http://localhost:3008/api/tesis/login";
 
    this.urlRegister = "http://localhost:51354/v1/identity";
  }

  loginByEmail(formLogin:LoginI):Promise<ResponseLoginI>{
    var data=this.http.post<ResponseLoginI>(this.urlLogin, formLogin).toPromise().then(res=>{
      this.setToken(res.accessToken, res.IdUser)
      
      return res
    }, error=>{
      alert ('Inicio de sesion fallido')
      return error
    })

    return data;
  }

  setToken(token:string, usuario:string){
    this.cookies.set("token", token);
    this.cookies.set("uiduser", usuario);
  }

  deleteToken(){
    this.cookies.delete("token")
  }

  //Guard
  isLogged(){
    return (this.cookies.get("token"))?true:false
  }
}
