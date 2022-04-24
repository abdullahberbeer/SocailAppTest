import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../alertify.service';
import { User } from '../_models/model/user.model';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
users:User[]=[];
userParams:any={};

  constructor(private userService: UserService,private alertify:AlertifyService,private authService:AuthService) { }

  ngOnInit(): void {

    this.userParams.orderby="lastactive";
    this.userParams.gender="male";
    this.userParams.minAge=18;
    this.userParams.maxAge=50;
    this.getUsers();
  }

  getUsers(){
    return this.userService.getUsers(null,this.userParams).subscribe(users=>{
      this.users=users;
    }

    )
  }

}
