import { Component } from '@angular/core';
import { DoctorCardComponent } from '../../shared/doctor-card/doctor-card.component';

@Component({
  selector: 'app-home-doctor-list',
  imports: [DoctorCardComponent],
  templateUrl: './home-doctor-list.component.html',
  styleUrl: './home-doctor-list.component.scss',
})
export class HomeDoctorListComponent {}
