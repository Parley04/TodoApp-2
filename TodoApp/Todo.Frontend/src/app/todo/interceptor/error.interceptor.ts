import { Injectable } from '@angular/core';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpResponse,
    HttpErrorResponse,
    HttpStatusCode,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { MessageService } from 'primeng/api';

@Injectable()


export class ErrorInterceptor implements HttpInterceptor {

    constructor(private messsageService: MessageService) { }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        return next.handle(request).pipe(catchError((error) => {
            switch (error.status) {
                case 0:
                    this.messsageService.add({ severity: 'error', summary: 'Error', detail: 'Sunucu Bağlantısı Yok' });
                    break;
                case HttpStatusCode.Unauthorized:
                    this.messsageService.add({ severity: 'error', summary: 'Error', detail: 'Yetkisiz İşlem' });
                    break;
                case HttpStatusCode.InternalServerError:
                    this.messsageService.add({ severity: 'error', summary: 'Error', detail: error.error.Message });
                    break;
                case HttpStatusCode.BadRequest:
                    this.messsageService.add({ severity: 'error', summary: 'Error', detail: error.error });
                    break;
                default:
                    this.messsageService.add({ severity: 'error', summary: 'Error', detail: 'Beklenmeyen Error' });
                    break;
            }
            return throwError(error);
        }));
    }
}
