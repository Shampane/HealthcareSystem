import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DoctorService } from '../../../data/services/doctor.service';
import { Doctor } from '../../../data/entities/doctor';
import { DoctorCardComponent } from '../../shared/doctor-card/doctor-card.component';
import { debounceTime, filter, map, switchMap } from 'rxjs';
import { GetDoctorsRequest } from '../../../data/requests/doctorRequests';

@Component({
  selector: 'app-doctors-list',
  imports: [DoctorCardComponent, ReactiveFormsModule],
  templateUrl: './doctors-list.component.html',
  styleUrl: './doctors-list.component.scss',
})
export class DoctorsListComponent {
  doctorService: DoctorService = inject(DoctorService);
  doctors: Doctor[] = [];

  specializations: string[] = [
    'All',
    'General Physician',
    'Gynecologist',
    'Dermatologist',
    'Pediatrician',
    'Neurologist',
    'Gastroenterologist',
  ];

  searchFormGroup = new FormGroup({
    searchName: new FormControl(''),
    searchSpecialization: new FormControl(this.specializations[0] ?? ''),
  });

  request: GetDoctorsRequest = {};

  constructor() {
    this.doctorService.getTestDoctors().subscribe((value) => (this.doctors = value));

    this.searchFormGroup.valueChanges
      .pipe(
        debounceTime(500),
        filter(
          (form) =>
            (form.searchName && form.searchName.length >= 3) ||
            form.searchName!.length === 0 ||
            form.searchSpecialization !== 'All',
        ),
        map((form) => {
          this.request.searchName = form.searchName!;
          this.request.searchSpecialization =
            form.searchSpecialization === 'All' ? '' : form.searchSpecialization!;
          return this.request;
        }),
        switchMap((request) => this.doctorService.getTestDoctors(request)),
      )
      .subscribe((list) => (this.doctors = list));
  }
}
