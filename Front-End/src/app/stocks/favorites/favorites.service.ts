import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class FavoritesServices {
  private url = "http://localhost:5019/favorites/";

  constructor(private http: HttpClient) {}

  getFavorites(user: string): Observable<any> {
    return this.http.get<any>(this.url + user);
  }
  addFavorites(user: string, symbol: string): Observable<any> {
    return this.http.post<any>(this.url + symbol+ "/"  + user,'');
  }
  removeFavorites(user: string, symbol: string): Observable<any> {
    return this.http.delete<any>(this.url + symbol+ "/" + user);
  }
}
