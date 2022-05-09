import { parametersArrayI, mcasI, variablesReq, index, mcasE, mcasIE, ResponseMcasI } from './../../../domain/mcas';

import { McasService } from './../../../service/mcas.service';
import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { ChildActivationEnd, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators, FormArray, FormControlName } from '@angular/forms'; import { LEADING_TRIVIA_CHARS } from '@angular/compiler/src/render3/view/template';
import { DataProjectI, variables } from 'src/app/domain/DataProjectInterface';
'@angular/forms';
import { v4 as uuidv4 } from 'uuid';
import { CookieService } from 'ngx-cookie-service';
import { HighlightAutoResult, HighlightLoader } from 'ngx-highlightjs';
import { __spreadArrays } from 'tslib';
import { getProjects } from 'src/app/domain/getProjects';
import { EncuestaModalComponent } from './encuesta-modal/encuesta-modal.component';

import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

const themeGithub: string = 'node_modules/highlight.js/styles/github.css';
const themeAndroidStudio: string = 'node_modules/highlight.js/styles/androidstudio.css';


@Component({
  selector: 'app-creation-new-projects',
  templateUrl: './creation-new-projects.component.html',
  styleUrls: ['./creation-new-projects.component.css']
})


export class CreationNewProjectsComponent implements OnInit, OnDestroy {

  loading = false;
  public showMsg: boolean = false;
  public msg: string = "";
  public type: string = "";
  public subList: Subscription = new Subscription;
  ctn: number;
  categoria: any;
  public code: string = "";
  public arrayParam = new Object();
  public arrayejemplo: any[] = [];
  public objvalue = new Object();
  public arrayValue: any[] = [];
  public alph: any[] = [];
  public response!: string;

  public vari_array:any[]=[]


  public CA: string
  public Columns: any
  public Variable:any[] = []
  public ca_notes: any
  public arrayVarExpected: any[] = [];

  request = new FormGroup({
    nameClass: new FormControl('', Validators.required),
    name: new FormControl('', Validators.required),

    return: new FormControl('', Validators.required),
    strength: new FormControl('', Validators.required),
  })

  request_var = new FormGroup({
    name_var: new FormControl('', Validators.required),
    type_var: new FormControl('', Validators.required),
  })

  request_value = new FormGroup({
    value: new FormControl('', Validators.required),
  })
 
  form_req_expect = new FormGroup({
    Expected: new FormArray([])
  });
  get Expected(): FormArray {
    return this.form_req_expect.get('Expected') as FormArray;
  }

  displaycode:boolean
  displayVar:boolean
  displayCodes:boolean

  constructor(private snackBar: MatSnackBar, public mcasService: McasService, private router: Router, private fb: FormBuilder, private cookie:CookieService,private hljsLoader: HighlightLoader, public dialog: MatDialog) {
    this.ctn = 0;
    this.arrayParam

    this.displaycode=false
    this.displayVar=false
    this.displayCodes=false
  }

  get parametersArray() {
    return this.form_parameters.get('parametersArray') as FormArray;
  }
  get values() {
    return this.form_parameters.get('values') as FormArray;
  }

  form_parameters = this.fb.group({
    name_var: this.fb.array([]),
    values: this.fb.array([])
  });

  //mcas = [];
  ngOnDestroy(): void {
    this.subList.unsubscribe();
  }

  ngOnInit(): void {
    this.codejava = `public class TestShould  \n [Theory]  \n[InlineData(\"2 \",\"4\",\"4 \",)]\n[InlineData(\"2 \",\"4 \",\"2 \",)]\n[InlineData(\"2 \",\"2 \",\"4 \",)]\n[InlineData(\"2 \",\"4\",\"4 \",)]\n[InlineData(\"2 \",\"4 \",\"4 \",)]\n[InlineData(\"2 \",\"2 \",\"2 \",)]\n[InlineData(\"3 \",\"4\",\"2 \",)]\npublic void nombredelmetododelaprueba(String Variable0,String Variable1,String Variable2)\n{\n//Arrange\nvar nombreClase = new nombreClase();\n//Act\nvar respuesta = nombreClase.nombredelmetododelusuario();\n//Assert\nAssert.Equal(respuesta, expected)\n}`
    
  //this.request.patchValue({static})
    /* this.crearFormularioParameters(); */
    /* this.anadirParameters(); */
    /* console.log("verificando estado")
    if (this.arrayParam == null){
      console.log("es diferente de null")
    }
    else{
      console.log("es null"+this.arrayParam)
    } */
  }

  indice(indice: number) {
    this.ctn = indice;

    this.arrayValue=[]
    /*para un futuro posible traer datos guardados al modal 
    */
    // if(this.arrayejemplo[this.ctn][2] != undefined){
    //   this.arrayValue = (this.arrayejemplo[this.ctn][2].split(","));
    //   console.log(this.arrayValue)
    // }
    // else{
    //   console.log("undfi")
    // } 
  }
  addNameField() {

    this.Expected.push(new FormControl('', Validators.required)); 
  }
  deletNameField() {
    if (this.Expected.length > 1) { 
      for(let i=0;i<this.Expected.length; i++){
        this.Expected.value.removeAt(i);
      } 
    }
  }
  /* 1 */
  addparameters() {
    console.log("add....")
    this.arrayejemplo.push(Object.values(this.request_var.value));
    // console.log(this.arrayejemplo)

    // for(let i = 0; i < 8; i++) {
    //   this.arrayVarExpected.push(Object.values(this.form_req_expect.value))
    //   console.log(this.arrayVarExpected)
    // }
    
    // this.arrayVarExpected.push(Object.values(this.Expected.at(0).value))
    for(let i = 0; i < this.Expected.length; i++) {
      console.log(this.Expected.at(i).value);
    }
    
   
  }

  /* 2 */
  addvalue() {
    console.log("add")
    this.arrayValue.push(Object.values(this.request_value.value));
  }

  /* 3 */
  save() {
    console.log("save")

    var str = this.arrayValue.toString();
    this.arrayejemplo[this.ctn].splice(2, 1, str);
    console.log(Object.values(this.arrayejemplo))
    /* console.log("arra.."+this.arrayejemplo) */
    this.arrayValue.splice(0, this.arrayValue.length);
  }

  editar(numero:number){
    this.ctn = numero;
    this.arrayValue=[]
    this.arrayValue=this.arrayejemplo[this.ctn][2].split(",")
  }

  eliminar(numero:number){
    this.ctn = numero;
    this.arrayValue=[]
    this.arrayejemplo.splice(this.ctn, 1)
  }
  eliminarElemento(numero:number){
    //var arr=this.arrayejemplo[this.ctn][2].split(",")
    this.arrayValue.splice(numero, 1)
  }

  generateTest() {
    // let Colums = this.arrayValue.length;
    // console.log("ok"+Object.values(this.arrayValue)+" tamaño"+this.arrayValue.length)
    // let fuerza = this.request.value.strength;
    // let alfabeto= String(this.generarAlfabeto());

    // let requestApi = { Alphabet: alfabeto,Columns: Colums, Strength : fuerza}
    // console.log("ok enviamos a la services... "+this.requestServices());
    this.codejava = `public class TestShould  \n [Theory]  \n[InlineData(\"2 \",\"4\",\"4 \",)]\n[InlineData(\"2 \",\"4 \",\"2 \",)]\n[InlineData(\"2 \",\"2 \",\"4 \",)]\n[InlineData(\"2 \",\"4\",\"4 \",)]\n[InlineData(\"2 \",\"4 \",\"4 \",)]\n[InlineData(\"2 \",\"2 \",\"2 \",)]\n[InlineData(\"3 \",\"4\",\"2 \",)]\npublic void nombredelmetododelaprueba(String Variable0,String Variable1,String Variable2)\n{\n//Arrange\nvar nombreClase = new nombreClase();\n//Act\nvar respuesta = nombreClase.nombredelmetododelusuario();\n//Assert\nAssert.Equal(respuesta, expected)\n}`
    // this.cambio()
  }

  requestServices(){

    this.loading = true;
    let inizDate=new Date();
    let fechareal=""+inizDate.getDate().toString()+"-"+(inizDate.getMonth()+1)+"-"+inizDate.getFullYear()+"";

    let newidproject=uuidv4()

    this.vari_array=[]
    for (let i = 0; i < this.arrayejemplo.length; i++) {
      
      var vars_:variables={
        _idVariable:uuidv4(),
        _idProject:newidproject,
        nombre_var:this.arrayejemplo[i][0],
        tipo_de_variable:this.tipode(Number(this.arrayejemplo[i][1])),
        valores:this.arrayejemplo[i][2]
       
      }
      
      this.vari_array.push({...vars_})
    }

    var datos:DataProjectI={
      PropertiesProject:{ 
        _idList:uuidv4(),
        _iduser:this.cookie.get("uiduser"),
        nombre:this.request.value.name,
        company:"UNICAUCA",
        fecha:fechareal
    },
    DataProject:{
        _idProject:newidproject,
        nameClass:this.request.value.nameClass,
        metodo:this.request.value.name,
        strength:this.request.value.strength,
        tipo_de_metodo:this.tipode(Number(this.request.value.return))
    },
    variables:this.vari_array
    }

    console.log(datos)
    
    this.mcasService.createProject(datos).then(res=>{
      this.code = res.code;
      console.log(res);
      this.CA = res.cA_notes
      this.Columns = res.columns;
      this.loading = false;
      this.displayVar=!this.displayVar;
      this.splitCode()
    })
  }

  splitCode(){
    console.log("split code..")
    this.ca_notes = null
    this.ca_notes = this.CA.split('/');
    this.deletNameField()
    
    for(let i=0 ; i< this.ca_notes.length;i++){
        this.Variable[i] = this.ca_notes[i].split(',');
        // console.log(this.Variable[i]);
        this.addNameField();
    }

  }
  armarCode(){
    console.log("armando code.."+this.tipode(Number(this.request.value.return)))
    this.arrayVarExpected.push(Object.values(this.form_req_expect.value));
    // console.log(this.vari_array[0].tipo_de_variable)
    var responseCode = `public class  TestShould  \n  { \n   [Theory] \n`;

    for(let i=0; i < this.ca_notes.length; i++)
    {

      responseCode = responseCode +  `    [InlineData(`;
        for (let K = 0; K < this.Columns; K++)
        {
   
          if(this.vari_array[K].tipo_de_variable === "String"){

            responseCode += `\"`;
            responseCode += this.Variable[i][K];
            responseCode += `\",`;
          }
          else{
            
            responseCode += this.Variable[i][K];
            responseCode += `,`;
          }

        }
        // console.log("val expected"+this.arrayVarExpected[i])
        responseCode += `\"`+Object.values(this.form_req_expect.value)[0][i]+`\",)]\n`;
    }
    responseCode += `  public void `+this.request.value.name+`(`;

    for (let K = 0; K < this.Columns+1; K++)
    {
        if (K == this.Columns)
        {
            responseCode += this.tipode(Number(this.request.value.return))+` expected `; // Hay que validar el tipo de variable, si es int, string, bool etc
        }
        else {
            responseCode += this.vari_array[0].tipo_de_variable+` `+this.vari_array[K].nombre_var+`,`; // Hay que validar el tipo de variable, si es int, string, bool etc
        }       
    }

            responseCode += `)\n`;
            responseCode += `  {\n`;
            responseCode += `     //Arrange\n`;
            responseCode += `     var `+this.request.value.nameClass+` = new `+this.request.value.nameClass+`();\n`; // aqui tenemos que recibir el nombre de la clase que el cliente va a probar, toca ponerlo en el front
            responseCode += `     //Act\n`;
            responseCode += `     var response = `+this.request.value.nameClass+`.`+this.request.value.name+`();\n`;
            responseCode += `     //Assert\n`;
            responseCode += `     Assert.Equal(response, expected)`; // Aqui va las respuestas esperadas por el Usuario , en expected
            responseCode += `\n}`;


    console.log(responseCode)
    this.codech  = responseCode
    this.codejava = responseCode

  }

  generateCode(){
    console.log("generando code..")
    this.displaycode=this.displaycode
    this.displayVar=!this.displayVar
    this.displayCodes=!this.displayCodes

    this.armarCode()
    
  }
  editVarOut(){
    this.displayVar=!this.displayVar
    this.displayCodes=!this.displayCodes
  }


  /*LINEAS DE CODIGO */

  codech=""
  codejava= ""
  tipodemetodo=""
  CaNotess:ResponseMcasI
  CaNotesArrays:any[]


  cambio(){
  //this.tipode(Number(this.arrayejemplo[0][1])).toLowerCase()+" "+this.arrayejemplo[0][0]+"=["+this.arrayejemplo[0][2]+"]"
    // this.tipodemetodo=this.tipode(Number.parseInt(this.request.value.return)).toLowerCase()
    /*this.codech=`
    using System;
    using System.IO;
    namespace readwriteapp
    {
      static class `+this.request.value.name+`
        {
            [STAThread]
            public `+((this.request.value.static==false)?"":"static ")+this.tipodemetodo+" "+this.request.value.name+` (string[] args)
            {
            
              `+
              this.arrayejemplo.map((tipo, index)=>{
                
                return this.tipode(Number(this.arrayejemplo[index][1])).toLowerCase()+" "+this.arrayejemplo[index][0]+"=["+this.arrayejemplo[index][2]+"]; \n"
              })
              +`
            }
        }
    }`*/
    console.log("codigo............"+ this.code);
    this.codech = this.code;

    
    console.log("first")
    // const a=this.codech.replace(/t/g, "\n")
    // const datos=a.split("{")
    // console.log(datos)
    /*for (let i = 0; i < a.length; i++) {
      console.log(a[i])
    }*/

    // console.log("second")

    // this.codejava=`
    // public class `+this.request.value.name+`{
    
    //   public`+((this.request.value.static==false)?"":"static ")+" "+this.tipodemetodo+` main(String [ ] args) {
    //     `+
    //     this.arrayejemplo.map((tipo, index)=>{
    //       return this.tipode(Number(this.arrayejemplo[index][1])).toLowerCase()+" "+this.arrayejemplo[index][0]+"=["+this.arrayejemplo[index][2]+"]; \n"
    //     })
    //     +`
    //   } 
    
    // } 
    // `
    
  }

  botonCambioLenguaje(){
    // this.displayCodes=!this.displayCodes
    // this.displayVar=!this.displayVar
    // this.displaycode=!this.displaycode
  }

  codigoInsertado: string = '';
  crearInsersion(){
  console.log("first")
  const a=this.codigoInsertado.replace(/t/g, "\n")
  const datos=a.split("{")
  console.log(datos)  
  console.log("second")

  }
  tipode(num:number){
    switch (num) {
      case 1:
        return "Int"
      break;
      case 2:
        return "Double"
      break;
      case 3:
        return "Long"
      break;
      case 4:
        return "String"
      break;
      default:
        return "void"
        break;
    }
  }
 

  generarAlfabeto() {    
    for(let i = 0; i< this.arrayejemplo.length;i++){
      let contador= (this.arrayejemplo[i][2].split(',')).length;
      this.alph.push(contador);
    }
    return String(this.alph);
  }

  /* public generatorTest(form:mcasIE):void{
     
    this.mcasService.save(form).subscribe(data =>{
      /* this.router.navigate(['/mcas-list']);  *
      this.showMsg = true;
      this.msg = 'se creo CAs exitosamente';
      this.type = 'success';
      console.log("se creo exitosamente");
    },error =>{
      console.log(error);
      this.showMsg = true;
      this.msg = 'Ha ocurrido un error';
      this.type = 'danger';
      console.log("error....");
    });
  } */



  borrarParameters(indice: number) {
    this.parametersArray.removeAt(indice);
  }


  switch_varType(type: variablesReq) {
    switch (type.type_var) {
      case "1":
        type.type_var = "int";
        break;
      case "2":
        type.type_var = "double";
        break;
      case "3":
        type.type_var = "long";
        break;
      case "4":
        type.type_var = "string";
        break;
      default:
        break;
    }

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
  
  crearFormularioParameters() {
    this.form_parameters = this.fb.group({
      parametersArray: this.fb.array([])
    });
  }
  addValues() {
    this.form_parameters = this.fb.group({
      values: this.fb.array([]),

    });
  }

  anadirParameters() {
    const arrays = this.fb.group({
      NameVariable: new FormControl('', Validators.required),
      value: new FormControl('', Validators.required),
      Type: new FormControl('', Validators.required)
    });
    /* this.parametersArray.push(arrays); */
  }

  /* borrarParameters(indice: number) {
    this.parametersArray.removeAt(indice);
  } */

  /*  submit() {
     console.log(this.form_parameters.value);
   } */

  save2(form2: mcasI) {
    /* console.log(form2.name); */

    /* this.categoria = this.mcasService.mcasI(); */
    //console.log(this.categoria);
    /* console.log(":::"+this.form_parameters.value); */
  }
  saveParameters() {
    //console.log(this.form_parameters.value);
  }
    rese: HighlightAutoResult;

    currentTheme: string = themeGithub
    arre:any=["1", "32", "1231"]

  modalPoll() {
    setTimeout(() => {
      
      const dialogRef = this.dialog.open(EncuestaModalComponent, {});
      dialogRef.afterClosed().subscribe(res => {
        if (res === true){
          this.snackBar.open('Gracias por responder esta encuesta','', {
            duration: 3000
            });
        }
        
      });
    }, 50);
  };

  get isNameFieldValid() {
    return this.request.get('name').touched && this.request.get('name').valid;
  }

  get isNameFieldInvalid() {
    return this.request.get('name').touched && this.request.get('name').invalid;
  }

  get isReturnFieldValid() {
    return this.request.get('return').touched && this.request.get('return').valid;
  }

  get isReturnFieldInvalid() {
    return this.request.get('return').touched && this.request.get('return').invalid;
  }

  get isInteractionFieldValid() {
    return this.request.get('strength').touched && this.request.get('strength').valid;
  }

  get isInteractionFieldInvalid() {
    return this.request.get('strength').touched && this.request.get('strength').invalid;
  }

  get isVarFieldValid() {
    return this.request_var.get('name_var').touched && this.request_var.get('name_var').valid;
  }

  get isVarFieldInvalid() {
    return this.request_var.get('name_var').touched && this.request_var.get('name_var').invalid;
  }

  get isTypeFieldValid() {
    return this.request_var.get('type_var').touched && this.request_var.get('type_var').valid;
  }

  get isTypeFieldInvalid() {
    return this.request_var.get('type_var').touched && this.request_var.get('type_var').invalid;
  }

  

}
