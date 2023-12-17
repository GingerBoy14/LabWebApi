import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  cards: any[] = [
    {
      title: 'Users List',
      content: 'List of all users in system',
      path: 'users-list',
    },
  ];
  constructor() {}
  ngOnInit() {}
}
