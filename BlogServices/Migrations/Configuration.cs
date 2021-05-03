namespace BlogServices.Migrations
{
    using BlogServices.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogServices.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BlogServices.Models.BlogContext context)
        {

            context.Posts.AddOrUpdate(x => x.PostID,
                new Post()
                {
                    Title = "Apple confirms the hybrid workplace is here to stay",
                    Description = "\"It would seem that work from home and the productivity of working from home will remain very critical, \" said Apple CEO Tim Cook.",
                    Body = @"Work from home ‘very critical’Cook's comments came as he was discussing his company’s record-breaking Q2 21 results. “It seems like many companies will be operating in a hybrid kind of mode,” he said about record-setting Mac and iPad sales. “And so, it would seem that work from home and the productivity of working from home will remain very critical.”",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Slug = "apple-confirms-the-hybrid-workplace-is-here-to-stay",
                    Tags = new List<Tag>()
                    {
                        new Tag() { TagName = "mobile" },
                        new Tag() { TagName = "apple" },
                        new Tag() { TagName = "macos" },
                        new Tag() { TagName = "ipad" },
                        new Tag() { TagName = "remote" }
                    }
                },
                new Post()
                {
                    Title = "Internet Trends 2018",
                    Description = "Ever wonder how?",
                    Body = @"An opinionated commentary, of the most important presentation of the year",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Slug = "internet-trends-2018",
                    Tags = new List<Tag>()
                    {
                        new Tag() { TagName = "trends" },
                        new Tag() { TagName = "innovation" },
                        new Tag() { TagName = "2018" }
                    }
                },
                new Post()
                {
                    Title = "Augmented Reality iOS Application",
                    Description = "Application Guard for Office is now available in public preview!",
                    Body = @"Files from the internet and other potentially unsafe locations can contain viruses, worms, or other kinds of malware that can harm your users’ computer and data. To help protect your users, Office opens files from potentially unsafe locations in Application Guard, a secure container that is isolated from the device through hardware-based virtualization. ",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Slug = "augmented-reality-ios-application",
                    Tags = new List<Tag>()
                    {
                        new Tag() { TagName = "ios" },
                        new Tag() { TagName = "apple" },
                        new Tag() { TagName = "mobile" },
                        new Tag() { TagName = "application" },
                        new Tag() { TagName = "public-preview" }
                    }
                }
                );

        }
    }
}
