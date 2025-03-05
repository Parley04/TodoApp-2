using AutoMapper;
using Todo.Backend.Application.Features.Tag.Command.Create;
using Todo.Backend.Application.Features.Tag.Command.Update;
using Todo.Backend.Application.Features.Todo.Command.Create;
using Todo.Backend.Application.Features.Todo.Command.Udate;
using Todo.Backend.Application.Features.TodoTag.Command.Create;
using Todo.Backend.Application.Features.TodoTag.Command.Delete;
using Todo.Backend.Application.Features.TodoTag.Command.Update;
using Todo.Backend.Domain.Dtos;
using Todo.Backend.Domain.Entities;

namespace Todo.Backend.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTagCommand, Tag>();
        CreateMap<UpdateTagCommand, Tag>();
        CreateMap<Tag, TagDto>();

        CreateMap<CreateTodoCommand, Domain.Entities.Todo>();
        CreateMap<UpdateTodoCommand, Domain.Entities.Todo>();

        CreateMap<CreateTodoTagCommand, Domain.Entities.Tag>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<CreateTodoTagCommand, Domain.Entities.TodoTag>(); 
        CreateMap<UpdateTodoTagCommand, TodoTag>();
        CreateMap<DeleteTodoTagCommand, Domain.Entities.Tag>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<DeleteTodoTagCommand, Domain.Entities.TodoTag>();
        CreateMap<DeleteWithItemsCommand, Domain.Entities.Tag>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<DeleteWithItemsCommand, Domain.Entities.TodoTag>();

    }
}
