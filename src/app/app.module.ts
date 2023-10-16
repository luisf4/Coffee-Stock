import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { StocksPageComponent } from './stocks/stocks-page/stocks-page.component';
import { SearchStocksComponent } from './stocks/search-stocks/search-stocks.component';
import { AuthService } from './auth/services/authServices';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './stocks/home/home.component';
import { NavbarComponent } from './stocks/navbar/navbar.component';
import { ChartsComponent } from './stocks/charts/charts.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { NavbarUpComponent } from './stocks/navbar-up/navbar-up.component';
import { FooterComponent } from './stocks/footer/footer.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    StocksPageComponent,
    SearchStocksComponent,
    HomeComponent,
    NavbarComponent,
    ChartsComponent,
    NavbarUpComponent,
    FooterComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule, // Add FormsModule here
    HttpClientModule,
    ReactiveFormsModule,
    NgApexchartsModule
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
