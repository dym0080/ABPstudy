using System.Threading.Tasks;

namespace Qm.Lims.Data
{
    public interface ILimsDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
