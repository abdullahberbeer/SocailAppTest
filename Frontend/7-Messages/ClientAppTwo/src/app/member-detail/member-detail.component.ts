import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageCreateComponent } from '../message-create/message-create.component';
import { User } from '../_models/model/user.model';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
user!:User;
followTexT:string="Takip Et"
  constructor(private userService:UserService,private route:ActivatedRoute,private authService:AuthService,private alertfy:AlertifyService,private modalService:NgbModal) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{

      this.user=data.user;

    })
  }
  followUser(userId:number){
    this.userService.followerUser(this.authService.decodedToken.nameid,userId).subscribe(result=>{
      this.alertfy.success(this.user.name + ' kullanıcısını takip ediyorsunuz');
      this.followTexT="Takip ediyorsunuz"
    },err=>{
      this.alertfy.error(err);
    }

    )
  }
  openSendMessageModal(){
const modalRef = this.modalService.open(MessageCreateComponent);
modalRef.componentInstance.recipientId = this.user.id;
  }
}
