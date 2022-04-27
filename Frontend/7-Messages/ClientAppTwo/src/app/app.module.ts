import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import{HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { NavbarComponent } from './navbar/navbar.component';
import { MembersComponent } from './members/members.component';
import { MemberDetailComponent } from './member-detail/member-detail.component';
import { MemberEditComponent } from './member-edit/member-edit.component';
import { UndefinedComponent } from './undefined/undefined.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MessagesComponent } from './messages/messages.component';
import { FriendsComponent } from './friends/friends.component';
import { ErrorInterceptor } from './_services/error.intercaptor';
import { RouterModule } from '@angular/router';
import { TimeagoModule } from 'ngx-timeago';

import { appRoute } from './routes';
import { FormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { AuthGuard } from './_guards/auth-guard';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { PhotoGalleryComponent } from './photo-gallery/photo-gallery.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MessageCreateComponent } from './message-create/message-create.component';

export function tokenGetter(){
  return localStorage.getItem("token")
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MembersComponent,
    MemberDetailComponent,
    MemberEditComponent,
    UndefinedComponent,
    HomeComponent,
    RegisterComponent,
    MessagesComponent,
    FriendsComponent,
    PhotoGalleryComponent,
    MessageCreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoute),
    TimeagoModule.forRoot(),

    JwtModule.forRoot({
      config:{
        tokenGetter:tokenGetter,
        allowedDomains:["localhost:5001"],
        disallowedRoutes:["localhost:5001/api/auth"]
      }
    }),
    NgbModule
  ],
  providers: [AuthGuard,{
    provide:HTTP_INTERCEPTORS,
    useClass:ErrorInterceptor,
    multi:true
  },MemberDetailResolver,MemberEditResolver],
  bootstrap: [AppComponent]
})
export class AppModule { }
