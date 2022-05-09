import { McasService } from './../../../service/mcas.service';
import { Component, OnInit , OnDestroy, NgIterable} from '@angular/core';
import { mcasI } from 'src/app/domain/mcas';
import { Subscription } from 'rxjs';
import {ProjectsSchema} from '../../../domain/ProjectsSchema'
import { getProjects } from 'src/app/domain/getProjects';
import { callcomponents } from 'src/app/service/Callcomponents.service';

@Component({
  selector: 'app-list-projects',
  templateUrl: './list-projects.component.html',
  styleUrls: ['./list-projects.component.css']
})
export class ListProjectsComponent implements OnInit, OnDestroy {

  public mcas : mcasI[] | undefined;
  public subMcas: Subscription = new Subscription;
  constructor(public mcasService: McasService, private serviceComponent:callcomponents) { 
    
  }

  ngOnDestroy(): void {
    this.subMcas.unsubscribe();
  }

  ngOnInit(): void {
    console.log(this.mcasService.obtenerVariables())
    this.getAll();
  }

   ProjectsSchemaArray:getProjects[]
  async getAll(){
    this.ProjectsSchemaArray=await this.mcasService.getAllProjects()
  }

  EditarButton(idlist:any){
    this.serviceComponent.callsecondcomponent(idlist)
  }

  async eliminarProyecto(idlist:String){
    await this.mcasService.eliminar(idlist)
  }
}
