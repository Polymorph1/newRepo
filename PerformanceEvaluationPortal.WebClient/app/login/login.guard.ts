import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot,Router } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from './auth.service';

@Injectable()

export class LoginGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    canActivate(
        destination: ActivatedRouteSnapshot,
        state: RouterStateSnapshot) {

        if (this.authService.isLoggedIn) {

            if (this.authService.isAdmin) {
                if (state.url == '/login' || state.url == '') {
                    this.router.navigate(['/users']);
                    return false;
                }
            }

            if (!this.authService.isAdmin)
            {
                if (state.url == '/login' || state.url == '') {
                    this.router.navigate(['/startPage']);
                    return false;
                }
            }
        }
       
        return true;
    }   
}
