import { Component } from '@angular/core';

@Component({
  selector: 'app-right-sidebar',
  templateUrl: './right-sidebar.component.html',
  styleUrls: ['./right-sidebar.component.scss']
})
export class RightSidebarComponent {
  
  closeRightSidebar = function(event:any){
    event.preventDefault();
    $('.right-sidebar').removeClass('open');
    $('.product, .orgnization').hide();
  }

  openRightSidebar = function (sidebarToOpen: string) {

    $('.right-sidebar').toggleClass('open');
    $('.product, .orgnization').hide();
    $('.' + sidebarToOpen).show();
  }

 }
  
