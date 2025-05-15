import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Game } from "../../Models/games.model";
import { environment } from "../../Environments/environment";
import { YatzyGame } from "../../Models/Yatzy.model";

@Injectable({
    providedIn: 'root',
})
export class YatzyService {
     private apiUrl = environment.apiUrl + '/YatzyGame/playGame';

    constructor(private http: HttpClient) { }

    playGame(betAmount: number): Observable<YatzyGame> { // <-- opdateret type
        return this.http.post<YatzyGame>(this.apiUrl, { betAmount });
    }
}