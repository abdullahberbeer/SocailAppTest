import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from '../_models/dto/login.dto';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})


export class NavbarComponent implements OnInit {

  loginDto:LoginDto={
    username:'',
    password:''
  };
  constructor(public authService:AuthService,private alertify:AlertifyService,private router:Router) { }

  ngOnInit(): void {
  }

  login(){
    this.authService.login(this.loginDto).subscribe(data=>{
      this.alertify.success("Giriş başarılı")
      this.router.navigate(["/members"])
    })
  }
loggedIn(){
  return this.authService.loggedIn()
}
logout(){
  localStorage.removeItem("token")
  this.alertify.success("Çıkış yapıldı")
  this.router.navigate(['/home'])
}
}
