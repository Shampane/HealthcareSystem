using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class DoctorMapping {
    public static DoctorDto ToDto(this Doctor d) {
        return new DoctorDto(
            d.DoctorId, d.Name, d.Description, d.ImageUrl, d.ExperienceAge,
            d.FeeInDollars, d.Specialization, d.PhoneNumber
        );
    }
}