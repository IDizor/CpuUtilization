import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ApiResponse } from '../models/api-response';
import { CpuStatus } from '../models/cpu-status';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class CpuUtilizationService {
    constructor(private http: Http) { }

    getApiStatus(): Observable<ApiResponse<string>> {
        let response = this.http.get('/api/cpu/status').map((res: Response) => res.json() as ApiResponse<string>);
        return response;
    };

    getCpuUtilization(offset: number, limit: number): Observable<ApiResponse<Array<CpuStatus>>> {
        let response = this.http.get("/api/cpu/utilization"
            + "?offset=" + (offset || 0)
            + "&limit=" + (limit || 50)
        ).map((res: Response) => res.json() as ApiResponse<Array<CpuStatus>>);

        return response;
    };
}
