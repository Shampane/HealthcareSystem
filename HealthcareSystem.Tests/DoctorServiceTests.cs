using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Application.Services;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using NSubstitute;

namespace HealthcareSystem.Tests;

public class DoctorServiceTests {
    private const string ErrorStatus = nameof(ResponseStatus.Error);
    private const string SuccessStatus = nameof(ResponseStatus.Success);
    private readonly IDoctorRepository _repositoryMock;
    private readonly DoctorService _service;

    public DoctorServiceTests() {
        _repositoryMock = Substitute.For<IDoctorRepository>();
        _service = new DoctorService(_repositoryMock);
    }

    [Fact]
    public async Task
        GetByIdAsync_ShouldReturnErrorResponse_WhenDoctorIsNull() {
        var id = Guid.NewGuid();

        GetEntityResponse<DoctorDto>?
            response = await _service.GetByIdAsync(id);
        Assert.Equal(ErrorStatus, response.Status);
        Assert.Equal("The Doctor wasn't found", response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task
        CreateAsync_ShouldReturnErrorResponse_WhenIntegersAreInvalid() {
        var request = new DoctorRequest(
            "Name", "Desc",
            "https://images.ru/image2.png", -5, -25,
            "", "+79123456789"
        );
        const string errorMessage =
            "Specialization can't be empty, Experience age can't be less than 0 or more than 100, Fee in dollars can't be less than 0 dollars.";

        CreateResponse<DoctorDto>? response =
            await _service.CreateAsync(request);
        Assert.Equal(ErrorStatus, response.Status);
        Assert.Equal(errorMessage, response.Message);
        Assert.Null(response.Data);
    }

    [Fact]
    public async Task
        CreateAsync_ShouldReturnSuccessResponse_WhenRequestIsValid() {
        var request = new DoctorRequest(
            "Ivan Ivanov", "Description",
            "https://images.ru/image3.png", 8, 20,
            "Special Argument", "+79123456789"
        );

        CreateResponse<DoctorDto>? response =
            await _service.CreateAsync(request);

        var doctor = new Doctor(
            request.Name, request.Description, request.ImageUrl,
            request.ExperienceAge, request.FeeInDollars,
            request.Specialization, request.PhoneNumber
        );
        var doctorDto = new DoctorDto(
            doctor.DoctorId, doctor.Name, doctor.Description,
            doctor.ImageUrl, doctor.ExperienceAge, doctor.FeeInDollars,
            doctor.Specialization, doctor.PhoneNumber
        );
        Assert.Equal(SuccessStatus, response.Status);
        Assert.Equal("The doctor was created", response.Message);
        Assert.Equivalent(doctorDto, response.Data);
    }
}