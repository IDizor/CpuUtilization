import { Component, OnInit } from '@angular/core';
import { ApiResponse } from './models/api-response';
import { CpuStatus } from './models/cpu-status';
import { CpuUtilizationService } from './services/cpu-utilization.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    constructor(private cpuUtilizationService: CpuUtilizationService) { }

    status: string;
    cpuUtilizationRecords: Array<CpuStatus>;
    offset: number = 0;
    limit: number = 50;
    totalCount: number = 0;
    page: number = 1;

    ngOnInit() {
        this.cpuUtilizationService.getApiStatus().subscribe((response: ApiResponse<string>) => {
            this.status = response.data;
        });

        this.getCpuUtilization();
    };

    getCpuUtilization(): void {
        this.cpuUtilizationService.getCpuUtilization(this.offset, this.limit).subscribe((response: ApiResponse<Array<CpuStatus>>) => {
            this.cpuUtilizationRecords = response.data;
            this.totalCount = response.totalCount;
        });
    };
}
