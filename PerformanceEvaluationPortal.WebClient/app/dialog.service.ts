import { Injectable } from '@angular/core';

declare var swal: any;

/**
 * Async modal dialog service
 * DialogService makes this app easier to test by faking this service.
 * TODO: better modal implementation that doesn't use window.confirm
 */
@Injectable()
export class DialogService {
    /**
     * Ask user to confirm an action. `message` explains the action and choices.
     * Returns promise resolving to `true`=confirm or `false`=cancel
     */
    confirm(message?: string) {
        return new Promise<boolean>(resolve => {
            return resolve(
                swal({
                        title: "Are you sure?",
                        text: message,
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#cc3f0c",
                        confirmButtonText: "Yes, proceed!",
                        closeOnConfirm: true
                    },
                    function (isConfirm) {
                        if (isConfirm) return true;
                        else return false;
                    }
                )
            )
        })
    }
}