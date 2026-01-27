import { Pipe, PipeTransform } from '@angular/core';
//import { _getComponentHostLElementNode } from '../../../node_modules/@angular/core/src/render3/instructions';

@Pipe({
    name: 'myfilterGeoTrackingAuto',
    pure: false
})
export class GeoTrackingPipe2 implements PipeTransform {
    transform(items: any[], filter: any): any {
        if (!items || !filter) {
            return items;
        }
        return items.filter(item => item.idfUser == filter.idfUser);
    }
}