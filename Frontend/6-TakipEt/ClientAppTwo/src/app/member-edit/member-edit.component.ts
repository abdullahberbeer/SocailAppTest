import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { User } from '../_models/model/user.model';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
user!:User;
  constructor(private userService:UserService,private route:ActivatedRoute,private authService:AuthService,private alertify:AlertifyService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{
      this.user=data.user;
    })
  }
  updateUser(){
    this.userService.updateUser(this.authService.decodedToken.nameid,this.user).subscribe(()=>{
      this.alertify.success("Güncelleme başarılı")

    },err=>{
      this.alertify.warning(err)
    })
  }

}
