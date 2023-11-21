import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
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

      this.router.events.subscribe((event) => {
        if (event instanceof NavigationEnd) {
          // Reset all active links
          document.querySelectorAll('.navbar-item').forEach((link) => {
            link.classList.remove('active');
          });

          // Get the active link and add active class
          const activeLink = document.querySelector(`.navbar-item[href='${this.router.url}']`);
          if (activeLink) {
            activeLink.classList.add('active');
          } else {
            // If no link matches the current route, activate the Home link
            const homeLink = document.querySelector(`.navbar-item[href='/']`);
            if (homeLink) {
              homeLink.classList.add('active');
            }
          }
        }
      });



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