using System.Threading;
using System.Threading.Tasks;

namespace SC.DevChallenge.DataAccess.EF.Seeder.Abstractions
{
    public interface IDbInitializer
    {
        Task InitializeAsync(CancellationToken cancellationToken = default);
    }
}
