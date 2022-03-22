using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using Blog.DAL.Repository;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TDD.DbTestHelpers.Core;
using NUnit.Framework;

namespace Blog.DAL.Tests
{

    [TestFixture]
    public class RepositoryTests : DbBaseTest<BlogFixtures>
    {

        [Test]
        public void GetAllPost_TwoPostsInDb_ReturnTwoPosts()
        {
            // arrange
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void AddPost_TwoPostsInDb_ReturnThreePosts()
        {
            // arrange
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            Post p = new Post
            {
                Author = "NewGuy123",
                Content = "Nice repo dude."
            };
            // act
            repository.AddPost(p);
            // assert
            var result = repository.GetAllPosts();
            Assert.AreEqual(3, result.Count());
            // aclean
        }

        [Test]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException))]
        public void AddPost_NoAuthor_ReturnException()
        {
            // arrange
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            Post p = new Post
            {
                Content = "I am anonymous."
            };
            // act
            repository.AddPost(p);
            // assert
            var result = repository.GetAllPosts();
            Assert.True(true);
            // aclean
        }

        [Test]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException))]
        public void AddPost_NoContent_ReturnException()
        {
            // arrange
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            Post p = new Post
            {
                Author = "Thoughtless"
            };
            // act
            repository.AddPost(p);
            // assert
            var result = repository.GetAllPosts();
            Assert.True(true);
            // aclean
        }

        [Test]
        public void GetAllCommentsForPostId_OneCommentInPost_ReturnOneComment()
        {
            // arrange
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            var post = context.Posts.Where(p => p.Content == "FIRST!!111!").FirstOrDefault();
            // act
            var result = repository.GetAllCommentsForPostId(post.Id);
            // assert
            Assert.AreEqual(1, result.Count());
            // aclean
        }

        [Test]
        public void AddComment_OneCommentInPost_ReturnTwoComments()
        {
            // arrange
            var context = new BlogContext();
            context.Database.CreateIfNotExists();
            var repository = new BlogRepository();

            var post = context.Posts.Where(p => p.Content == "test, test, test...").FirstOrDefault();
            Comment c = new Comment
            {
                PostId = post.Id,
                Content = "I am a testing comment."
            };
            // act
            repository.AddComment(c);
            // assert
            var result = repository.GetAllCommentsForPostId(post.Id);
            Assert.AreEqual(2, result.Count());
            // aclean
        }
    }
}
