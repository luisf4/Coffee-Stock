import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { HomeComponent } from './stocks/home/home.component';
import { ChartsComponent } from './stocks/charts/charts.component';
import { SearchStocksComponent } from './stocks/search-stocks/search-stocks.component';
import { StocksPageComponent } from './stocks/stocks-page/stocks-page.component';
import { PortfolioComponent } from './stocks/portfolio/portfolio.component';
import { PortfolioItemComponent } from './stocks/portfolio-item/portfolio-item.component';
import { AboutComponent } from './stocks/about/about.component';
import { ChartDonutComponent } from './stocks/chart-donut/chart-donut.component';

const routes: Routes = [
  {path: "", component: HomeComponent},
  {path: "login", component: LoginComponent},
  {path: "register", component: RegisterComponent},
  {path: "charts", component: ChartsComponent},
  {path: "search", component: SearchStocksComponent},
  {path: "stock/:symbol", component: StocksPageComponent},
  {path: "portfolio", component: PortfolioComponent},
  {path: "portfolio/:portfolio_id/:name", component: PortfolioItemComponent},
  {path: "about", component: AboutComponent},
  {path: "charts-donut", component: ChartDonutComponent},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
