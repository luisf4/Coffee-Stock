import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  
  constructor(private auth: AuthService, private router: Router) {
  }
  
  // On acessing the page verify if the token is valid
  ngOnInit(): void {
    var token = localStorage.getItem("jwtToken");

    try { 
      this.auth.verifyToken(token!).subscribe(Response => { 
        if (Response == "ok"){
          console.log(Response);
        } else if (Response == "Token has expired") {
          this.router.navigate(['/login']);
        } else { 
          console.log(Response)
        }
      });
    }catch(e){
      console.error(e);
    }



  }
}
