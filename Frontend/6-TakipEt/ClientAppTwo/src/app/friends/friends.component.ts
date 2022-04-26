import { Component, OnInit } from '@angular/core';
import { User } from '../_models/model/user.model';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-friends',
  templateUrl: './friends.component.html',
  styleUrls: ['./friends.component.css']
})
export class FriendsComponent implements OnInit {

  users!:User[];
  followParams:string="followings";
  constructor(private userService:UserService,private alertify:AlertifyService) { }

  ngOnInit(): void {
  this.getUsers();
  }


  getUsers(){
    return this.userService.getUsers(this.followParams).subscribe(users=>{
      this.users=users;
    },err=>{
      this.alertify.error(err);
    })
  }


}
