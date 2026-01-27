import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'image'
})
export class ImagePipe implements PipeTransform {

  transform(imgname: string, imgtype: string = 'unidad'): any {
    let url = '/api/image';
    // #region TODO: devolver imágenes por defecto
    if (!imgname) {
      return url + '/error/error';
    }

    if (imgname.indexOf('http') >= 0) {
      return imgname;
    }

    console.log(imgtype);
    switch (imgtype) {
      case 'clients':
        url += '/clients/' + imgname;
        break;
      case 'users':
        url += '/users/' + imgname;
        break;
      case 'injuries':
        url += '/injuries/' + imgname;
        break;
      case 'clientFormValues':
        url += '/clientFormValues/' + imgname;
        break;
      case 'projectFormValues':
        url += '/projectFormValues/' + imgname;
        break;
      case 'staffFormValues':
        url += '/staffFormValues/' + imgname;
        break;
      default:
        url += '/error/error';
    }
    return url;
  }
}
