using System.Threading.Tasks;

namespace SC.DevChallenge.DataAccess.EF.Seeder.Abstractions
{
    public interface IDbInitializer
    {
        Task InitializeAsync(string filePath);
    }
}
