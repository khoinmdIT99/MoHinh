namespace TechAndTools.Services.Contracts
{
    using TechAndTools.Data.Models;

    using System.Threading.Tasks;

    public interface IUserService
    {
        TechAndToolsUser GetUserByUsername(string username);

        TechAndToolsUser GetUserById(string userId);

        Task<bool> EditFirstName(TechAndToolsUser user, string firstName);

        Task<bool> EditLastName(TechAndToolsUser user, string lastName);
    }
}
