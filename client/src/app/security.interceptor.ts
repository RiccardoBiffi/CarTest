import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { SecurityService } from './security.service';
import { environment } from '../environments/environment';

@Injectable()
export class SecurityInterceptor implements HttpInterceptor {
  constructor(private injector: Injector, private router: Router) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const auth = this.injector.get(SecurityService);

    if (request.url.indexOf(environment.serverMethodsUrl) != 0 && request.url.indexOf(environment.radzenRentacar) != 0 && request.url.indexOf(environment.securityUrl) != 0) {
      return next.handle(request);
    }

    if (auth.accessToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${auth.accessToken}`
        }
      });
    }

    return next.handle(request)
    .pipe(catchError((response: any) => {
      if (response.status === 401) {
        const redirectUrl = this.router.url;

        this.router.navigate([{outlets: {popup: null}}]).then(() =>
            this.router.navigate(['login'], {queryParams: {redirectUrl}})
        );

        response.error = {error: {message: 'Session expired.'}};
      }

      return throwError(response);
    }));
  }
}
