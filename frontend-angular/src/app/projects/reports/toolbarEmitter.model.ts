export class CellModel {
    public value: string = "";
    public name: string = "";
    public width: number = 100;

    public constructor(name: string = "", width: number) {
        this.name = name;
        this.width = width;
    }
}

export class RowModel {
    public cells: CellModel[] = [];
}

export class ColumnsModel {
    public columns: CellModel[] = [];
}

export class PagesModel {
    public title: string;
    public rows: RowModel[] = [];
    public groupLabels: string[] = [];
    public groupsValues: string[] = [];
}

export class ReportPageModel {
    public pages: PagesModel[] = [];
}