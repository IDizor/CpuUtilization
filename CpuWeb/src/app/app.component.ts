import { Component, OnInit } from '@angular/core';
import { ApiResponse } from './models/api-response';
import { CpuStatus } from './models/cpu-status';
import { CpuUtilizationService } from './services/cpu-utilization.service';
import { NgClass } from '@angular/common';
import 'rxjs/add/operator/map';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(private cpuUtilizationService: CpuUtilizationService) { }

    status: string;
    cpuUtilizationRecords: Array<CpuStatus>;
    page: number = 0;
    pageSize: number = 50;
    totalCount: number = 0;

    ngOnInit() {
        this.cpuUtilizationService.getApiStatus().subscribe((response: ApiResponse<string>) => {
            this.status = response.data;
        });

        this.getCpuUtilization();
    };

    getCpuUtilization(p: number = 1): void {
        this.page = p;
        let offset = this.page * this.pageSize - this.pageSize;

        this.cpuUtilizationService.getCpuUtilization(offset, this.pageSize).subscribe((response: ApiResponse<Array<CpuStatus>>) => {
            this.cpuUtilizationRecords = response.data.map(cs => new CpuStatus(cs.pcName, cs.usage, cs.timeStamp));
            this.totalCount = response.totalCount;
        });
    };
}
