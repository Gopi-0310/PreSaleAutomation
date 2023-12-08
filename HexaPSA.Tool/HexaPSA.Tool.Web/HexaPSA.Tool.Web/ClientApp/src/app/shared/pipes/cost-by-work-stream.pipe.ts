import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'costByWorkStream'
})
export class CostByWorkStreamPipe implements PipeTransform {
  groupedData : any;
  transform(items: any[]): any[] {
    if (!items) return [];

    const result = [];

    items.forEach(item => {
      const key = item.workStream.id;
      if (!this.groupedData[key]) {
        this.groupedData[key] = [];
      }
      this.groupedData[key].push(item);
    });

    for (const workStreamId in this.groupedData) {
      if (this.groupedData.hasOwnProperty(workStreamId)) {
        const group = this.groupedData[workStreamId];
        const overallRate = group.reduce((sum: number, item: { rate: { rate: number; }; hours: number; }) => sum + (item.rate.rate * item.hours), 0) / group.reduce((sum: any, item: { hours: any; }) => sum + item.hours, 0);
        const overallHours = group.reduce((sum: any, item: { hours: any; }) => sum + item.hours, 0);

        const summary = {
          workStreamId: workStreamId,
          overallRate: overallRate,
          overallHours: overallHours
        };
        result.push(summary);
      }
    }
     console.log("pipes",result);
    return result;
  }
}
