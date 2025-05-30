import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { Account } from '../interfaces/account';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
    private readonly baseUrl: string = "https://localhost:7296/api/accounts"

    constructor(private http: HttpClient) { }

    getAccounts(): Observable<Account | undefined> {
        return this.http
            .get<Account>(this.baseUrl)
            .pipe(catchError(() => of(undefined)))
    }

    getAccount(id: number): Observable<Account | undefined> {
        return this.http
            .get<Account>(`${this.baseUrl}/${id}`)
            .pipe(catchError(() => of(undefined))) 
    }

    createAccount(account: Account): Observable<Account | undefined> {
        return this.http
            .post<Account>(this.baseUrl, account)
            .pipe(catchError(() => of(undefined))) 
    }

    updateAccount(account: Account): Observable<Account | undefined> {
        return this.http
            .put<Account>(`${this.baseUrl}/${account.id}`, account)
            .pipe(catchError(() => of(undefined))) 
    }

    deleteAccount(id: number) {
        this.http.delete(`${this.baseUrl}/${id}`)
    } 
}
