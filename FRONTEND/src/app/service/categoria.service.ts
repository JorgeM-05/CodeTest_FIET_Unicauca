import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {

  constructor(protected http: HttpClient) { }

  getCategorias() {
    return this.http.get(`/categorias`);
  }

  getCategoria(idCategoria: string){
   return this.http.get(`/categorias/`);
  }

  guardarCategoria(categoria: any) {
    return this.http.post(`/addCategoria`, categoria);
  }

  borrarCategoria(idCategoria: number){
      return this.http.delete(`/deleteCategoria/`);
  }

  actualizarCategoria(categoria: any){
    return this.http.put(`/updateCategoria/`, categoria);
  }
}
