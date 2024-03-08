using LittleLemonAPI.Models;

namespace LittleLemonAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Staff staffUser);
    }
}
