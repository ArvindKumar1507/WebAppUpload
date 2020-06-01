using Microsoft.EntityFrameworkCore;
using SampleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.DataAccess
{
    public class WebAppUploadContext : DbContext
    {
        public WebAppUploadContext(DbContextOptions<WebAppUploadContext> options) : base(options) // Is this code really needed.
        {
        }
        public DbSet<FileDetails> FileDetails { get; set; }
    }
}
