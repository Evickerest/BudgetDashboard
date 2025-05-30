import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { TransactionType } from '../interfaces/transaction-type';

@Injectable({
  providedIn: 'root'
})
export class TransactionTypeService {
    private readonly baseUrl: string = "https://localhost:7296/api/transaction-types"

    constructor(private http: HttpClient) { }

    getTransactionTypes(): Observable<TransactionType | undefined> {
        return this.http
            .get<TransactionType>(this.baseUrl)
            .pipe(catchError(() => of(undefined)))
    }

    getTransactionType(id: number): Observable<TransactionType | undefined> {
        return this.http
            .get<TransactionType>(`${this.baseUrl}/${id}`)
            .pipe(catchError(() => of(undefined))) 
    }

    createTransactionType(transactionType: TransactionType): Observable<TransactionType | undefined> {
        return this.http
            .post<TransactionType>(this.baseUrl, transactionType)
            .pipe(catchError(() => of(undefined))) 
    }

    updateTransactionType(transactionType: TransactionType): Observable<TransactionType | undefined> {
        return this.http
            .put<TransactionType>(`${this.baseUrl}/${transactionType.id}`, transactionType)
            .pipe(catchError(() => of(undefined))) 
    }

    deleteTransactionType(id: number) {
        this.http.delete(`${this.baseUrl}/${id}`)
    } 
}
