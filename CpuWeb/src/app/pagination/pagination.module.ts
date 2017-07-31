import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from './pagination.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        PaginationComponent
    ],
    imports: [
        BrowserModule,
        CommonModule,
        NgxPaginationModule,
        FormsModule
    ],
    exports: [PaginationComponent],
    bootstrap: [PaginationComponent]
})
export class PaginationModule { }
