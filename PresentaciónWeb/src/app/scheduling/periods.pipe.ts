import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'myfilterperiods',
    pure: false
})

export class MyFilterPipePeriods implements PipeTransform {
    transform(items: any[], filter: Object): any {
        if (!items || !filter) {
            return items;
        }

        return items.filter(item => item.abm.indexOf(filter["abm"]) == -1);
    }
}