using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Backend.Application.Features.Todo.Command.Create;
using Todo.Backend.Application.Features.Todo.Command.Delete;
using Todo.Backend.Application.Features.Todo.Command.Udate;
using Todo.Backend.Application.Features.Todo.Query.GetBy;
using Todo.Backend.Application.Features.Todo.Query.List;
using Todo.Backend.WebAPI.Abstractions;

namespace Todo.Backend.WebAPI.Controllers
{
    public class TodoController : ApiController
    {
        public TodoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetList(string id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ListTodoQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdTodoQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

    }
}
