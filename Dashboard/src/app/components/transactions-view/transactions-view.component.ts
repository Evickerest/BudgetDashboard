import { Component } from '@angular/core';

@Component({
    selector: 'app-transactions-view',
    standalone: false,
    template: `
        <div *ngFor="let transaction of transactions"
    ` ,
    styleUrl: './details-view.component.css'
})
export class TransactionsViewComponent {

    
}
