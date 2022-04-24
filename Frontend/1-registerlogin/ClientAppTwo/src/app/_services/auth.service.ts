import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

import { LoginDto } from '../_models/dto/login.dto';
import { map } from 'rxjs/operators';
import { RegisterDto } from '../_models/dto/register.dto';

import { User } from '../_models/model/user.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl:string="https://localhost:5001/api/auth/";
  jwtHelper=new JwtHelperService();
  decodedToken:any;
  constructor(private http:HttpClient) { }
login(login:LoginDto){
  return this.http.post(this.baseUrl+"login",login).pipe(
    map((response:any)=>{
      const result = response;
      if(result){
          localStorage.setItem("token",result.token)
          this.decodedToken=this.jwtHelper.decodeToken(result.token)
      }
    })
    )
}
  register(model:RegisterDto){

    return this.http.post(this.baseUrl+"register",model);

  }
  loggedIn(){
    const token = localStorage.getItem("token")
    return !this.jwtHelper.isTokenExpired(token!);
  }
}
