import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private apiKey = '8mt1Nqq6enbPzLMvirM82J'; // Replace with your Alpha Vantage API key

  constructor(private http: HttpClient) { }

  searchStock(symbol: string) {
    const url = `https://brapi.dev/api/quote/list?search=${symbol}&?token=${this.apiKey}`;
    return this.http.get(url);
  }
}


