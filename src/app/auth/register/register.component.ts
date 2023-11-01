import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent {
  username = new FormControl('');
  password = new FormControl('');

  constructor(private auth: AuthService, private router: Router) { }

  Register() {
    // Get input values
    const usernameValue = this.username.value;
    const passwordValue = this.password.value;

    // Make the request 
    this.auth.createUser(usernameValue!, passwordValue!)
      .subscribe(response => {
        // Log the response
        console.log(response),
          // Storing the JWT in LocalStorage
          localStorage.setItem('jwtToken', response);
          this.router.navigate(['/login']);
      });

  }
}
