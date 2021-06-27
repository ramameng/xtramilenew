using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Countries
{
    public class List
    {
        public class Query : IRequest<List<CountryDto>> { }

        public class Handler : IRequestHandler<Query, List<CountryDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<CountryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Countries
                    .OrderBy(x => x.Name)
                    .ProjectTo<CountryDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}