using Application.Features.Enrollments.Commands.Create;
using Application.Features.Enrollments.Commands.Delete;
using Application.Features.Enrollments.Commands.Update;
using Application.Features.Enrollments.Queries.GetById;
using Application.Features.Enrollments.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedEnrollmentResponse>> Add([FromBody] CreateEnrollmentCommand command)
    {
        command.UserId = getUserIdFromRequest();
        CreatedEnrollmentResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedEnrollmentResponse>> Update([FromBody] UpdateEnrollmentCommand command)
    {
        UpdatedEnrollmentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedEnrollmentResponse>> Delete([FromRoute] Guid id)
    {
        DeleteEnrollmentCommand command = new() { Id = id };

        DeletedEnrollmentResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdEnrollmentResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdEnrollmentQuery query = new() { Id = id };

        GetByIdEnrollmentResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListEnrollmentQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEnrollmentQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListEnrollmentListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}