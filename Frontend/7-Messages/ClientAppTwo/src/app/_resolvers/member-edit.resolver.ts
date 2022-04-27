import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";

import { User } from "../_models/model/user.model";
import { AlertifyService } from "../_services/alertify.service";
import { AuthService } from "../_services/auth.service";
import { UserService } from "../_services/user.service";
@Injectable()
export class MemberEditResolver implements Resolve<User>{

  constructor(private userService:UserService,private alertifyService:AlertifyService ,private authService:AuthService,private router:Router){

  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
    return this.userService.getUser(this.authService.decodedToken.nameid).pipe(
      catchError((error)=>{
        this.router.navigate(['/members'])
        return of(error);
      })
    )
  }

}
