using DataAccess;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using PgCtx;
using Service;
using Service.TransferModels.Requests;
using Service.Validators;

namespace ServiceTests;

public class HospitalServiceTests
{
    private readonly HospitalService _hospitalService;

    public HospitalServiceTests()
    {
        var pgCtxSetup = new PgCtxSetup<HospitalContext>();
        _hospitalService = new HospitalService(NullLogger<HospitalService>.Instance, new StubHospitalRepository(), new CreatePatientValidator(), new UpdatePatientValidator(), pgCtxSetup.DbContextInstance);
    }

    [Fact]
    public void CreatePatient_Should_Successfully_Return_A_Patient()
    {
        var createPatientDto = new CreatePatientDto
        {
            Name = "John Doe",
            Address = "1234 Elm Street",
            Birthdate = new DateOnly(1990, 1, 1),
            Gender = true
        };
        var result = _hospitalService.CreatePatient(createPatientDto);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("1234 Elm Street", result.Address);
        Assert.Equal(new DateOnly(1990, 1, 1), result.Birthdate);
    }
    
    [Fact]
    public void Test1()
    {
        var updatePatient = new UpdatePatientDto
        {
            Name = "",
        };
        //Asser throws
        Assert.Throws<ValidationException>(() => _hospitalService.UpdatePatient(updatePatient));

    }
}