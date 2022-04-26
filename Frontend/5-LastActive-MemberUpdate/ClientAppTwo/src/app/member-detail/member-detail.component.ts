import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/model/user.model';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
user!:User;
  constructor(private userService:UserService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.route.data.subscribe(data=>{

      this.user=data.user;

    })
  }

}
