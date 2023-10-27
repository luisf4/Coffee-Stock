import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StockService {
  private apiKey = 'GEI0DCLRW3HGMQJQ'; // Replace with your Alpha Vantage API key

  constructor(private http: HttpClient) { }

  searchStock(symbol: string) {
    const url = `https://www.alphavantage.co/query?function=SYMBOL_SEARCH&keywords=${symbol}&apikey=${this.apiKey}`;
    return this.http.get(url);
  }
}
