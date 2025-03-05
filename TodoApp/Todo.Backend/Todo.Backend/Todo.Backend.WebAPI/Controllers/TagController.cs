using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Backend.Application.Features.Tag.Command.Create;
using Todo.Backend.Application.Features.Tag.Command.Delete;
using Todo.Backend.Application.Features.Tag.Command.Update;
using Todo.Backend.Application.Features.Tag.Query.GetBy;
using Todo.Backend.Application.Features.Tag.Query.List;
using Todo.Backend.Application.Features.Todo.Query.List;
using Todo.Backend.WebAPI.Abstractions;

namespace Todo.Backend.WebAPI.Controllers
{
    public class TagController : ApiController
    {
        public TagController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetList(string id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ListTagQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetListOfUnchosenTags(string userId,Guid todoId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ListOfUnchosenTagsQuery(userId, todoId), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdTagQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
