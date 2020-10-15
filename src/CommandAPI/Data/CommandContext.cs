using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Controllers.Data
{
    public class CommandContext: DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options): base(options)
        {
            
        }
        
        public DbSet<Command> CommandItems { get; set; }
    }
}