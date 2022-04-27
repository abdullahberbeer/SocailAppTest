import { Component, Injectable, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-message-create',
  templateUrl: './message-create.component.html',
  styleUrls: ['./message-create.component.css']
})
@Injectable({
  providedIn:"root"
})
export class MessageCreateComponent implements OnInit {
  @Input() recipientId!:number;
  message:any={};
  constructor(private activeModal:NgbActiveModal,private authService:AuthService,private userService:UserService,private router:Router ,private alertifyService:AlertifyService) { }

  ngOnInit(): void {
  }
closeModal(){
this.activeModal.close();
}
sendMessage(){
  this.message.recipientId=this.recipientId;
  this.userService.sendMessage(this.authService.decodedToken.nameid,this.message).subscribe(result=>{
    this.alertifyService.success("mesaj gönderildi")
    this.activeModal.close();
    this.router.navigate(['/messages'])
  },err=>{
    this.alertifyService.error("hata")
  })
}
}
