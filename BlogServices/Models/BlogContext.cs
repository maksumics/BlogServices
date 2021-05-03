using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;

namespace BlogServices.Models
{
    public class BlogContext:DbContext
    {
        public BlogContext():base("BlogDatabase")
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }


        public IEnumerable<Post> GetPosts(string byTag="")
        {
            if (byTag == "")
                return this.Posts.ToList().OrderByDescending(x => x.CreatedAt);
            else
                return this.Posts.Where(x => x.Tags.Where(t => t.TagName == byTag).Any()).ToList();
        }
        public Post GetPostBySlug(string slug)
        {
            return this.Posts.Where(x => x.Slug == slug).SingleOrDefault();
        }

        public Post InsertPost(Post item, List<string> tags)
        {
            item.Slug = GenerateSlug(item.Title);
            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;
            foreach (var tagname in tags)
            {
                var tag = this.Tags.Where(x => x.TagName == tagname).SingleOrDefault();
                if (tag != null)
                    item.Tags.Add(tag);
                else
                    item.Tags.Add(new Tag() { TagName = tagname });

            }
            var post =this.Posts.Add(item);
            this.SaveChanges();
            return post;
        }

        public Post UpdatePost(string slug, Post newPost)
        {
            var post = GetPostBySlug(slug);
            if (post != null)
            {
                if (newPost.Title != null)
                {
                    post.Title = newPost.Title;
                    post.Slug = GenerateSlug(post.Title);
                }
                if (newPost.Description != null)
                    post.Description = newPost.Description;
                if (newPost.Body != null)
                    post.Body = newPost.Body;
                post.UpdatedAt = DateTime.Now;

                this.SaveChanges();
            }
            return post;
        }

        public bool RemovePost(string slug)
        {
            var post = this.Posts.Where(x => x.Slug == slug).SingleOrDefault();
            if (post != null)
            {
                this.Posts.Remove(post);
                this.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public IEnumerable<Tag> GetAllTags()
        {
            return this.Tags.ToList().OrderByDescending(t => t.TagID);
        }
        string GenerateSlug(string title)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var c in title)
            {
                if (!char.IsPunctuation(c))
                    builder.Append(c);
            }
            var slug = builder.ToString().Replace(' ', '-').ToLower();
            var sameSlug = new List<Post>();
            do
            {
                sameSlug = this.Posts.Where(x => x.Slug == slug).ToList();
                if(sameSlug.Count > 0)
                {
                    slug += '-';
                    slug += sameSlug.Count + 1;
                }
            } while (sameSlug.Count > 0);
            { 
                
            }
            return slug;
        }
    }
}