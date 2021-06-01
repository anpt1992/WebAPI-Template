using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Template.Data;
using WebAPI_Template.Domain;

namespace WebAPI_Template.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();     
        Task<bool> CreatePostAsync(Post test);
        Task<Post> GetPostByIdAsync(Guid testId);
        Task<bool> UpdatePostAsync(Post testToUpdate);
        Task<bool> DeletePostAsync(Guid testId);
        Task<bool> UserOwnsPostAsync(Guid testId, string userId);
    }
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid testId)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == testId);
        }
        public async Task<bool> CreatePostAsync(Post test)
        {
            await _dataContext.Posts.AddAsync(test);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }
        public async Task<bool> UpdatePostAsync(Post testToUpdate)
        {
            _dataContext.Posts.Update(testToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
        public async Task<bool> DeletePostAsync(Guid testId)
        {
            var test = await GetPostByIdAsync(testId);
            _dataContext.Posts.Remove(test);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid testId, string userId)
        {
            var test = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == testId);
            if(test == null)
            {
                return false;
            }
            if(test.UserId != userId)
            {
                return false;
            } 
            return true;
        }
        

    }
}
