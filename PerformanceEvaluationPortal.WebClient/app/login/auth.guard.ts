import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';


@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) { }

    canActivate(
        destination: ActivatedRouteSnapshot,
        state: RouterStateSnapshot)
    {
        if (this.authService.isLoggedIn) {
            if (!this.authService.isAdmin) {
                if (state.url == '/users' || state.url == '/templates') {
                    this.router.navigate(['/startPage']);
                    return false;
                }
            }
            return true;
        }
              
        this.router.navigate(['/login']);
        return false;
    }
}
