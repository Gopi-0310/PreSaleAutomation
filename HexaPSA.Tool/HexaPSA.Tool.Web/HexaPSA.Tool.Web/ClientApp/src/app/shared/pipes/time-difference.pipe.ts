import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeDifference'
})
export class TimeDifferencePipe implements PipeTransform {

  transform(startDate: Date, endDate: Date, unit:string): string {
    // Convert date strings to Date objects
    const startDateObj = new Date(startDate);
    const endDateObj = new Date(endDate);

    // Check if the conversion was successful - NaN - Not a Number
    if (isNaN(startDateObj.getTime()) || isNaN(endDateObj.getTime())) 
      return 'Invalid date';
    
    const timeDifference = endDateObj.getTime() - startDateObj.getTime();
    
    switch(unit)
    {
      case 'hour' : return `${(timeDifference / (1000 * 60 * 60)).toFixed(2)} hours`;
      case 'week' : return `${(timeDifference / (1000 * 60 * 60 * 24 * 7)).toFixed(2)} weeks`;
      case 'week-days':
        const weeks = Math.floor(timeDifference / (1000 * 60 * 60 * 24 * 7));
        const days = Math.floor((timeDifference % (1000 * 60 * 60 * 24 * 7)) / (1000 * 60 * 60 * 24));
        if (weeks === 0) {
          return `${days} ${days === 1 ? 'day' : 'days'}`;
        } else if (days === 0) {
          return `${weeks} ${weeks === 1 ? 'week' : 'weeks'}`;
        } else {
          return `${weeks} ${weeks === 1 ? 'week' : 'weeks'} ${days} ${days === 1 ? 'day' : 'days'}`;
        }
      default     : return 'Invalid date';
    }
  }

}
