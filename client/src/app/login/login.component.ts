import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: any = {};
  baseUrl = 'https://localhost:5001/api/'; //hmm
  validationErrors: string[] = []; //hmm

  constructor(public accountService: AccountService, private router: Router, 
    private toastr: ToastrService, private http: HttpClient) {}

  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl('/members'), //says were not using an argument for this particular method
    })
  }
}
