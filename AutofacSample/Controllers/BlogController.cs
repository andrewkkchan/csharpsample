using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncDemo;
using AutofacSample.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AutofacSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BloggingContext bloggingContext;
        private readonly MyConfiguration myConfiguration;
        public BlogController(BloggingContext bloggingContext, IOptions<MyConfiguration> options)
        {
            this.bloggingContext = bloggingContext;
            this.myConfiguration = options.Value;
        }

        public async Task<IEnumerable<Blog>> Get()
        {

            if (myConfiguration.MyProperty) {
                Console.WriteLine("Get the property of app json");
                Console.WriteLine("My lucky number is " + myConfiguration.MyNumber); 
            }
            // Create a new blog and save it
            await bloggingContext.Blogs.AddAsync(new Blog
            {
                Name = "Test Blog #" + (bloggingContext.Blogs.Count() + 1)
            });
            Console.WriteLine("Calling SaveChanges.");
            await bloggingContext.SaveChangesAsync();
            Console.WriteLine("SaveChanges completed.");

            // Query for all blogs ordered by name
            Console.WriteLine("Executing query.");
            var blogs = await (from b in bloggingContext.Blogs
                         orderby b.Name
                         select b).ToListAsync();

            return blogs;
        }
    }
}
