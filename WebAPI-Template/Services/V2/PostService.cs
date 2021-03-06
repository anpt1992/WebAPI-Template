using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_Template.Contracts.V2.Requests.Post;
using WebAPI_Template.Domain;
using WebAPI_Template.Infrastructure;

namespace WebAPI_Template.Services.V2
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync(FilterPostRequest filter = null);
        //Task<bool> CreatePostAsync(Post test);
        //Task<Post> GetPostByIdAsync(Guid testId);
        //Task<bool> UpdatePostAsync(Post testToUpdate);
        //Task<bool> DeletePostAsync(Guid testId);
        //Task<bool> UserOwnsPostAsync(Guid testId, string userId);
        //Task<List<Tag>> GetAllTagsAsync();
        //Task<bool> DeleteTagAsync(string tagName);
        //Task<bool> CreateTagAsync(Tag tag);
    }
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(FilterPostRequest filter = null)
        {
            if (filter == null)
            {
                return await _unitOfWork.Posts.GetAllAsync();
            }

            var skip = (filter.PageNumber - 1) * filter.PageSize;

            return await _unitOfWork.Posts.GetManyPaginationAsync(x => x.Name == filter.Name || String.IsNullOrEmpty(filter.Name), x => x.Id, filter.PageNumber, filter.PageSize);
        }

        //public async Task<Post> GetPostByIdAsync(Guid testId)
        //{
        //    return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == testId);
        //}
        //public async Task<bool> CreatePostAsync(Post post)
        //{
        //    post.Tags?.ForEach(postTag => postTag.TagName = postTag.TagName.ToLower());

        //    await this.AddNewTagsAsync(post);
        //    _dataContext.Posts.Add(post);
        //    var numCreated = await _dataContext.SaveChangesAsync();

        //    return numCreated > 0;
        //}
        //public async Task<bool> UpdatePostAsync(Post testToUpdate)
        //{
        //    _dataContext.Posts.Update(testToUpdate);
        //    var updated = await _dataContext.SaveChangesAsync();
        //    return updated > 0;
        //}
        //public async Task<bool> DeletePostAsync(Guid testId)
        //{
        //    var test = await GetPostByIdAsync(testId);
        //    _dataContext.Posts.Remove(test);
        //    var deleted = await _dataContext.SaveChangesAsync();

        //    return deleted > 0;
        //}

        //public async Task<bool> UserOwnsPostAsync(Guid testId, string userId)
        //{
        //    var test = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == testId);
        //    if (test == null)
        //    {
        //        return false;
        //    }
        //    if (test.UserId != userId)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //public async Task<List<Tag>> GetAllTagsAsync()
        //{
        //    return await _dataContext.Tags.AsNoTracking().ToListAsync();
        //}
        //public async Task<bool> DeleteTagAsync(string tagName)
        //{
        //    var tagToDelete = await _dataContext.Tags.FirstOrDefaultAsync(tag => tag.Name == tagName);

        //    if (tagToDelete == null)
        //    {
        //        return false;
        //    }

        //    _dataContext.Tags.Remove(tagToDelete);
        //    var numDeleted = await _dataContext.SaveChangesAsync();

        //    return numDeleted > 0;
        //}
        //public async Task<bool> CreateTagAsync(Tag tag)
        //{
        //    _dataContext.Tags.Add(tag);
        //    var numCreated = await _dataContext.SaveChangesAsync();

        //    return numCreated > 0;
        //}

        //private async Task AddNewTagsAsync(Post post)
        //{
        //    foreach (var newTag in post.Tags)
        //    {
        //        var matchingTag = await _dataContext.Tags.SingleOrDefaultAsync(existingTag => existingTag.Name == newTag.TagName);

        //        if (matchingTag != null)
        //        {
        //            continue;
        //        }

        //        _dataContext.Tags.Add(new Tag
        //        {
        //            Name = newTag.TagName,
        //            CreatorId = post.UserId,
        //            CreatedOn = DateTime.UtcNow
        //        });
        //    }
        //}

    }
}
