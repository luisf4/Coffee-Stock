import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class PortfolioService {
  private url = "http://localhost:5019/portfolio/";

  constructor(private http: HttpClient) {}

  getPortfolios(token: string): Observable<any> {
    const credentials = { jwtToken: token };
    return this.http.post<any>(this.url + "get/all", credentials);
  }

  CreatePortfolios(token: string, portfolio: string): Observable<any> {
    const credentials = { jwtToken: token };
    return this.http.post<any>(`${this.url}${portfolio}`, credentials);
  }

  DeletePortfolio(token: string, id: string): Observable<any> {
    return this.http.delete<any>(`${this.url}${id}/${token}`);
  }
  
  UpdatePortfolio(token: string, portfolio: string, newName: string): Observable<any> {
    const credentials = { jwtToken: token };
    return this.http.put<any>(`${this.url}${portfolio}/${newName}`, credentials);
  }
}
