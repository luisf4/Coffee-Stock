import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
@Injectable({
  providedIn: "root"
})

export class StockService { 
  private url ="http://localhost:5019/stock/"

  constructor(private http: HttpClient) {}

  GetStock(symbol: string): Observable<any> {
    const data = { symbol: symbol };
    return this.http.get<any>(this.url+symbol);
  }
}