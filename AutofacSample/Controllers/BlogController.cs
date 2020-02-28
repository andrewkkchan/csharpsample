using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncDemo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutofacSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BloggingContext bloggingContext;
        public BlogController(BloggingContext bloggingContext)
        {
            this.bloggingContext = bloggingContext;
        }

        public async Task<IEnumerable<Blog>> Get()
        {
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
