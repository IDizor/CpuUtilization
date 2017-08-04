export class CpuStatus {
    pcName: string = "";
    usage: number = 0;
    timeStamp: Date = null;

    get cssColorClass(): string {
        return this.usage > 70
            ? "danger"
            : this.usage >= 30
                ? "warning"
                : "success";
    }
}
