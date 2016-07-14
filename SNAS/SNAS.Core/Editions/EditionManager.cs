using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace SNAS.Core.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public const string DefaultEditionName = "Standard";

        public EditionManager(
            IRepository<Edition> editionRepository, 
            IRepository<EditionFeatureSetting, long> editionFeatureRepository)
            : base(
                editionRepository, 
                editionFeatureRepository
            )
        {
        }
    }
}
