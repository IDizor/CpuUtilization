import { Component, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { ApiResponse } from './models/api-response';
import { CpuStatus } from './models/cpu-status';
import { PaginationComponent } from './pagination/pagination.component';
import { CpuUtilizationService } from './services/cpu-utilization.service';
import { PaginationOptions } from './models/pagination-options';
import { Helpers } from './helpers/helpers';
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
    pagerOptions = new PaginationOptions();

    ngOnInit() {
        this.cpuUtilizationService.getApiStatus().subscribe((response: ApiResponse<string>) => {
            this.status = response.data;
        });

        this.getCpuUtilization();
    };

    getCpuUtilization(): void {
        this.cpuUtilizationService.getCpuUtilization(this.pagerOptions.offset, this.pagerOptions.pageSize).subscribe((response: ApiResponse<Array<CpuStatus>>) => {
            this.cpuUtilizationRecords = response.data.map(cs => Helpers.updateObject(new CpuStatus(), cs));
            this.pagerOptions.totalCount = response.totalCount;
        });
    };
}
