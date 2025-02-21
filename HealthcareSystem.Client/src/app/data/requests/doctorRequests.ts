export interface GetDoctorsRequest {
  pageIndex?: number;
  pageSize?: number;
  sortField?: string;
  sortOrder?: string;
  searchName?: string;
  searchSpecialization?: string;
}

export interface CreateDoctorRequest {
  name: string;
  description: string;
  imageUrl?: string;
  experienceAge: number;
  feeInDollars: number;
  specialization: string;
  phoneNumber: string;
}

export interface UpdateDoctorRequest {
  name?: string;
  description?: string;
  imageUrl?: string;
  experienceAge?: number;
  feeInDollars?: number;
  specialization?: string;
  phoneNumber?: string;
}
