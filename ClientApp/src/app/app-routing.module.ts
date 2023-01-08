import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

/** Components */
import { WorkshopComponent } from './screens/workshop/workshop.component';
import { FetchDataComponent } from './screens/fetch-data/fetch-data.component';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', loadChildren: () => import('./home/home.module').then(m => m.HomeModule) },
    { path: 'workshop', component: WorkshopComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: '**', redirectTo: 'home' },

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
