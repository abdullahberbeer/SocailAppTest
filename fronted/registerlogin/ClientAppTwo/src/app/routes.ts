import { Routes } from "@angular/router";
import { FriendsComponent } from "./friends/friends.component";
import { HomeComponent } from "./home/home.component";
import { MemberDetailComponent } from "./member-detail/member-detail.component";
import { MemberEditComponent } from "./member-edit/member-edit.component";
import { MembersComponent } from "./members/members.component";
import { MessagesComponent } from "./messages/messages.component";
import { UndefinedComponent } from "./undefined/undefined.component";
import { AuthGuard } from "./_guards/auth-guard";

export const appRoute:Routes=[
  {path:'',component:HomeComponent},

  {path:'',component:HomeComponent},
  {path:'home',component:HomeComponent},
  {path:'members',component:MembersComponent,canActivate:[AuthGuard]},
  {path:'members/edit',component:MemberEditComponent,canActivate:[AuthGuard]},
  {path:'members/:id',component:MemberDetailComponent,canActivate:[AuthGuard]},
  {path:'friends',component:FriendsComponent,canActivate:[AuthGuard]},
  {path:'messages',component:MessagesComponent,canActivate:[AuthGuard]},
  {path:'**',component:UndefinedComponent}
]
