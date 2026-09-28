using FlowDesk.Api.DTOs;

namespace FlowDesk.Api.Services.Interface
{
    public interface IUserService
    {
        Task<List<UsersDto>> GetAllAsync();
        Task<UsersDto> CreateAsync(CreateUserDto dto);
        Task<UsersDto?> GetByIdAsync(int id);
    }
}
