import { ResponseLoginI } from '../../../domain/response.interface';
import { LoginI } from '../../../domain/login.interface';
import { LoginService } from '../../../service/login.service';
import { CustomerLogin } from '../../../domain/customer';

import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms'; '@angular/forms';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  public showMsg: boolean = false;
  public msg: string = "";
  public type: string = "";
  public customerLogin!: CustomerLogin;
  public subList: Subscription = new Subscription;

  loginForm = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  })

  constructor(private loginService: LoginService, private router: Router, /*private cookieService:CookieService */) { }


  ngOnDestroy(): void {
    this.subList.unsubscribe();
  }

  ngOnInit(): void {

    /* this.customerLogin = new CustomerLogin('',''); */

  }
  onLogin() {
    var LoginUser:LoginI={
      username:this.loginForm.controls['email'].value,
      password:this.loginForm.controls['password'].value
    }
    console.log(LoginUser);
    this.loginService.loginByEmail(LoginUser).then(res=>{
      
    })
  }

  //  public login() {
  //   console.log(this.customerLogin);
  //   this.loginService.login(this.customerLogin).subscribe(data => {
  //      console.log(data);
  //     let dataResponse:LoginService = data;
  //     console.log("dataresponse"+dataResponse.getToken); 
  //     if(dataResponse.getToken = true){
  //       localStorage.setItem("token", data);
  //       this.router.navigate(['/new-Projects']); 
  //       this.showMsg = true;
  //       this.msg = 'login successful';
  //       this.type = 'success';
  //      } 

  //     /* this.loginService.setToken(data.token); */
  //     /* console.log("token...."+this.loginService.setToken(data.token)); */


  //     /* this.router.navigate(['/new-Projects']); *
  //   }, error => {
  //     console.log(error);
  //     this.showMsg = true;
  //     this.msg = 'Error login user';
  //     this.type = 'danger';
  //   });
  // } 


}
