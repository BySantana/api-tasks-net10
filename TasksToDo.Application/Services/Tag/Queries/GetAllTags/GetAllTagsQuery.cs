using MediatR;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Services.Tag.Queries.GetAllTags;

public sealed record GetAllTagsQuery(PagedRequest PagedRequest) : IRequest<PagedResponse<TagResponse>>;