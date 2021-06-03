using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{year:min(2000)}/{month:range(1,12)}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {

            //parameter name is important, as that's what .NET Core looks for in the url params
            var post = new Post
            {
                Title = "My blog post",
                Posted = DateTime.Now,
                Author = "Marco Howard",
                Body = "This is a great blog post!"
            };

            return View(post);
        }
    }
}
