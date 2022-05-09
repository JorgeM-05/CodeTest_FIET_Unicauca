import { mcasE, mcasI, mcasIE, ResponseMcasI } from 'src/app/domain/mcas';
import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomerLogin } from '../domain/customer';
import { getProjects } from '../domain/getProjects';
import { CookieService } from 'ngx-cookie-service';
import { DataProjectI, variables } from '../domain/DataProjectInterface';



@Injectable({
  providedIn: 'root'
})
export class McasService {

  constructor(private http:HttpClient, private cookie:CookieService){

  }
  
  urls:string="http://localhost:52270"

  header:any={
    headers: {
      "Access-Control-Allow-Origin":"http://localhost:4200",
      "Content-Type": "application/json",
      "Authorization": "Bearer "+this.cookie.get("token"),
    }
  }

  createProject(DataPost:DataProjectI):Promise<ResponseMcasI>{

    var data=this.http.post<ResponseMcasI>(this.urls+"/Orchestrator/Api_Logic/Gateway", DataPost, {
      headers: {
        "Access-Control-Allow-Origin":"http://localhost:52270/api",
        "Content-Type": "application/json",
        "Authorization": "Bearer "+this.cookie.get("token"),
      }
    }).toPromise().then(res=>{
      return res
    }).catch(error=>{
      return error
    })

    return data
  }

  getAllProjects():Promise<getProjects[]>{
    var data=this.http.get<getProjects>(this.urls+"/getprojects/"+this.cookie.get("uiduser"), {
      headers: {
        "Access-Control-Allow-Origin":"http://localhost:4200",
        "Content-Type": "application/json",
        "Authorization": "Bearer "+this.cookie.get("token"),
      }
    }).toPromise().then(response=>{
      
      
      return response
    }).catch(error=>{
      
      console.log(error)
      return error
    })
    
    console.log(data)
   return data
  }

  eliminar(idlist:String){
    this.http.get(this.urls+"/eliminarproyecto/"+idlist, this.header).toPromise().then(res=>{
      return res
    })
  }

  arrayOfItems:variables[];

  obtenerVariables():Promise<any[]> {
    return this.http.get('https://my.api.mockaroo.com/variables?key=bfdfc2b0').toPromise()
      .then(res=>JSON.parse(JSON.stringify(res)).data)
      .then(res=>{
        console.log(res)

        return JSON.parse(JSON.stringify(res))
      })
    
  }


  getMCa(idlist:string):Promise<ResponseMcasI>{
    var data=this.http.get<ResponseMcasI>(this.urls+"/getmca/"+idlist, this.header).toPromise().then(res=>{
      return res
    }).catch(error=>{
      console.log(error)
      return error
    })

    return data
  }
}


