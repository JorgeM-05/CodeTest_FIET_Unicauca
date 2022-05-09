import { exampleComponent } from './component/configurations-mcas/example/example.component';
import { ListProjectsComponent } from './component/configurations-mcas/list-projects/list-projects.component';
import { CreationNewProjectsComponent} from './component/configurations-mcas/creation-new-projects/creation-new-projects.component'
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './component/menu/login/login.component';
import { FormsModule } from "@angular/forms"
import { TablaDatosComponent } from './component/tabla-datos/tabla-datos.component';

const routes: Routes = [
  {path: 'mcas-list', component: ListProjectsComponent},
  {path: 'new-Projects', component: CreationNewProjectsComponent},
  {path: 'Login', component: LoginComponent},
  {path: 'example', component: exampleComponent},
  {path: 'data-project', component:TablaDatosComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
            FormsModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }