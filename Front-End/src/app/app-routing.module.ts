import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { HomeComponent } from './stocks/home/home.component';
import { ChartsComponent } from './stocks/charts/charts.component';
import { SearchStocksComponent } from './stocks/search-stocks/search-stocks.component';
import { StocksPageComponent } from './stocks/stocks-page/stocks-page.component';

const routes: Routes = [
  {path: "", component: HomeComponent},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},
  {path: "charts", component: ChartsComponent},
  {path: "search", component: SearchStocksComponent},
  {path: "stock/:symbol", component: StocksPageComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
