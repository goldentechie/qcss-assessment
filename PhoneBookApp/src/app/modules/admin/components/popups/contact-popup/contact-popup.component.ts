import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ContactsService } from 'src/app/services/contacts.service';
import { entityExistsValidator } from 'src/app/validators/entity-exists.validator';

@Component({
  selector: 'contact-popup',
  templateUrl: './contact-popup.component.html',
  styleUrls: ['./contact-popup.component.scss']
})
export class ContactPopupComponent {

  public contactId: string | null;

  public contactForm: FormGroup = new FormGroup({
    firstName: new FormControl('', { validators: [Validators.required], updateOn: 'blur' }),
    lastName: new FormControl('', { validators: [Validators.required], updateOn: 'blur' }),
    phoneNumber: new FormControl('', [Validators.required]),
  });

  @Output() updateEvent = new EventEmitter<any>();

  constructor(private service: ContactsService) {
    
  }

  isLoading: boolean = false;

  closeRightSidebar(){
    this.contactForm.reset();
    $('.contact-sidebar').removeClass('open');
  }

  openRightSidebar(id: string | null = null) {
    this.contactForm.reset();
    this.contactId = id;

    $('.contact-sidebar').toggleClass('open');

    if(this.contactId) {
      this.contactForm.controls['phoneNumber'].setAsyncValidators([]);
      this.isLoading = true;
      this.service.getById(this.contactId).subscribe(res => {
        if(!res.isError) {
          this.contactForm.reset({ firstName: res.data.firstName, lastName: res.data.lastName, phoneNumber: res.data.phoneNumber });
        this.isLoading = false;
        }
      })
    }
    else {
      this.contactForm.controls['phoneNumber'].setAsyncValidators([entityExistsValidator(this.service)]);
    }
  }

  onSubmit() {
    if(this.contactForm.valid) {
      this.isLoading = true;
      if(this.contactId) {
        this.service.update(this.contactId, this.contactForm.value).subscribe(data => {
          this.updateEvent.emit(this.contactId);
          this.closeRightSidebar();
          this.isLoading = false;
        });
      }
      else {
        this.service.add(this.contactForm.value).subscribe(data => {
          this.updateEvent.emit(this.contactId);
          this.closeRightSidebar();
          this.isLoading = false;
        });
      }
    }
  }

 }
  
