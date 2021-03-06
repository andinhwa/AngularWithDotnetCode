import { Component, OnInit } from '@angular/core';
import { AlertService} from '../../_services';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})
export class AlertComponent implements OnInit {

  private subscription: Subscription;
    message: any;

    constructor(private alertService: AlertService) { }

    ngOnInit() {
        this.subscription = this.alertService.getMessage().subscribe(message => {
            this.message = message;
        });
    }

// tslint:disable-next-line: use-life-cycle-interface
    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
