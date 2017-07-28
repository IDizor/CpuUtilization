export class CpuStatus {
    pcName: string = "";
    usage: number = 0;
    timeStamp: Date = null;

    constructor(pcName: string, usage: number, timeStamp: Date) {
        this.pcName = pcName;
        this.usage = usage;
        this.timeStamp = timeStamp;
    }

    get color(): string {
        return this.usage > 70
            ? "red"
            : this.usage >= 30
                ? "yellow"
                : "green";
    }
}
