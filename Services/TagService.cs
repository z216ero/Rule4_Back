﻿using MapsterMapper;
using Rule4.Data;
using Rule4.Models;

namespace Rule4.Services
{
    public class TagService : BaseService
    {
        public TagService(IMapper mapper, DataContext dataContext) : base(mapper, dataContext)
        {
        }

        public List<Tag> GetAllTags()
        {
            return _dataContext.Tags.ToList();
        }
    }
}