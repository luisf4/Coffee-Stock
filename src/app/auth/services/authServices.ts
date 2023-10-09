import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class AuthService { // Renamed to AuthService (singular form)
  private url = "http://localhost:5000/auth/"; // Added "http://"

  constructor(private http: HttpClient) { }

  createUser(username: string, password: string): Observable<any> { // Renamed to camelCase
    const credentials = { Username: username, Password: password }; // Adjusted object structure
    return this.http.post<any>(this.url+"register", credentials);
  }

  loginUser(username: string, password: string): Observable<any> { 
    const credentials = { Username: username, Password: password};
    return this.http.post<any>(this.url+"login", credentials)
  }
}
