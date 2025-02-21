import { Component, input, InputSignal } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-doctor-card',
  templateUrl: './doctor-card.component.html',
  styleUrl: './doctor-card.component.scss',
  imports: [NgOptimizedImage],
})
export class DoctorCardComponent {
  doctorImage = input<string>('');
  name = input<string>('');
  specialization = input<string>('');
  doctorUrl = input<string>('');
}
