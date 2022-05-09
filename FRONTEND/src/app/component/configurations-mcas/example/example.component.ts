import { mcasE, parametersArrayI, variablesReq } from './../../../domain/mcas';
import { Component, OnInit } from '@angular/core';
import { CategoriaService } from '../../../service/categoria.service';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HighlightAutoResult, HighlightLoader } from "ngx-highlightjs";

const themeGithub: string = 'node_modules/highlight.js/styles/github.css';
const themeAndroidStudio: string = 'node_modules/highlight.js/styles/androidstudio.css';


@Component({
  selector: 'app-reactive-form',
  templateUrl: './example.component.html',
  styleUrls: ['./example.component.css']
})
export class exampleComponent implements OnInit {
  parametersArray: string[] = [];

  animals: string[] = [];

  exam = [
    {
      name: '1',
      description: '1'
    },
    {
      name: '2',
      description: '2'
    }

  ]
  data = Object();
  //**** */
  categorias: object;
  categoriaForm: FormGroup;
  categoria: string[] = [];
  obj: string[] = [];

  nombreCat: string[] = [];

  idCategoria!: string;
  textButton: string;
  lista: string[] = ["hola", "que", "tal", "estas"];
  dato: string;
  arr: Object;

  categoriform = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl('', Validators.required)
  })
  constructor(protected categoriaService: CategoriaService, public fb: FormBuilder, private route: ActivatedRoute,private hljsLoader: HighlightLoader) {
    this.textButton = "add";
    this.dato = '';
    this.categoriaForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
    

    this.categorias = [{name:String , description:String}];
    this.arr = [];
    

  }
  ngOnInit(): void {
    /* console.log(this.categoria); */
    let robot = {columns: 2, strength : 2,alphabet:'2,2,2'}
    console.log("ok"+this.diHola(robot));
  }


  codse = `function myFunction() {
    document.getElementById("demo1").innerHTML = "Test 1!";
    document.getElementById("demo2").innerHTML = "Test 2!";
  }`;
  

  saveParameter(form: variablesReq){
    let name = form;
    let description = form;
    console.log(" name: "+name+"\n description: "+description);
    this.parametersArray.push();
    console.log(this.data);
  }

  diHola(params:mcasE) {
   return  "aqui va el mcasE"+params.columns;
  }
  










  datosCat = []
  datos = [
    { id: 11, name: 'Mr. Nice', description: "aaaa" },
    { id: 12, name: 'Narco', description: "aaaa" },
    { id: 13, name: 'Bombasto', description: "aaaa" },
    { id: 14, name: 'Celeritas', description: "aaaa" },
    { id: 15, name: 'Magneta', description: "aaaa" },
    { id: 16, name: 'RubberMan', description: "aaaa" },
    { id: 17, name: 'Dynama', description: "aaaa" },
    { id: 18, name: 'Dr IQ', description: "aaaa" },
    { id: 19, name: 'Magma', description: "aaaa" },
    { id: 20, name: 'Tornado', description: "aaaa" }
  ];


  public datoSeleccionado = '';

  public informacion(dato: any) {
    this.datoSeleccionado = dato;
  }

  /* getCategorias() {
    this.categoriaService.getCategorias().subscribe(res => {
      this.categorias = res;
    });
  } */

  agregarCategoria() {
    console.log(this.categoriform);
    this.categorias = this.categoriform;
    const obj = Object.values(this.categoriform);
    console.log("....." + obj + ":::: " + this.categorias);
    var arre = [];
    arre.push(this.categoriaForm.value);
    /* console.log(arre); */
    this.categoria = arre;
    /* console.log(Object.values(this.categoria)); */
    /* this.datosCat = Object.values(this.categoriaForm.value); */
    /* console.log("datos::"+this.datosCat); */

  }

  consultar() {
    /* const obj2 = Object.values(this.categoria) */
    /*  console.log(this.datosCat); */

    /* var arr = []; */
    /* arr.push(this.categoriaForm.value); */
    /* arr[arr.length] = this.categoriaForm.value; */
    /* this.arr = arr;
    console.log(arr); */


    /* const count = this.animals.push('cows'); */
    /* console.log(count); */
    // expected output: 4
    /* console.log(this.animals); */
    // expected output: Array ["pigs", "goats", "sheep", "cows"]

    /* this.animals.push(); */
    /* console.log(this.animals); */
    // expected output: Array ["pigs", "goats", "sheep", "cows", "chickens", "cats", "dogs"]

  }

  /* borrarCategoria(idCategoria: number) {
    this.categoriaService.borrarCategoria(idCategoria).subscribe(res => {
      console.log(res);
      this.getCategorias();
    })
  } */

  response: HighlightAutoResult;

  code = `function myFunction() {
  document.getElementById("demo1").innerHTML = "Test 1!";
  document.getElementById("demo2").innerHTML = "Test 2!";
}`;

  currentTheme: string = themeGithub;

  onHighlight(e: HighlightAutoResult) {
    this.response = {
      language: e.language,
      relevance: e.relevance,
      secondBest: '{...}',
      value: '{...}',
    };
  }

  changeTheme() {
    this.currentTheme = this.currentTheme === themeGithub ? themeAndroidStudio : themeGithub;
    this.hljsLoader.setTheme(this.currentTheme);
  }

codech=`
using System;
using System.IO;
namespace readwriteapp
{
    class Nombre de clase c#
    {
        [STAThread]
        static void Main(string[] args)
        {
            String line;

        }
    }
}`

codejava=`
public class Nombre de clase java {

  public static void main (String [ ] args) {
      System.out.println ("Empezamos la ejecución del programa");
  } 

} 
`

}


