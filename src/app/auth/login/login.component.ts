import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AuthService } from '../services/authServices';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = new FormControl('');
  password = new FormControl('');

  constructor(private auth: AuthService) {  }

  Login() {  
    const usernameValue = this.username.value;
    const passwordValue = this.password.value;
    this.auth.loginUser(usernameValue!, passwordValue!)
      .subscribe(response => {
        // Handle the response here
        console.log(response);
      });

  }
}
