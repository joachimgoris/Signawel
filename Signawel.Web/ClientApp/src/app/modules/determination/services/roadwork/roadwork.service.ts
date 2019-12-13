import { Injectable } from '@angular/core';
import { ROADWORK_INFO } from 'src/app/constants/api.constants';
import { HttpClient } from '@angular/common/http';
import { RoadworkAssignmentModel } from '../../models/roadwork-assignment.model';

@Injectable({
    providedIn: 'root'
})
export class RoadworkService {
    constructor(private http: HttpClient) { }

    getRoadworkAssignment(roadworkId: number) {
        return this.http.get<RoadworkAssignmentModel>(ROADWORK_INFO + `${roadworkId}`);
    }
}