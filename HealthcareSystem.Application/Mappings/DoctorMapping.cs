using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class DoctorMapping {
    public static DoctorDto ToDto(this Doctor d) {
        return new DoctorDto {
            Id = d.Id, Name = d.Name, Description = d.Description,
            ImageUrl = d.ImageUrl, ExperienceAge = d.ExperienceAge,
            FeeInDollars = d.FeeInDollars, Specialization = d.Specialization,
            PhoneNumber = d.PhoneNumber
        };
    }
}