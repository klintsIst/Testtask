namespace AGSRTestTask.WebAPI.Controllers;

using Common.Models.PatientModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// TODO: Add validators.
// TODO: Divide models + add mapper.

/// <summary>
/// Patient API controller.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService patientService;

    public PatientController(IPatientService patientService)
    {
        this.patientService = patientService;
    }

    /// <summary>
    /// Gets all exist Patients.
    /// </summary>
    /// <returns>Collection of Patient items.</returns>
    /// <response code="200">Returns all Patient items.</response>
    [HttpGet]
    [Route(nameof(GetAllPatients))]
    public async Task<IResult> GetAllPatients()
    {
        var allPatients = await patientService.GetAllPatientsAsync();

        return Results.Ok(allPatients);
    }

    /// <summary>
    /// Gets Patient item by Id.
    /// </summary>
    /// <param name="id">Patient Id.</param>
    /// <returns>Patient item.</returns>
    /// <response code="200">Returns the requested Patient item.</response>
    /// <response code="404">If the Patient item is not found.</response>
    [HttpGet]
    [Route(nameof(GetPatient))]
    public async Task<IResult> GetPatient(int id)
    {
        var patient = await patientService.GetPatientAsync(id);
        if (patient == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(patient);
    }

    /// <summary>
    /// Creates new Patient item.
    /// </summary>
    /// <param name="patientCreate">Create model for Patient item.</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /CreatePatient
    ///     {
    ///        "gender": 1,
    ///        "gender": "2025-07-27T11:30:50.763Z",
    ///        "active": true,
    ///        "name":
    ///        {
    ///             "use": "use1",
    ///             "family": "family1,
    ///             "givenNames": [
    ///                 "givenName1",
    ///                 "givenName2",
    ///             ]
    ///        }
    ///     }
    ///
    /// </remarks>
    /// <response code="201">If the Patient item was created.</response>
    [HttpPost]
    [Route(nameof(CreatePatient))]
    public async Task<IResult> CreatePatient([FromBody]PatientCreate patientCreate)
    {
        await patientService.CreateNewPatientAsync(patientCreate);

        return Results.Created();
    }

    /// <summary>
    /// Creates N random Patient items.
    /// </summary>
    /// <param name="n">Count of random Patients for creation.</param>
    /// <returns></returns>
    /// <response code="201">If the Patient items were created.</response>
    [HttpPost]
    [Route(nameof(CreateRandomPatients))]
    public async Task<IResult> CreateRandomPatients([FromBody]int n)
    {
        await patientService.CreateRandomPatientsAsync(n);

        return Results.Created();
    }

    /// <summary>
    /// Updates all fileds of Patient item.
    /// </summary>
    /// <param name="patientCreate">Create model with new values.</param>
    /// <param name="patientId">Patient Id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /CreatePatient
    ///     {
    ///        "gender": 1,
    ///        "gender": "2025-07-27T11:30:50.763Z",
    ///        "active": true,
    ///        "name":
    ///        {
    ///             "use": "use1",
    ///             "family": "family1,
    ///             "givenNames": [
    ///                 "givenName1",
    ///                 "givenName2",
    ///             ]
    ///        }
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Patient item was updated.</response>
    [HttpPatch]
    [Route(nameof(UpdatePatient))]
    public async Task<IResult> UpdatePatient([FromBody]PatientCreate patientCreate, int patientId)
    {
        await patientService.UpdatePatientAsync(patientCreate, patientId);

        return Results.Ok();
    }

    /// <summary>
    /// Deletes a specific Patient item.
    /// </summary>
    /// <param name="id">Patient Id.</param>
    /// <returns></returns>
    /// <response code="204">If the Patient item is deleted.</response>
    [HttpDelete]
    [Route(nameof(DeletePatient))]
    public async Task<IResult> DeletePatient(int id)
    {
        await patientService.DeletePatient(id);

        return Results.StatusCode(204);
    }

    /// <summary>
    /// Search Patients by FHIR query.
    /// </summary>
    /// <param name="fhirQuery">FHIR query.</param>
    /// <see ref="https://www.hl7.org/fhir/search.html#date"/>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /SearchByBirthDate
    ///     {
    ///         "fhirQuery":"le2024-07-27T11:30:50.763Z"
    ///     }
    ///     
    ///     GET /SearchByBirthDate
    ///     {
    ///         "fhirQuery":"ge2020-07-27T11:30:50.763Z"
    ///     }
    ///     
    ///     GET /SearchByBirthDate
    ///     {
    ///         "fhirQuery":"eq2025-07-27T11:58:19.312"
    ///     }
    /// </remarks>
    /// <returns>Collection of filtered Patient items.</returns>
    /// <response code="200">Returns filtered Patient items.</response>
    [HttpGet]
    [Route(nameof(SearchByBirthDate))]
    public async Task<IResult> SearchByBirthDate(string fhirQuery)
    {
        var patients = await patientService.SearchByBirthDateAsync(fhirQuery);

        return Results.Ok(patients);
    }
}
