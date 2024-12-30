import {AbstractControl, AsyncValidatorFn} from '@angular/forms';
import { BaseService } from '../services/base-service';
import { map, of } from 'rxjs';

export function entityExistsValidator(service: BaseService): AsyncValidatorFn  {
    return (control: AbstractControl) => {
        return service.entityExists(control.value)
            .pipe(
                map(exists => exists ? { entityExists:true } : null)
            );
    }
}