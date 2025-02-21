import { Component } from '@angular/core';
import { DoctorsListComponent } from '../../components/doctors-list/doctors-list.component';

@Component({
  selector: 'app-doctors-page',
  imports: [DoctorsListComponent],
  templateUrl: './doctors-page.component.html',
})
export class DoctorsPageComponent {}
