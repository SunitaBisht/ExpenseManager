import { Component } from "@angular/core";

@Component({
  selector: 'app-root',
   templateUrl: './app.component.html',
   styleUrls: ['./app.component.css']
 })
 export class AppComponent {
   title = 'expense-manager-client';
  firstName: string = 'Sunita';
   lastName: string = "Bisht";
 }