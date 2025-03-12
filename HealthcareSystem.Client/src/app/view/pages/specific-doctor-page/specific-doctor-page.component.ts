import { Component, inject, signal } from "@angular/core";
import { DoctorService } from "../../../data/services/doctor.service";
import { ActivatedRoute } from "@angular/router";
import { NgOptimizedImage } from "@angular/common";
import { Doctor } from "../../../data/entities/doctor";
import { doc } from "prettier";
import { ScheduleService } from "../../../data/services/schedule.service";
import { Schedule } from "../../../data/entities/schedule";

@Component({
  selector: "app-specific-doctor-page",
  imports: [NgOptimizedImage],
  templateUrl: "./specific-doctor-page.component.html",
  styleUrl: "./specific-doctor-page.component.scss",
})
export class SpecificDoctorPageComponent {
  doctorService = inject(DoctorService);
  scheduleService = inject(ScheduleService);

  id = signal<string>("");
  doctor = signal<Doctor | null>(null);
  schedules = signal<Schedule[] | null>(null);

  constructor(private activateRoute: ActivatedRoute) {
    this.id.set(this.activateRoute.snapshot.paramMap.get("id")!);
    this.doctorService.getDoctorById(this.id()).subscribe({
      next: (response: Doctor | null) => {
        this.doctor.set(response);
      },
      error: (err) => {
        console.log(err);
      },
    });
    this.scheduleService.getSchedulesByDoctorId(this.id()).subscribe({
      next: (response: Schedule[]) => {
        this.schedules.set(
          response?.map((s) => ({
            ...s,
            startTime: new Date(s.startTime),
            endTime: new Date(s.endTime),
          })),
        );
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  protected readonly doc = doc;
  protected readonly Date = Date;
}
