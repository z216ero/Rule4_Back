﻿using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Rule4.Data;
using Rule4.Dto.Posts;
using Rule4.Models;

namespace Rule4.Services
{
    public class PostService : BaseService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PostService(IMapper mapper, DataContext dataContext, IWebHostEnvironment webHostEnvironment) : base(mapper, dataContext)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<GetPostDto> GetPosts()
        {
            var posts = _dataContext.Posts.ProjectToType<GetPostDto>().ToList();
            return posts;
        }

        public GetPostDto GetPost(long id)
        {
            var post = _dataContext.Posts
                .FirstOrDefault(p => p.Id == id);

            return post.Adapt<GetPostDto>();
        }

        public async Task<Post> AddPost(AddPostDto dto)
        {
            var mappedPost = _mapper.Map<Post>(dto);
            _dataContext.Posts.Add(mappedPost);
            await _dataContext.SaveChangesAsync();
            return mappedPost;
        }

        public async Task<bool> DeletePost(long id)
        {
            var existPost = _dataContext.Posts.FirstOrDefault(p => p.Id == id);
            if (existPost != null)
            {
                _dataContext.Posts.Remove(existPost);
                await _dataContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePost(UpdatePostDto post)
        {
            var mappedPost = _mapper.Map<Post>(post);
            var existPost = _dataContext.Posts.FirstOrDefault(p => p.Id == mappedPost.Id);

            if (existPost == null)
                return false;

            await _dataContext.SaveChangesAsync();
            return true;
        }
    }
}