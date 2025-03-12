import { Component, input, InputSignal } from "@angular/core";
import { NgOptimizedImage } from "@angular/common";
import { RouterLink } from "@angular/router";

@Component({
  selector: "app-doctor-card",
  templateUrl: "./doctor-card.component.html",
  styleUrl: "./doctor-card.component.scss",
  imports: [NgOptimizedImage, RouterLink],
})
export class DoctorCardComponent {
  id = input<string>("");
  doctorImage = input<string>("");
  name = input<string>("");
  specialization = input<string>("");
}
