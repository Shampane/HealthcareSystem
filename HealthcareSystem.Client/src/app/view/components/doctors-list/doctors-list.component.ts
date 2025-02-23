import { Component, computed, effect, inject, signal } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { DoctorService } from "../../../data/services/doctor.service";
import { Doctor } from "../../../data/entities/doctor";
import { DoctorCardComponent } from "../../shared/doctor-card/doctor-card.component";
import { debounceTime, filter, map } from "rxjs";
import { GetDoctorsRequest } from "../../../data/requests/doctorRequests";

@Component({
  selector: "app-doctors-list",
  imports: [DoctorCardComponent, ReactiveFormsModule],
  templateUrl: "./doctors-list.component.html",
  styleUrl: "./doctors-list.component.scss",
})
export class DoctorsListComponent {
  doctorService = inject(DoctorService);

  specializations: string[] = [
    "All",
    "General Physician",
    "Gynecologist",
    "Dermatologist",
    "Pediatrician",
    "Neurologist",
    "Gastroenterologist",
  ];
  sortFields: string[] = [
    "None",
    "Name",
    "Experience age",
    "Fee in dollars",
    "Specialization",
  ];
  pageSizes: number[] = [8, 12, 16, 20];

  doctors = signal<Doctor[]>([]);
  totalCount = signal(-1);
  lastPage = computed(() => Math.ceil(this.totalCount() / this.pageSize()));
  pageIndex = signal(1);
  pageSize = signal(this.pageSizes[0]);
  request = signal<GetDoctorsRequest>({
    pageSize: this.pageSizes[0],
  });

  searchFormGroup = new FormGroup({
    name: new FormControl("", { nonNullable: true }),
    specialization: new FormControl(this.specializations[0], {
      nonNullable: true,
    }),
  });
  sortFormGroup = new FormGroup({
    field: new FormControl(this.sortFields[0], { nonNullable: true }),
    order: new FormControl(false, { nonNullable: true }),
  });
  pageSizeForm = new FormControl(this.pageSizes[0], { nonNullable: true });

  fetchDoctors(request: GetDoctorsRequest) {
    this.doctorService.getDoctors(request).subscribe((response) => {
      this.doctors.set(response.data);
      this.totalCount.set(response.totalCount);
    });
  }

  goToPreviousPage() {
    if (this.pageIndex() > 1) {
      this.pageIndex.set(this.pageIndex() - 1);
    }
  }
  goToNextPage() {
    if (this.pageIndex() < this.lastPage()) {
      this.pageIndex.set(this.pageIndex() + 1);
    }
  }
  setPage(page: number) {
    this.pageIndex.set(page);
  }

  constructor() {
    this.fetchDoctors(this.request());

    effect(() => {
      const pageIndex = this.pageIndex();
      this.request.update((r) => ({
        ...r,
        pageIndex,
      }));
      this.fetchDoctors(this.request());
    });

    this.searchFormGroup.valueChanges
      .pipe(
        debounceTime(500),
        filter(
          (form) =>
            (form.name && form.name.length >= 3) ||
            form.name?.length === 0 ||
            form.specialization !== "All",
        ),
        map((form) => {
          const searchName = form.name;
          const searchSpecialization =
            form.specialization === "All" ? "" : form.specialization;
          this.request.update((r) => ({
            ...r,
            searchName,
            searchSpecialization,
          }));
          return this.request();
        }),
      )
      .subscribe((request) => this.fetchDoctors(request));

    this.sortFormGroup.valueChanges
      .pipe(
        debounceTime(200),
        map((form) => {
          const sortOrder = form.order ? "DESC" : "ASC";
          const sortField = form.field;
          this.request.update((r) => ({
            ...r,
            sortOrder,
            sortField,
          }));
          return this.request();
        }),
      )
      .subscribe((request) => this.fetchDoctors(request));

    this.pageSizeForm.valueChanges
      .pipe(
        debounceTime(200),
        map((value) => {
          this.pageSize.set(value);
          this.pageIndex.set(1);
          const pageIndex = 1;
          const pageSize = value;

          this.request.update((r) => ({
            ...r,
            pageSize,
            pageIndex,
          }));

          return this.request();
        }),
      )
      .subscribe((request) => this.fetchDoctors(request));
  }
}
