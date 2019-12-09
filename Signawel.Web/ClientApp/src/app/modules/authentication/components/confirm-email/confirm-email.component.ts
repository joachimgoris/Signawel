import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.sass']
})
export class ConfirmEmailComponent implements OnInit {

  private loading: boolean;
  private success: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthenticationService
  ) {
    this.loading = true;
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.authService.confirmEmail(params['userId'], params['token']).subscribe(success => {
        this.loading = false;
        this.success = success;
      })
    });
  }
}
