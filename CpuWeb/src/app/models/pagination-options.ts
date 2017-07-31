export class PaginationOptions {
    page: number = 1;
    pageSize: number = 50;
    pageSizes: number[] = [10, 20, 50, 100];
    totalCount: number = 0;

    get offset(): number {
        return (this.page - 1) * this.pageSize;
    }
}
