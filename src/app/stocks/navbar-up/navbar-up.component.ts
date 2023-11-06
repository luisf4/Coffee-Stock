import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-navbar-up',
  templateUrl: './navbar-up.component.html',
  styleUrls: ['./navbar-up.component.css']
})
export class NavbarUpComponent implements OnInit {


  constructor(private auth: AuthService, private router: Router) { }

  // On acessing the page verify if the token is valid
  ngOnInit(): void {
    var token = localStorage.getItem("jwtToken");

    try {
      this.auth.verifyToken(token!).subscribe(Response => {
        if (Response == "ok") {
          console.log(Response);
        } else if (Response == "Token has expired") {
          this.router.navigate(['/login']);
        } else {
          console.log(Response)
          this.router.navigate(['/login']);
        }
      });
    } catch (e) {
      console.error(e);
      this.router.navigate(['/login']);
    }
  }
}