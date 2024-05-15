using SoldierInfoContext;
using ValorVault.Models;

namespace ValorVault.Services.SourceService
{
    public interface ISourceService
    {
        Task AddSource(Source model);
    }

    public class SourceService : ISourceService
    {
        private readonly SoldierInfoDbContext _context;

        public SourceService(SoldierInfoDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddSource(Source model)
        {
            var source = new Source
            {
                url = model.url,
                source_name = model.source_name,
            };

            _context.sources.Add(source);
            await _context.SaveChangesAsync();
        }
    }
}