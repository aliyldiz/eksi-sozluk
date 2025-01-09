using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EksiSozluk.Api.Application.Interfaces.Repositories;
using EksiSozluk.Common.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EksiSozluk.Api.Application.Features.Queries.GetEntiries;

public class GetEntriesQuery : IRequest<List<GetEntriesViewModel>>
{
    public bool TodayEntries { get; set; }
    public int Count { get; set; } = 100;
    
}
