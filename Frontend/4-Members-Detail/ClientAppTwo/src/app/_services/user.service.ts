import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/model/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
baseUrl:string='https://localhost:5001/api/user/';
  constructor(private http:HttpClient) { }

  getUsers(followParams?:any,userParams?:any):Observable<User[]>{
    let params = new HttpParams();

    if(followParams==="follewers"){
      params=params.append("followers",true)
    }
    if(followParams==="followings"){
      params=params.append("followings",true)

    }


    if(userParams!=null){
      if(userParams.gender !=null){
        params=params.append('gender',userParams.gender)
      }
      if(userParams.minAge!=null)
      params=params.append('minAge',userParams.minAge)

      if(userParams.maxAge!=null)
      params=params.append('maxAge',userParams.maxAge)

      if(userParams.country!=null)
      params=params.append('country',userParams.country)

      if(userParams.city!=null)
      params=params.append('city',userParams.city)
      if(userParams.orderby!=null)
      params=params.append('orderby',userParams.orderby)
    }

    return this.http.get<User[]>(this.baseUrl,{params:params})
  }
  getUser(id:number):Observable<User>{
    return this.http.get<User>(this.baseUrl+id);
  }
}
