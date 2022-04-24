import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { AlertifyService } from "../alertify.service";
import { User } from "../_models/model/user.model";
import { UserService } from "../_services/user.service";

@Injectable()
export class MemberDetailResolver implements Resolve<User>{
  constructor(private userService:UserService,private alertify:AlertifyService,private router:Router)
{

}
 resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {

return this.userService.getUser(route.params["id"]).pipe(
  catchError((error)=>{

    this.router.navigate(['/members'])
    return of(error)
  })

)


  }

}
