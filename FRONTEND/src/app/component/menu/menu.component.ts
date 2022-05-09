import { LoginI } from './../../domain/login.interface';
import { Component, OnInit } from '@angular/core';
import { ResponseLoginI } from '../../domain/response.interface';

import { LoginService } from '../../service/login.service';



import { Subscription, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms'; '@angular/forms';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  public subList: Subscription = new Subscription;
  public showMsg: boolean = false;
  public msg: string = "";
  public type: string = "";
  public dataU: string[] = [];
  public tam : string | any;

  public parametros ={
    name : [''],
    description:['']
  }
  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  })

  constructor(private loginService: LoginService, private router: Router, private cookies: CookieService) { 
    this.dataU = [];
  }

  ngOnDestroy(): void {
    this.subList.unsubscribe();
  }
  ngOnInit(): void {
    this.checkLocalStorage();
  }

  checkLocalStorage(){
    if(localStorage.getItem('token')){
      /* this.router.navigate(['mcas-list']) */
      /* this.router.navigate(['example']) */
    }
  }
  offLogin(){
    localStorage.removeItem("token");
    this.cookies.deleteAll()
    this.router.navigate(['/']);
  }

  onLogin(){
    var LoginUser:LoginI={
      username:this.loginForm.controls['email'].value,
      password:this.loginForm.controls['password'].value
    }
    
    this.loginService.loginByEmail(LoginUser).then(res=>{
      if(res.succeeded==true){
        console.log(res)
        this.router.navigate(['/mcas-list']);
      }else{
        
      }
    })
  }
}
