using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System;
using System.Linq;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository()
        {
            _context = new BlogContext();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public Post GetPostById(long id)
        {
            return _context.Posts.Where(c => c.Id == id).FirstOrDefault();
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            if (comment.Post == null)
                comment.Post = GetPostById(comment.PostId);
            else
                comment.PostId = comment.Post.Id;

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetAllCommentsForPostId(long id)
        {
            return _context.Comments.Where(c => c.PostId == id);
        }

        public IEnumerable<Comment> GetAllCommentsForPost(Post post)
        {
            return _context.Comments.Where(c => c.PostId == post.Id);
        }
    }
}
