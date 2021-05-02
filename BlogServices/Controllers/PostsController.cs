using BlogServices.Models;
using BlogServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogServices.Controllers
{
    public class PostsController : ApiController
    {
        public IHttpActionResult GetPosts()
        {
            List<PostViewModel> _listPosts = new List<PostViewModel>();
            using (var db = new BlogContext())
            {
                var _posts = db.GetPosts();
                foreach (var item in _posts)
                {
                    _listPosts.Add(new PostViewModel()
                    {
                        Slug = item.Slug,
                        Title = item.Title,
                        Description = item.Description,
                        Body = item.Body,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt
                    });
                    foreach (var _tag in item.Tags)
                    {
                        _listPosts.Last().Tags.Add(_tag.TagName);
                    }
                }
            }
            var data = new
            {
                blogPosts = _listPosts,
                postsCount = _listPosts.Count
            };
            return Json(data);
        }
        public IHttpActionResult GetPosts(string tag)
        {
            List<PostViewModel> _listPosts = new List<PostViewModel>();
            using(var db = new BlogContext())
            {
                var _posts = db.GetPosts(tag);
                foreach (var item in _posts)
                {
                    _listPosts.Add(new PostViewModel()
                    {
                        Slug = item.Slug,
                        Title = item.Title,
                        Description = item.Description,
                        Body = item.Body,
                        CreatedAt = item.CreatedAt,
                        UpdatedAt = item.UpdatedAt
                    });
                    foreach (var _tag in item.Tags)
                    {
                        _listPosts.Last().Tags.Add(_tag.TagName);
                    }
                }
            }
            var data = new
            {
                blogPosts = _listPosts,
                postsCount = _listPosts.Count
            };
            return Json(data);
        }

        public IHttpActionResult GetPostBySlug(string id)
        {
            PostViewModel post = new PostViewModel(); ;
            using (var db = new BlogContext())
            {
                var _post = db.GetPostBySlug(id);
                if (_post != null)
                {
                    post.Title = _post.Title;
                    post.Slug = _post.Slug;
                    post.Description = _post.Description;
                    post.Body = _post.Body;
                    post.CreatedAt = _post.CreatedAt;
                    post.UpdatedAt = _post.UpdatedAt;
                    foreach (var _tag in _post.Tags)
                    {
                        post.Tags.Add(_tag.TagName);
                    }
                }
            }
            var data = new
            {
                blogPost = post
            };
            return Json(data);
        }
        
        public IHttpActionResult PostArticle(PostViewModel post)
        {
            if(ModelState.IsValid)
            {
                using(var db = new BlogContext())
                {
                    var item = new Post();
                    item.Body = post.Body;
                    item.Description = post.Description;
                    item.Title = post.Title;
                    item = db.InsertPost(item, post.Tags);
                    post.Slug = item.Slug;
                    post.CreatedAt = item.CreatedAt;
                    post.UpdatedAt = item.UpdatedAt;
                }
                return RedirectToRoute("DefaultApi", new  { id = post.Slug });
            }
            else
            {
                return Json(new { status = "not valid input data" });
            }
        }

        [AcceptVerbs("PUT")]
        public IHttpActionResult UpdatePost(string id, UpdatePostViewModel model)
        {
            using(var db = new BlogContext())
            {
                var updated = db.UpdatePost(id, new Post() { Title = model.Title, Description = model.Description, Body = model.Body });
                if(updated!=null)
                {
                    return RedirectToRoute("DefaultApi", new { id = updated.Slug });
                }
                return Json(new { status = "not found" });
            }
        }

        public IHttpActionResult DeletePost(string id)
        {
            using(var db = new BlogContext())
            {
                if (db.RemovePost(id))
                    return Json(new { status = "ok" });
                else
                    return Json(new { status = "not found" });
            }
        }
    }
}
