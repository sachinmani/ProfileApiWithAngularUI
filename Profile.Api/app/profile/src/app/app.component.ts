import { Component } from '@angular/core';
import {Profile} from './profile'
import { 
  ProfileService 
} from './app.service'
import { Router }  from '@angular/router';  

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[ProfileService]
})
export class AppComponent {
  model = new Profile('','');
  profiles: Profile[] = [];
  constructor(private _profile: ProfileService, private _router: Router) {
  }

  ngOnInit() : void {
    this._profile.getAll()
    .subscribe(profiles => {this.profiles = profiles;
    console.log(JSON.stringify(profiles))});
 }

 onSubmit(){
   console.log("submit Invoked");
   this._profile.addProfile(this.model).subscribe(()=> this.getAll());
 }

 getAll(){
  this._profile.getAll().subscribe(profiles => {this.profiles = profiles;
    console.log(JSON.stringify(profiles))});;
 }

}
