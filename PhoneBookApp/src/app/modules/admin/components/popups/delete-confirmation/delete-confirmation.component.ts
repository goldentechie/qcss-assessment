import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'delete-confirmation',
  templateUrl: './delete-confirmation.component.html',
  styleUrls: ['./delete-confirmation.component.scss']
})
export class DeleteConfirmationComponent {

  @Input() heading = '';
  @Input() message = '';
  @Input() confirmButton = '';
  @Input() cancelButton = '';

  @Output() confirmationResult = new EventEmitter<any>();

  private itemId: any;

  @ViewChild('popupTrigger', { read: ElementRef }) trigger: ElementRef<HTMLElement>;

  showPopup(id: any) {
    this.itemId = id;
    this.trigger.nativeElement.click();
  }

  onConfirmation() {
    this.confirmationResult.emit({ confirmed: true, id: this.itemId});
    this.trigger.nativeElement.click();
  }

}
