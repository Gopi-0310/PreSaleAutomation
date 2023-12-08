import { Pipe, PipeTransform } from '@angular/core';
import { isArray } from 'chart.js/dist/helpers/helpers.core';

@Pipe({
  name: 'filterPipe'
})
export class FilterPipePipe implements PipeTransform {

  transform(value: any[], filterString: string): any[] {
    if(!value || value.length === 0){
      console.log("return data filter 0",value);
      console.log("return data filter 0",filterString);
      return value;
    }
    if(!filterString){
      return value;
    }
   filterString = filterString.toLocaleLowerCase();
   console.log("return data filter 1",filterString);
    return value.filter(item => item.name.toLowerCase().includes(filterString));
  }
  

}

