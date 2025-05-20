import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../Environments/environment";

@Injectable({
    providedIn: "root",  
})

export class KenoService {
    private getOdds = environment.apiUrl + '/Keno/getOdds';
    private getRandomPlayerNumbers = environment.apiUrl + '/Keno/getRandomPlayerNumbers';
    private playGame = environment.apiUrl + '/Keno/playGame';

    constructor(private http: HttpClient) {}

}