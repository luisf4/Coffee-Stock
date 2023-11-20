import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class AuthService { // Renamed to AuthService (singular form)
  private url = "http://localhost:5019/auth/"; // Added "http://"

  constructor(private http: HttpClient) { }

  // Create user
  createUser(username: string, password: string): Observable<any> { // Renamed to camelCase
    const credentials = { Username: username, Password: password }; // Adjusted object structure
    return this.http.post<any>(this.url+"register", credentials);
  }

  // Login user
  loginUser(username: string, password: string): Observable<any> { 
    const credentials = { Username: username, Password: password};
    const res = this.http.post<any>(this.url+"login", credentials);
    return res;
  }

  // Verify if the token is valid 
  verifyToken(token: string): Observable<any> { 
    const credentials = { jwtToken: token };
    const res = this.http.post<any>(this.url+"jwt",credentials);
    return res;
  }

  // Use the token to get the user name
  getUsername(token: string): Observable<any> { 
    const credentials = { jwtToken: token };
    const res = this.http.post<any>(this.url+"user",credentials);
    return res;
  }
}
