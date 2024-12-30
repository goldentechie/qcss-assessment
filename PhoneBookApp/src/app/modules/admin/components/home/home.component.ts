import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
// For ng select 
assignees = [
  {
    name: 'John Deo'
  },
  {
    name: 'William Gens'
  },
  {
    name: 'Martial Fin'
  },
  {
    name: 'Lorem Den'
  },
  {
    name: 'Nomaski Pell'
  }
];
selectedAssignee = this.assignees[1].name;
}
