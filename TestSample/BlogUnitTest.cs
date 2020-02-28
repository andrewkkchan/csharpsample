using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncDemo;
using AutofacSample.Controllers;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace TestSample
{
    public class BlogUnitTest
    {
        [Fact]
        public async Task TestGet()
        {
            BloggingContext._created = true;
            var mockContext = new Mock<BloggingContext>();
            var data = new List<Blog>
            {
                new Blog { Name = "BBB" },
                new Blog { Name = "ZZZ" },
                new Blog { Name = "AAA" },
            }.AsQueryable();
            var mockSet = data.BuildMockDbSet();
            mockContext.Setup(m => m.Blogs).Returns(mockSet.Object);
            BlogController blogController = new BlogController(mockContext.Object);
            IEnumerable<Blog> enumerable = await blogController.Get();
            Assert.True(enumerable.Any());
            mockSet.Verify(m => m.AddAsync(It.IsAny<Blog>(), default), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());

        }
    }
}
