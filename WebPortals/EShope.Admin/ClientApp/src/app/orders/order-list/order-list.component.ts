import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';


@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  public orderList: string[] = [];
  private hubConnection: HubConnection;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }
  private createConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/ordersHub').build();
  }
  private registerOnServerEvents(): void {
    this.hubConnection.on('NewOrder', (data: string) => {
      this.orderList.push(data + ' -- ' + new Date().toTimeString());
    });
  }
  private startConnection() {
    setTimeout(() => {
      this.hubConnection.start().then(() => {
        console.log('Hub connection started');
      });
    }, 2000);
  }
  ngOnInit() {
      this.http.get<string[]>(this.baseUrl + 'api/Orders').subscribe(list => {
        this.orderList = list;
      }, error => console.error(` ----- Erorrrrrr ----- ${error}`));
  }

}
