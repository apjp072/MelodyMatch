import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  

  constructor(public accountService: AccountService, private router: Router, 
    private toastr: ToastrService) {}

  ngOnInit(): void {

  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

  showLoginButton(): boolean {
    // Check if the user is logged out and the current URL is the login page
    return this.router.url !== '/login';
  }

}
