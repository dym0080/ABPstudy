using System.Linq;
using Abp.Application.Editions;

using SNAS.EntityFramework;
using SNAS.Core.Editions;

namespace SNAS.Migrations.SeedData
{
    public class DefaultEditionsBuilder
    {
        private readonly SNASDbContext _context;

        public DefaultEditionsBuilder(SNASDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                //TODO: Add desired features to the standard edition, if wanted!
            }   
        }
    }
}