using HealthcareSystem.Application.Dtos;

namespace HealthcareSystem.Application.Mappings;

public static class DoctorMapping {
    public static DoctorDto ToDto(this DoctorDto d) {
        return new DoctorDto {
            Id = d.Id, Name = d.Name, Description = d.Description,
            ImageUrl = d.ImageUrl, ExperienceAge = d.ExperienceAge,
            FeeInDollars = d.FeeInDollars, Specialization = d.Specialization,
            PhoneNumber = d.PhoneNumber
        };
    }
}