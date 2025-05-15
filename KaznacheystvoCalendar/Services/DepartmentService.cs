using AutoMapper;
using KaznacheystvoCalendar.DTO.Departament;
using KaznacheystvoCalendar.Interfaces;
using KaznacheystvoCalendar.Interfaces.ISevices;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Services;

public class DepartmentService:IDepartmentService
{
    private readonly IGenericRepository<Department> _departmentRepo;
    private readonly IMapper _mapper;
    
    public DepartmentService(IGenericRepository<Department> departmentRepo, IMapper mapper)
    {
        _departmentRepo = departmentRepo;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<GetDeparamentsDTO>> GetAllDepartmentAsync()
    {
        return _mapper.Map<IEnumerable<GetDeparamentsDTO>>(await _departmentRepo.GetAllAsync());
    }

    public async Task<GetDeparamentsDTO?> GetDepartmentByIdAsync(int id)
    {
        return _mapper.Map<GetDeparamentsDTO>(await _departmentRepo.GetByIdAsync(id));
    }

    public async Task<CreatedDepartamentDTO> CreateDepartmentAsync(CreateDepartamentDTO dto)
    {
        return _mapper.Map<CreatedDepartamentDTO>(await _departmentRepo.CreateAsync(_mapper.Map<Department>(dto)));
    }

    public async Task<bool> UpdateDepartmentAsync(int id, CreateDepartamentDTO dto)
    {
        var entity = await _departmentRepo.GetByIdAsync(id);
        if(entity == null)
            return false;
        entity.Name = dto.Name;
        await _departmentRepo.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteDepartmentAsync(int id)
    {
        var isExist = await _departmentRepo.GetByIdAsync(id);
        if(isExist == null)
            return false;
        await _departmentRepo.DeleteAsync(isExist);
        return true;
    }
}