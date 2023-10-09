import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/authServices';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent {
  username = new FormControl('');
  password = new FormControl('');

  constructor(private auth: AuthService) { }

  Register() {
    console.log("AAAAAAAAA")
    
    const usernameValue = this.username.value;
    const passwordValue = this.password.value;
    this.auth.createUser(usernameValue!, passwordValue!)
      .subscribe(response => {
        // Handle the response here
        console.log(response);
      });

  }
}
