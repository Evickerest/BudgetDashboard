import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class APIService {
    private readonly baseUrl: string = "https://localhost:7296/api"; 

    constructor(private http: HttpClient) { }

    getAll<T>(route: string): Observable<T[]> { 
        return this.http
            .get<T[]>(`${this.baseUrl}/${route}`)
            .pipe(
                catchError(() => of([])));
    }

    getById<T>(route: string, id: number): Observable<T> { 
        return this.http
            .get<T>(`${this.baseUrl}/${route}/${id}`)
            .pipe(
                catchError(() => of()));
    }

    create<T>(route: string, t: T) {
        this.http.post<T>(`${this.baseUrl}/${route}`, t)
    }

    update<T>(route: string, id: number, t: T) {
        this.http.put<T>(`${this.baseUrl}/${route}/${id}`, t)
    }

    delete(route: string, id: number) {
        this.http.delete(`${this.baseUrl}/${route}/${id}`)
    }
}
