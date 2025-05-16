using AutoMapper;
using KaznacheystvoCalendar.DTO.Location;
using KaznacheystvoCalendar.Interfaces;
using KaznacheystvoCalendar.Interfaces.ISevices;
using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Services;

public class LocationService: ILocationService
{
    private readonly IGenericRepository<Location> _locationRepo;
    private readonly IMapper _mapper;

    public LocationService(IGenericRepository<Location> locationRepo, IMapper mapper)
    {
        _locationRepo = locationRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetLocationDTO>> GetLocationAsync()
    {
        return _mapper.Map<IEnumerable<GetLocationDTO>>(await _locationRepo.GetAllAsync());
    }

    public async Task<GetLocationDTO> GetLocationByIdAsync(int id)
    {
        return _mapper.Map<GetLocationDTO>(await _locationRepo.GetByIdAsync(id));
    }

    public async Task<CreatedLocationDTO> CreateLocationAsync(CreateLocationDTO dto)
    {
        return _mapper.Map<CreatedLocationDTO>(await _locationRepo.CreateAsync(_mapper.Map<Location>(dto)));
    }

    public async Task<bool> UpdateLocationAsync(int id,CreateLocationDTO dto)
    {
        var entity = await _locationRepo.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Name = dto.Name;
        await _locationRepo.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteLocationAsync(int id)
    {
        var entity = await _locationRepo.GetByIdAsync(id);
        if (entity == null) return false;
        await _locationRepo.DeleteAsync(entity);
        return true;
    }
}