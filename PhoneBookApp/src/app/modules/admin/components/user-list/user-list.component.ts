import { Component, ViewChild } from '@angular/core';
import { AgGridAngular } from 'ag-grid-angular';
import { ICellRendererParams, ColDef, GridReadyEvent } from 'ag-grid-community';
import { UsersService } from 'src/app/services/users.service';
@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {

  constructor(private userService: UsersService) {
  }

  // Each Column Definition results in one Column.
  public columnDefs: ColDef[] = [
    {
      field: 'firstName',
      flex:2,
      filter: 'agTextColumnFilter', suppressMenu: true, floatingFilter: true, sortable: true
    },
    { field: 'lastName', flex:2, filter: 'agTextColumnFilter', suppressMenu: true, floatingFilter: true, sortable: true},
    { field: 'email', flex:3, filter: 'agTextColumnFilter', suppressMenu: true, floatingFilter: true, sortable: true},
    { field: 'phoneNumber', headerName: 'Phone', flex:2, filter: 'agTextColumnFilter', suppressMenu: true, floatingFilter: true, sortable: true},

    {
      field: 'action',
      width:150,
      filter: false,
      sortable:false,
      cellRenderer: (params: ICellRendererParams) => {
        
        return `<div class="btn-col">
        <a href="/admin/user-add/${params.data.id}" class="btn btn-icon-fill-light"><i class="fal fa-pen"></i></a>
        </div>
        `
      }
    },
  ];
  public paginationPageSize = 10;
  // DefaultColDef sets props common to all Columns
  public defaultColDef: ColDef = {
    filter: true,
    sortable:true
  };

  public rowData$!: any[];

  @ViewChild(AgGridAngular) agGrid!: AgGridAngular;

  onGridReady(params: GridReadyEvent) {
    this.getGridData();
  }

  private getGridData() {
    this.userService.get().subscribe(r => {
      this.rowData$ = r.data;
    });
  }

}
