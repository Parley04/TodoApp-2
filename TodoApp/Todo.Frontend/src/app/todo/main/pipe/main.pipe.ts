import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'mainPipe',
})
export class MainPipe implements PipeTransform {

  transform(value: any, filterText: string): any {
    if (!filterText) {
      return value;
    }
  
    const searchText = filterText.toLocaleLowerCase();
  
    return value.filter((p:any) => {
      const title = p.title?.toLocaleLowerCase().includes(searchText);
      const description = p.description?.toLocaleLowerCase().includes(searchText);
      
      // Tags listesinde herhangi bir tag'in name değeri searchText içeriyor mu?
      const tagMatch = p.tags?.some((tag:any) => tag.name.toLocaleLowerCase().includes(searchText));
  
      return title || description || tagMatch;
    });
  }
  
}
