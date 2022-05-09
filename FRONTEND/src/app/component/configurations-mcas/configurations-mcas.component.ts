import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-configurations-mcas',
  templateUrl: './configurations-mcas.component.html',
  styleUrls: ['./configurations-mcas.component.css']
})
export class ConfigurationsMCAsComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
    const token = localStorage.getItem("token");
    /* console.log(" token.... \n"+localStorage.getItem("token")); */
    if(!token){
      /* this.router.navigate(['Login']); */
      /* this.router.navigate(['new-Projects']); */
    }
    else {
      this.router.navigate(['mcas-list']);
      this.router.navigate(['new-Projects']);
     /*  this.router.navigate(['example']); */
      
    }
  }
}
