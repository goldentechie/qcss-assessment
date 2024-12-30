import { Component, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ICellRendererParams, CellClickedEvent, ColDef, GridReadyEvent } from 'ag-grid-community';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { ContactsService } from 'src/app/services/contacts.service';
import { ContactPopupComponent } from '../popups/contact-popup/contact-popup.component';
import { BtnCellRenderer } from 'src/app/components/btn-cell-renderer/btn-cell-renderer.component';
import { DeleteConfirmationComponent } from '../popups/delete-confirmation/delete-confirmation.component';
@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.scss']
})
export class ContactsListComponent {
  constructor(private toastr: ToastrService, private contactService: ContactsService) { }

  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr Title');
  }
  
  // Each Column Definition results in one Column.
  public columnDefs: ColDef[] = [
    { field: 'firstName', flex: 2, filter: 'agTextColumnFilter', suppressMenu: true, floatingFilter: true, sortable: true },
    { field: 'lastName', flex: 2, filter: 'agTextColumnFilter', suppressMenu: true, floatingFilter: true, sortable: true },
    { field: 'phoneNumber', flex: 2, filter: 'agTextColumnFilter', suppressMenu: true, floatingFilter: true, sortable: true },
    { field: 'action',
      width: 150,
      filter: false,
      sortable: false,
      cellRenderer: BtnCellRenderer, 
      cellRendererParams: {
          onEdit: this.openRightSidebar.bind(this),
          onDelete: this.confirmationPopup.bind(this)
      }
    },
  ];
  
  public paginationPageSize = 10;

  // DefaultColDef sets props common to all Columns
  public defaultColDef: ColDef = {
    filter: true,
    sortable: true
  };

  public rowData$!: any[];

  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;
  @ViewChild(ContactPopupComponent) popup: ContactPopupComponent;
  @ViewChild(DeleteConfirmationComponent) confPopup: DeleteConfirmationComponent;

  onGridReady(params: GridReadyEvent) {
    this.getGridData();
  }

  openRightSidebar(data: any = null) {

    if (data) {
      this.popup.openRightSidebar(data.id);
    }
    else {
      this.popup.openRightSidebar();
    }
  }

  confirmationPopup(data: any) {
    this.confPopup.showPopup(data.id);
  }

  popupUpdate(data: any) {
    this.getGridData();
  }

  onConfirmation(result: any) {
    if(result.confirmed) {
      this.contactService.delete(result.id).subscribe(() => {
        this.getGridData();
      })
    }
  }

  private getGridData() {
    this.contactService.get().subscribe(r => {
      if(!r.isError) {
        this.rowData$ = r.data;
      }
    });
  }

}
