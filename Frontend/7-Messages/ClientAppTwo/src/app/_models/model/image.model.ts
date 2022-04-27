import { User } from "./user.model";

export interface Image{
id:number,
name:string,
description:string,
dateAdded:Date,
isProfile:boolean,
userId:number,
user:User
}
