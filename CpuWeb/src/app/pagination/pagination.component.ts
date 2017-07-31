import { Component, Input, Output, EventEmitter } from '@angular/core';
import { PaginationOptions } from '../models/pagination-options';

@Component({
    selector: 'pages',
    templateUrl: './pagination.component.html'
})
export class PaginationComponent {
    constructor() { }

    @Input() @Output() options: PaginationOptions;
    @Output() onChange = new EventEmitter<any>();

    onUpdate(page: number): void {
        this.options.page = page;
        this.onChange.emit();
    }
}
