import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BlacklistEmailModel } from '../../models/blacklist-email.model';
import { BLACKLIST_EMAILS } from 'src/app/constants/api.constants';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: "root"
})
export class BlacklistEmailsService {
    constructor(private http: HttpClient) { }

    public getAllBlacklistEmails(): Observable<BlacklistEmailModel[]> {
        return this.http.get<BlacklistEmailModel[]>(BLACKLIST_EMAILS);
    }

    public addBlacklistEmail(email: string): Observable<BlacklistEmailModel> {
        return this.http.post<BlacklistEmailModel>(BLACKLIST_EMAILS, {
            email
        });
    }

    public removeBlacklistEmail(id: string) {
        return this.http.delete(BLACKLIST_EMAILS + `/${id}`);
    }
}