import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { CpuUtilizationService } from './services/cpu-utilization.service';
import { PaginationModule } from './pagination/pagination.module';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        CommonModule,
        PaginationModule,
        NgxPaginationModule
    ],
    providers: [CpuUtilizationService],
    bootstrap: [AppComponent]
})
export class AppModule { }
