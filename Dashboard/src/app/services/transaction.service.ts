import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Transaction } from '../interfaces/transaction';
import { catchError, Observable, of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
    private readonly baseUrl: string = "https://localhost:7296/api/transactions" 

    constructor(private http: HttpClient) { }

    getTransactions(): Observable<Transaction[]> {
        return this.http
            .get<Transaction[]>(this.baseUrl)
            .pipe(catchError(() => of([])))
    }

    getTransaction(id: number): Observable<Transaction> {
        return this.http
            .get<Transaction>(`${this.baseUrl}/${id}`)
            .pipe(catchError(() => of())) 
    }

    createTransaction(transaction: Transaction): Observable<Transaction> {
        return this.http
            .post<Transaction>(this.baseUrl, transaction)
            .pipe(catchError(() => of())) 
    }

    updateTransaction(transaction: Transaction): Observable<Transaction> {
        return this.http
            .put<Transaction>(`${this.baseUrl}/${transaction.id}`, transaction)
            .pipe(catchError(() => of())) 
    }

    deleteTransaction(id: number) {
        this.http.delete(`${this.baseUrl}/${id}`)
    } 
}
