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

        public async Task<bool> SaveTag(Tag tag)
        {
            var existTag = _dataContext.Tags.FirstOrDefault(x => x.Code == tag.Code || x.Name == tag.Name);
            if (existTag == null)
            {
                _dataContext.Tags.Add(tag);
                await _dataContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
