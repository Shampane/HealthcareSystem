import { Component } from "@angular/core";
import { AngularSvgIconModule } from "angular-svg-icon";
import { NgOptimizedImage } from "@angular/common";
import { SpecializationsList } from "../../interfaces/specializationsList";

@Component({
  selector: "app-home-specialization",
  imports: [AngularSvgIconModule, NgOptimizedImage],
  templateUrl: "./home-specialization.component.html",
  styleUrl: "./home-specialization.component.scss",
})
export class HomeSpecializationComponent {
  specializations: SpecializationsList[] = [
    {
      pageUrl: "",
      imageUrl: "/assets/specialization-general-physician-icon.svg",
      title: "General Physician",
    },
    {
      pageUrl: "",
      imageUrl: "/assets/specialization-gynecologist-icon.svg",
      title: "Gynecologist",
    },
    {
      pageUrl: "",
      imageUrl: "/assets/specialization-dermatologist-icon.svg",
      title: "Dermatologist",
    },
    {
      pageUrl: "",
      imageUrl: "/assets/specialization-pediatrician-icon.svg",
      title: "Pediatrician",
    },
    {
      pageUrl: "",
      imageUrl: "/assets/specialization-neurologist-icon.svg",
      title: "Neurologist",
    },
    {
      pageUrl: "",
      imageUrl: "/assets/specialization-gastroenterologist-icon.svg",
      title: "Gastroenterologist",
    },
  ];
}
