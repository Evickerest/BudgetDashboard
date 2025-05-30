import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Transaction } from '../../interfaces/transaction';
import { TransactionService } from '../../services/transaction.service';

@Component({
    selector: 'app-transactions-view',
    standalone: false,
    template: './transactions-view.component.html',
    styleUrl: './transactions-view.component.css'
})
export class TransactionsViewComponent {
    transactions$: Observable<Transaction[]>;

    constructor(private transactionService: TransactionService) { }
        
    ngOnInit() {
        this.transactions$ = this.transactionService.getTransactions();
    } 
}
