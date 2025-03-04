using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Backend.Application.Features.TodoTag.Command.Create;
using Todo.Backend.Application.Features.TodoTag.Command.Delete;
using Todo.Backend.Application.Features.TodoTag.Command.Update;
using Todo.Backend.Application.Features.TodoTag.Query.GetBy;
using Todo.Backend.Application.Features.TodoTag.Query.List;
using Todo.Backend.WebAPI.Abstractions;

namespace Todo.Backend.WebAPI.Controllers
{

    public class TodoTagController : ApiController
    {
        public TodoTagController(IMediator mediator) : base(mediator)
        {
        }

        //[HttpGet]
        //public async Task<IActionResult> GetList(string id, CancellationToken cancellationToken)
        //{
        //    var response = await _mediator.Send(new ListTodoTagQuery(id), cancellationToken);
        //    return StatusCode(response.StatusCode, response);
        //}
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdTodoTagQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoTagCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateTodoTagCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTodoTagCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
