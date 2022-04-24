import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { RegisterDto } from '../_models/dto/register.dto';
import { Router } from '@angular/router';
import { User } from '../_models/model/user.model';

@Component({
  selector: 'register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 model:RegisterDto={
  name:'',
  username:'',
  email:'',
  gender:'',
  password:'',
dateOfBirth:'',
  country:'',
  city:''

}
  constructor(private authService:AuthService,private alertifyy:AlertifyService,private router:Router) { }

  ngOnInit() {
  }
  register(){
    this.authService.register(this.model).subscribe(
      ()=>{
        this.alertifyy.success("Kayıt başarılı")

      },err=>{
        this.alertifyy.error(err)
      },()=>{
        this.authService.login(this.model).subscribe(()=>{
          this.router.navigate(['/members'])
        }
        )
      }
    )
  }


}
