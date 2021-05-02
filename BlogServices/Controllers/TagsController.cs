using BlogServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogServices.Controllers
{
    public class TagsController : ApiController
    {
        public IHttpActionResult GetTags()
        {
            var tagNames = new List<string>();
            using (var db = new BlogContext())
            {
                var tags = db.GetAllTags();

                
                foreach (var item in tags)
                {
                    tagNames.Add(item.TagName);
                }
            }
            var output = new { tags = tagNames };
            return Json(output);
        }
    }
}
