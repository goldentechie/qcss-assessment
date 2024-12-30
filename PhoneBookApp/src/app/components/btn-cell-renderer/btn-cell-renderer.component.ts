import { Component, EventEmitter, Output } from "@angular/core";
import { ICellRendererAngularComp } from "ag-grid-angular";
import { ICellRendererParams } from "ag-grid-community";

@Component({
    selector: 'btn-cell-renderer',
    template: `
    <div class="btn-col">
      <button *ngIf="params.onEdit" (click)="onEditClick()" class="btn btn-icon-fill-light"><i class="fal fa-pen"></i></button>
      <button *ngIf="params.onDelete" (click)="onDeleteClick()" class="btn btn-icon-fill-light"><i class="fal fa-trash-alt"></i></button>
      <button *ngIf="params.getValue() !== undefined ? params.getValue().onConfirmation : undefined " (click)="onConfirmationClick()" class="btn"><i class="fal fa-check-square"></i></button>
    </div>
    `,
  })
  export class BtnCellRenderer implements ICellRendererAngularComp
  {

    refresh(params: ICellRendererParams<any, any, any>): boolean {
        return false;
    }

    public params: any;
  
    agInit(params: any): void {
        this.params = params;
        
    }

    onEditClick(): void {
        this.params.onEdit(this.params.data);
    }

    onDeleteClick(): void {
        this.params.onDelete(this.params.data);
    }

    onConfirmationClick(): void {
        this.params.getValue().onConfirmation(this.params.getValue());
    }
   
  }