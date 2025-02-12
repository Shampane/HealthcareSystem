import { Component, input, InputSignal } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-doctor-card',
  templateUrl: './doctor-card.component.html',
  styleUrl: './doctor-card.component.scss',
  imports: [NgOptimizedImage],
})
export class DoctorCardComponent {
  doctorImage: InputSignal<string> = input('');
  name: InputSignal<string> = input('');
  specialization: InputSignal<string> = input('');
  doctorUrl: InputSignal<string> = input('');
}
