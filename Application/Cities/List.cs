using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cities
{
    public class List
    {
        public class Query : IRequest<List<CityDto>> 
        { 
            public Guid id { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<CityDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<CityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Cities
                    .Where(x => x.Country.Id == request.id)
                    .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
                    .OrderBy(x => x.Name)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}