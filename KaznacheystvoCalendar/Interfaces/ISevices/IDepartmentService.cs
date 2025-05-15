using KaznacheystvoCalendar.DTO.Departament;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface IDepartmentService
{
    public Task<IEnumerable<GetDeparamentsDTO>> GetAllDepartmentAsync();
    public Task<GetDeparamentsDTO?> GetDepartmentByIdAsync(int id);
    public Task<CreatedDepartamentDTO> CreateDepartmentAsync(CreateDepartamentDTO dto);
    public Task<bool> UpdateDepartmentAsync(int id,CreateDepartamentDTO dto);
    public Task<bool> DeleteDepartmentAsync(int id);
}