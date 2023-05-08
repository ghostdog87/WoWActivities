﻿using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class ListActivities
    {
        public class Query : IRequest<Result<List<UserActivityDto>>>
        {
            public string Username { get; set; }
            public string Predicate { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<UserActivityDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<UserActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<UserActivityDto> query = _context.ActivityAttendees
                    .Where(x => x.AppUser.UserName == request.Username)
                    .OrderBy(x => x.Activity.Date)
                    .ProjectTo<UserActivityDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                query = request.Predicate switch
                {
                    "past" => query.Where(x => x.Date <= DateTime.Now),
                    "future" => query.Where(x => x.Date > DateTime.Now),
                    "hosting" => query.Where(x => x.HostUsername == request.Username),
                    _ => query.Where(x => x.Date > DateTime.Now),
                };

                var activites = await query.ToListAsync();

                return Result<List<UserActivityDto>>.Success(activites);
            }
        }
    }
}