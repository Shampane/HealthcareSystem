import { Component, inject } from '@angular/core';
import { DoctorCardComponent } from '../../shared/doctor-card/doctor-card.component';
import { DoctorService } from '../../../data/services/doctor.service';
import { Doctor } from '../../../data/entities/doctor';

@Component({
  selector: 'app-home-doctor-list',
  templateUrl: './home-doctor-list.component.html',
  styleUrl: './home-doctor-list.component.scss',
  imports: [DoctorCardComponent],
})
export class HomeDoctorListComponent {
  doctorService: DoctorService = inject(DoctorService);
  doctors: Doctor[] = [];

  constructor() {
    this.doctorService.getTestDoctors().subscribe((value) => (this.doctors = value));
  }
}
