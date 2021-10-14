﻿using Convey.CQRS.Queries;
using PizzaItaliano.Services.Releases.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaItaliano.Services.Releases.Application.Queries
{
    public class GetReleases : IQuery<IEnumerable<ReleaseDto>>
    {
        public Guid? OrderId { get; set; }
    }
}
