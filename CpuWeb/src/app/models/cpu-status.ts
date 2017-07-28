export class CpuStatus {
    pcName: string = "";
    usage: number = 0;
    timeStamp: Date = null;

    constructor(pcName: string, usage: number, timeStamp: Date) {
        this.pcName = pcName;
        this.usage = usage;
        this.timeStamp = timeStamp;
    }

    get cssColorClass(): string {
        return this.usage > 70
            ? "danger"
            : this.usage >= 30
                ? "warning"
                : "success";
    }
}
