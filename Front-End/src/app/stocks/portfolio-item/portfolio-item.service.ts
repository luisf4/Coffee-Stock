import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class StocksPortfolioService {
    private url = "http://localhost:5019/stocksportfolio/";

    constructor(private http: HttpClient) { }

    getStocksPortfolios(token: string, id: any): Observable<any> {
        const credentials = { jwtToken: token };
        return this.http.post<any>(`${this.url}${id}`, credentials);
    }

    CreateStocksPortfolios(token: string, user: string, portfolio_id: any, stock: string, qnt: any, price: any, date: any): Observable<any> {
        // Convert 'portfolio_id', 'qnt', and 'price' to integers or floats
        const parsedPortfolioId = parseInt(portfolio_id);
        const parsedQuantity = parseInt(qnt, 10);
        const parsedPrice = parseFloat(price);

        const credentials = {
            jwtToken: token,
            user: user,
            portfolio_id: parsedPortfolioId,
            stock: stock,
            qnt: parsedQuantity,
            price: parsedPrice,
            date: date
        };
    
        console.log(credentials);
        return this.http.post<any>(`${this.url}`, credentials);
    }
    

    DeleteStocksPortfolio(token: string, id: string, portfolio_id: any): Observable<any> {
        return this.http.delete<any>(`${this.url}${id}/${portfolio_id}/${token}`);
    }

    UpdateStocksPortfolio(token: string, user: string, portfolio_id: any, stock: string, qnt: any, price: any, date: any, id: any): Observable<any> {
           // Convert 'portfolio_id', 'qnt', and 'price' to integers or floats
           const parsedPortfolioId = parseInt(portfolio_id);
           const parsedQuantity = parseInt(qnt, 10);
           const parsedPrice = parseFloat(price);
       
           const credentials = {
               jwtToken: token,
               user: user,
               portfolio_id: parsedPortfolioId,
               stock: stock,
               qnt: parsedQuantity,
               price: parsedPrice,
               date: date
           };
       
           console.log(credentials);
           return this.http.put<any>(`${this.url}${id}`, credentials);
    }
}
