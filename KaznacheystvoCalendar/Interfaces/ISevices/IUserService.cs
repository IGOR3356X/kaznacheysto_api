using CoolFormApi.DTO.Auth;
using KaznacheystvoCalendar.DTO;
using KaznacheystvoCalendar.DTO.User;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface IUserService
{
    public Task<PaginatedResponse<GetAllUserDTO>> GetUsersAsync(QueryObject queryObject);
    public Task<GetUserDTO> GetUserByIdAsync(int id);
    public Task<User?> CreateUserAsync(CreateUserDTO userDto);
    public Task<UserSevicesErrors> UpdateUserAsync(int id,UpdateUserDTO updateUserDTO);
    public Task<bool> DeleteUserAsync(int id);
    public Task<User> LoginUserAsync(AuthDTO authDTO);
}