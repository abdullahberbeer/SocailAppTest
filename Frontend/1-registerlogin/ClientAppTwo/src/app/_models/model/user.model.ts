import { Image} from './image.model'
export interface User{
  id:number,
  username:string,
  name:string,
    age:number,
    gender:string,
    created:Date,
    lastActive:Date,
    city:string,
    country:string,
    introduction:string,
    hobbies:string,
    profileImage:string,
    profileImageUrl:string,
    images:Image[]
}
