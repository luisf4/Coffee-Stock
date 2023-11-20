import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-navbar-up',
  templateUrl: './navbar-up.component.html',
  styleUrls: ['./navbar-up.component.css']
})
export class NavbarUpComponent implements OnInit {
  isLoggedIn = false;
  userName = '';
  isLoading = true; // Add loading indicator


  constructor(private auth: AuthService, private router: Router) { }

  // On acessing the page verify if the token is valid
  ngOnInit(): void {
    try {
      var token = localStorage.getItem("jwtToken");
      this.auth.getUsername(token!).subscribe(
        Response =>
          this.userName = Response
      )

      this.auth.verifyToken(token!)
        .pipe(
          finalize(() => { this.isLoading = false; }) // Set isLoading to false on completion
        )
        .subscribe(Response => {
          if (Response == "ok") {
            console.log("Login OK");
            this.isLoggedIn = true;
          } else if (Response == "Token has expired") {
            this.router.navigate(['/login']);
          } else {
            this.router.navigate(['/login']);
          }
        });
    } catch (e) {
      console.error(e);
      this.router.navigate(['/login']);
    }
  }

  clearToken() {
    localStorage.removeItem('jwtToken');
  }
}