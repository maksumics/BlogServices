# BlogServices

## Info 
#### This application implements basic functions for blog site - CRUD operations. Tested with Postman agent for operations GET, POST, PUT and DELETE. I have developed it in ASP.NET Web API, standard .NET Framework 4.7.2 and EntityFramework 6. Database is not attached becouse approach is code first. In BlogContext class database is called "BlogDatabase", with one migration that will seed some data in databse for testing first without POST request. Database will create on default instance of SQL Server, in my case it is LocalDB. 

## Instruction for using
#### When clone project, first need to install some nuget packages that package manager will automaticly detect, then run command update-databse. This will create database "BlogDatabase" and seed some blog posts and tags. After that api is ready for usage. 
### GET api/posts
#### Return all posts ordered by most recent.
```
{
    "blogPosts": [
        {
            "Slug": "apple-confirms-the-hybrid-workplace-is-here-to-stay",
            "Title": "Apple confirms the hybrid workplace is here to stay",
            "Description": "\"It would seem that work from home and the productivity of working from home will remain very critical, \" said Apple CEO Tim Cook.",
            "Body": "Work from home ‘very critical’Cook's comments came as he was discussing his company’s record-breaking Q2 21 results. “It seems like many companies will be operating in a hybrid kind of mode,” he said about record-setting Mac and iPad sales. “And so, it would seem that work from home and the productivity of working from home will remain very critical.”",
            "CreatedAt": "2021-05-03T00:35:06.44",
            "UpdatedAt": "2021-05-03T00:35:06.44",
            "Tags": [
                "mobile",
                "apple",
                "macos",
                "ipad",
                "remote"
            ]
        },
        {
            "Slug": "internet-trends-2018",
            "Title": "Internet Trends 2018",
            "Description": "Ever wonder how?",
            "Body": "An opinionated commentary, of the most important presentation of the year",
            "CreatedAt": "2021-05-03T00:35:06.44",
            "UpdatedAt": "2021-05-03T00:35:06.44",
            "Tags": [
                "trends",
                "innovation",
                "2018"
            ]
        }
    ],
    "postsCount": 2
}
```
### GET api/posts/:slug
#### This will return a single post by slug.
```
{
    "blogPost": {
        "Slug": "internet-trends-2018",
        "Title": "Internet Trends 2018",
        "Description": "Ever wonder how?",
        "Body": "An opinionated commentary, of the most important presentation of the year",
        "CreatedAt": "2021-05-03T00:35:06.44",
        "UpdatedAt": "2021-05-03T00:35:06.44",
        "Tags": [
            "trends",
            "innovation",
            "2018"
        ]
    }
}
```
### GET api/posts/?tagName=
#### Return all posts where certain tag is listed.
### POST api/posts
#### Create new post, with reques body in following form:
```
 {
    "title": "Title of the post",
    "description": "A short description",
    "body": "Content of the article",
    "tags": ["some", "tags"]
 }
```
### PUT api/posts:slug
#### Send body request as bellow to update post specified via slug. If post cannot be found, as response, status will be not found. Optional parameters is title, description and body.
```
 {
    "title": "Title of the post",
    "description": "A short description",
    "body": "Content of the article"
 }
```
### DELETE api/posts/:slug
#### This will delete post with selected slug. Tags, if any, of the post will stay in database. If there is no post like slug in uri, method will send result like JSON bellow:
```
{
    "status": "not found"
}
```
### GET api/tags
#### At last, there is method for retrieving all tags from databse. 

## Details about development
#### On this project is used SQL Server and Entity Framework 6 for the database operations. Code first is, for me, sounds like great approach for this example, where generated SQL is not so important and we dont have too much data in databse. On first migration that you will run when download project, EF will seed database with two posts and few tags. Database will be created on default instance. I havent written connection string, becouse I dont know what is on your side, which instance is in use and which authentication is set.
#### To divide database objects from JSON data, I have used view models with some data annotations for validating model state. If post that is selected by slug for delete or update, you will get custom JSON response with status and message.
#### For ident the post, in api we use the slug. Slug is generated by title on post creating, but if we try to insert post with the same title as one in the database, the system will automaticly add "-2" to the slug. 
#### Tags are case-sensitive, so Microsoft and microsoft aren't same, becouse in assigment that is not specified. Tags and posts are represents like two tables in database and connected with many to many relationship.
