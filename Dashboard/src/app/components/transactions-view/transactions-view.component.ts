import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../../interfaces/transaction';
import { APIService } from '../../services/api.service';

@Component({
    selector: 'app-transactions-view',
    standalone: false,
    templateUrl: './transactions-view.component.html',
    styleUrl: './transactions-view.component.css'
})
export class TransactionsViewComponent {
    transactions$: Observable<Transaction[]>;

    constructor(private apiService: APIService) { } 
        
    ngOnInit() {
        this.transactions$ = this.apiService.getAll<Transaction>("transactions");
    } 
}
