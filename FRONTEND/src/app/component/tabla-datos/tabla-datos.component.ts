import { Component, OnInit } from '@angular/core';
import { ResponseMcasI } from 'src/app/domain/mcas';
import { McasService } from 'src/app/service/mcas.service';

const themeGithub: string = 'node_modules/highlight.js/styles/github.css';

@Component({
  selector: 'app-tabla-datos',
  templateUrl: './tabla-datos.component.html',
  styleUrls: ['./tabla-datos.component.css']
})
export class TablaDatosComponent implements OnInit {

  constructor(public mcasService: McasService) { 
    this.displaycode=false
  }
  displaycode:boolean
  ngOnInit(): void {
    
  }

  botonCambioLenguaje(){
    this.displaycode=!this.displaycode
  }


  CaNotes:ResponseMcasI
  CaNotesArray:any[]
  async getAll(){
    this.CaNotes=await this.mcasService.getMCa("a")

    const fila=this.CaNotes.cA_notes.split("/")

    for (let i = 0; i < fila.length; i++) {
      const element = fila[i].split(",");
      
    }
  }
  
  codech=""

codejava= ""
tipodemetodo=""

currentTheme: string = themeGithub


}
