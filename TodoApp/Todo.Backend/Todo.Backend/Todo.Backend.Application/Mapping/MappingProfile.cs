using AutoMapper;
using Todo.Backend.Application.Features.Tag.Command.Create;
using Todo.Backend.Application.Features.Tag.Command.Update;
using Todo.Backend.Application.Features.Todo.Command.Create;
using Todo.Backend.Application.Features.Todo.Command.Udate;
using Todo.Backend.Application.Features.TodoTag.Command.Create;
using Todo.Backend.Application.Features.TodoTag.Command.Update;
using Todo.Backend.Domain.Entities;

namespace Todo.Backend.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTagCommand, Tag>();
        CreateMap<UpdateTagCommand, Tag>();

        CreateMap<CreateTodoCommand, Domain.Entities.Todo>();
        CreateMap<UpdateTodoCommand, Domain.Entities.Todo>();

        CreateMap<CreateTodoTagCommand, TodoTag>();
        CreateMap<UpdateTodoTagCommand, TodoTag>();

    }
}
