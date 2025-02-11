import { Component, input, InputSignal } from '@angular/core';

@Component({
  selector: 'app-doctor-card',
  templateUrl: './doctor-card.component.html',
  styleUrl: './doctor-card.component.scss',
})
export class DoctorCardComponent {
  doctorImage: InputSignal<string> = input('');
  name: InputSignal<string> = input('');
  specialization: InputSignal<string> = input('');
  doctorUrl: InputSignal<string> = input('');
}
