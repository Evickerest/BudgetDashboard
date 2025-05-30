import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { Budget } from '../interfaces/budget';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
    private readonly baseUrl: string = "https://localhost:7296/api/budgets"

    constructor(private http: HttpClient) { }

    getBudgets(): Observable<Budget | undefined> {
        return this.http
            .get<Budget>(this.baseUrl)
            .pipe(catchError(() => of(undefined)))
    }

    getBudget(id: number): Observable<Budget | undefined> {
        return this.http
            .get<Budget>(`${this.baseUrl}/${id}`)
            .pipe(catchError(() => of(undefined))) 
    }

    createBudget(budget: Budget): Observable<Budget | undefined> {
        return this.http
            .post<Budget>(this.baseUrl, budget)
            .pipe(catchError(() => of(undefined))) 
    }

    updateBudget(budget: Budget): Observable<Budget | undefined> {
        return this.http
            .put<Budget>(`${this.baseUrl}/${budget.id}`, budget)
            .pipe(catchError(() => of(undefined))) 
    }

    deleteBudget(id: number) {
        this.http.delete(`${this.baseUrl}/${id}`)
    } 
}
