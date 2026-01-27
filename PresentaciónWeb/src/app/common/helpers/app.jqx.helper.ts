import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

// const jwtHelper = new JwtHelperService();

@Injectable()
export class JqxHelper {

    constructor() { }

    getScheduleLocation_en: any = {
        '/': "/",
        ':': ":",
        firstDay: 0,
        days: {
            names: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            namesAbbr: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
            namesShort: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"]
        },
        months: {
            names: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", ""],
            namesAbbr: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", ""]
        },
        AM: ["AM", "am", "AM"],
        PM: ["PM", "pm", "PM"],
        eras: [
            { "name": "A.D.", "start": null, "offset": 0 }
        ],
        twoDigitYearMax: 2029,
        patterns: {
            d: "M/d/yyyy",
            D: "dddd, MMMM dd, yyyy",
            t: "h:mm tt",
            T: "h:mm:ss tt",
            f: "dddd, MMMM dd, yyyy h:mm tt",
            F: "dddd, MMMM dd, yyyy h:mm:ss tt",
            M: "MMMM dd",
            Y: "yyyy MMMM",
            S: "yyyy\u0027-\u0027MM\u0027-\u0027dd\u0027T\u0027HH\u0027:\u0027mm\u0027:\u0027ss",
            ISO: "yyyy-MM-dd hh:mm:ss",
            ISO2: "yyyy-MM-dd HH:mm:ss",
            d1: "dd.MM.yyyy",
            d2: "dd-MM-yyyy",
            d3: "dd-MMMM-yyyy",
            d4: "dd-MM-yy",
            d5: "H:mm",
            d6: "HH:mm",
            d7: "HH:mm tt",
            d8: "dd/MMMM/yyyy",
            d9: "MMMM-dd",
            d10: "MM-dd",
            d11: "MM-dd-yyyy"
        },
        agendaViewString: "Agenda",
        agendaAllDayString: "all day",
        agendaDateColumn: "Date",
        agendaTimeColumn: "Time",
        agendaAppointmentColumn: "Appointment",
        backString: "Back",
        forwardString: "Forward",
        toolBarPreviousButtonString: "previous",
        toolBarNextButtonString: "next",
        emptyDataString: "No data to display",
        loadString: "Loading...",
        clearString: "Clear",
        todayString: "Today",
        dayViewString: "Day",
        weekViewString: "Week",
        monthViewString: "Month",
        timelineDayViewString: "TimeLine Day",
        editDialogStatuses:
        {
            free: "Free",
            tentative: "Tentative",
            busy: "Busy",
            outOfOffice: "Out of Office"
        }
    };

    getScheduleLocation_es: any =
        {
            '/': "/",
            ':': ":",
            firstDay: 0,
            days: {
                names: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
                namesAbbr: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab"],
                namesShort: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
            },
            months: {
                names: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Deciembre", ""],
                namesAbbr: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic", ""]
            },
            AM: ["AM", "am", "AM"],
            PM: ["PM", "pm", "PM"],
            eras: [
                { "name": "A.D.", "start": null, "offset": 0 }
            ],
            twoDigitYearMax: 2029,
            patterns: {
                d: "M/d/yyyy",
                D: "dddd, MMMM dd, yyyy",
                t: "h:mm tt",
                T: "h:mm:ss tt",
                f: "dddd, MMMM dd, yyyy h:mm tt",
                F: "dddd, MMMM dd, yyyy h:mm:ss tt",
                M: "MMMM dd",
                Y: "yyyy MMMM",
                S: "yyyy\u0027-\u0027MM\u0027-\u0027dd\u0027T\u0027HH\u0027:\u0027mm\u0027:\u0027ss",
                ISO: "yyyy-MM-dd hh:mm:ss",
                ISO2: "yyyy-MM-dd HH:mm:ss",
                d1: "dd.MM.yyyy",
                d2: "dd-MM-yyyy",
                d3: "dd-MMMM-yyyy",
                d4: "dd-MM-yy",
                d5: "H:mm",
                d6: "HH:mm",
                d7: "HH:mm tt",
                d8: "dd/MMMM/yyyy",
                d9: "MMMM-dd",
                d10: "MM-dd",
                d11: "MM-dd-yyyy"
            },
            agendaViewString: "Agenda",
            agendaAllDayString: "all day",
            agendaDateColumn: "Date",
            agendaTimeColumn: "Time",
            agendaAppointmentColumn: "Appointment",
            backString: "Back",
            forwardString: "Forward",
            toolBarPreviousButtonString: "previous",
            toolBarNextButtonString: "next",
            emptyDataString: "No existe información para visualizar",
            loadString: "cargando...",
            clearString: "Clear",
            todayString: "Hoy",
            dayViewString: "Dia",
            weekViewString: "Semana",
            monthViewString: "Mes",
            timelineDayViewString: "Linea de Tiempo",
            editDialogStatuses:
            {
                free: "Free",
                tentative: "Tentative",
                busy: "Busy",
                outOfOffice: "Out of Office"
            }
        };

    getGridLocation_en: any =
        {
            '/': '/',
            ':': ':',
            firstDay: 0,
            days: {
                names: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
                namesAbbr: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
                namesShort: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa']
            },
            months: {
                names: ['January', 'February', 'March', 'April', 'Mayxx', 'June', 'July', 'August', 'September', 'October', 'November', 'Dec', ''],
                namesAbbr: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec', '']
            },
            AM: ['AM', 'am', 'AM'],
            PM: ['PM', 'pm', 'PM'],
            eras: [
                { 'name': 'A.D.', 'start': null, 'offset': 0 }
            ],
            twoDigitYearMax: 2029,
            patterns: {
                d: 'M/d/yyyy',
                D: 'dddd, MMMM dd, yyyy',
                t: 'h:mm tt',
                T: 'h:mm:ss tt',
                f: 'dddd, MMMM dd, yyyy h:mm tt',
                F: 'dddd, MMMM dd, yyyy h:mm:ss tt',
                M: 'MMMM dd',
                Y: 'yyyy MMMM',
                S: 'yyyy\u0027-\u0027MM\u0027-\u0027dd\u0027T\u0027HH\u0027:\u0027mm\u0027:\u0027ss',
                ISO: 'yyyy-MM-dd hh:mm:ss',
                ISO2: 'yyyy-MM-dd HH:mm:ss',
                d1: 'dd.MM.yyyy',
                d2: 'dd-MM-yyyy',
                d3: 'dd-MMMM-yyyy',
                d4: 'dd-MM-yy',
                d5: 'H:mm',
                d6: 'HH:mm',
                d7: 'HH:mm tt',
                d8: 'dd/MMMM/yyyy',
                d9: 'MMMM-dd',
                d10: 'MM-dd',
                d11: 'MM-dd-yyyy'
            },
            percentsymbol: '%',
            currencysymbol: '$',
            currencysymbolposition: 'before',
            decimalseparator: '.',
            thousandsseparator: ',',
            pagergotopagestring: 'Go to page:',
            pagershowrowsstring: 'Show rows:',
            pagerrangestring: ' of ',
            pagerpreviousbuttonstring: 'previous',
            pagernextbuttonstring: 'next',
            pagerfirstbuttonstring: 'first',
            pagerlastbuttonstring: 'last',
            groupsheaderstring: 'Drag a column and drop it here to group by that column',
            sortascendingstring: 'Sort Ascending',
            sortdescendingstring: 'Sort Descending',
            sortremovestring: 'Remove Sort',
            groupbystring: 'Group By this column',
            groupremovestring: 'Remove from groups',
            filterclearstring: 'Clear',
            filterstring: 'Filter',
            filtershowrowstring: 'Show rows where:',
            filterorconditionstring: 'Or',
            filterandconditionstring: 'And',
            filterselectallstring: '(Select All)',
            filterchoosestring: 'Please Choose:',
            filterstringcomparisonoperators: ['empty', 'not empty', 'contain', 'contain (match case)',
                'does not contain', 'does not contain(match case)', 'starts with', 'starts with(match case)',
                'ends with', 'ends with(match case)', 'equal', 'equal(match case)', 'null', 'not null'],
            filternumericcomparisonoperators: ['equal', 'not equal', 'less than', 'less than or equal', 'greater than', 'greater than or equal', 'null', 'not null'],
            filterdatecomparisonoperators: ['equal', 'not equal', 'less than', 'less than or equal', 'greater than', 'greater than or equal', 'null', 'not null'],
            filterbooleancomparisonoperators: ['equal', 'not equal'],
            validationstring: 'Entered value is not valid',
            emptydatastring: 'No data to display',
            filterselectstring: 'Select Filter',
            loadtext: 'Loading...',
            clearstring: 'Clear',
            todaystring: 'Today'
        };

    getGridLocation_es: any =
        {
            '/': '/',
            ':': ':',
            firstDay: 0,
            days: {
                names: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
                namesAbbr: ["Dom", "Lun", "Mar", "Mie", "Jue", "Vie", "Sab"],
                namesShort: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"]
            },
            months: {
                names: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Deciembre", ""],
                namesAbbr: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic", ""]
            },
            AM: ['AM', 'am', 'AM'],
            PM: ['PM', 'pm', 'PM'],
            eras: [
                { 'name': 'A.D.', 'start': null, 'offset': 0 }
            ],
            twoDigitYearMax: 2029,
            patterns: {
                d: 'M/d/yyyy',
                D: 'dddd, MMMM dd, yyyy',
                t: 'h:mm tt',
                T: 'h:mm:ss tt',
                f: 'dddd, MMMM dd, yyyy h:mm tt',
                F: 'dddd, MMMM dd, yyyy h:mm:ss tt',
                M: 'MMMM dd',
                Y: 'yyyy MMMM',
                S: 'yyyy\u0027-\u0027MM\u0027-\u0027dd\u0027T\u0027HH\u0027:\u0027mm\u0027:\u0027ss',
                ISO: 'yyyy-MM-dd hh:mm:ss',
                ISO2: 'yyyy-MM-dd HH:mm:ss',
                d1: 'dd.MM.yyyy',
                d2: 'dd-MM-yyyy',
                d3: 'dd-MMMM-yyyy',
                d4: 'dd-MM-yy',
                d5: 'H:mm',
                d6: 'HH:mm',
                d7: 'HH:mm tt',
                d8: 'dd/MMMM/yyyy',
                d9: 'MMMM-dd',
                d10: 'MM-dd',
                d11: 'MM-dd-yyyy'
            },
            percentsymbol: '%',
            currencysymbol: '$',
            currencysymbolposition: 'before',
            decimalseparator: '.',
            thousandsseparator: ',',
            pagergotopagestring: 'Ir a Página:',
            pagershowrowsstring: 'Mostrar filas:',
            pagerrangestring: ' de ',
            pagerpreviousbuttonstring: 'anterior',
            pagernextbuttonstring: 'siguiente',
            pagerfirstbuttonstring: 'primero',
            pagerlastbuttonstring: 'ultimo',
            groupsheaderstring: 'Arrastre una columna y sueltela aqui para agrupar',
            sortascendingstring: 'Ordenar Ascendentemetne',
            sortdescendingstring: 'Ordenar Descendentemente',
            sortremovestring: 'Remover Ordenar',
            groupbystring: 'Agrupar por esta columna',
            groupremovestring: 'Remover de grupos',
            filterclearstring: 'Limpiar',
            filterstring: 'Filtro',
            filtershowrowstring: 'Mostrar filas donde:',
            filterorconditionstring: 'O',
            filterandconditionstring: 'Y',
            filterselectallstring: '(Seleccionar todo)',
            filterchoosestring: 'Seleccione:',
            filterstringcomparisonoperators: ['vacio', 'no vacio', 'contain', 'contain (match case)',
                'no contiene', 'no contiene(exacto)', 'inicia con', 'inicia con(exacto)',
                'termina con', 'termina con(exacto)', 'igual', 'igual(exacto)', 'nulo', 'no nulo'],
            filternumericcomparisonoperators: ['igual', 'diferente', 'menor que', 'menor que o igual', 'mayor que', 'mayor que o igual', 'nulo', 'no nulo'],
            filterdatecomparisonoperators: ['igual', 'diferente', 'menor que', 'menor que o igual', 'mayor que', 'mayor que o igual', 'nulo', 'no nulo'],
            filterbooleancomparisonoperators: ['igual', 'diferente'],
            validationstring: 'Valor ingresado no es válido',
            emptydatastring: 'No existe información para visualizar',
            filterselectstring: 'Seleccione filtro',
            loadtext: 'Cargando...',
            clearstring: 'Limpiar',
            todaystring: 'Hoy'
        };
}
