using KaznacheystvoCalendar.DTO.Location;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface ILocationService
{
    public Task<IEnumerable<GetLocationDTO>> GetLocationAsync();
    public Task<GetLocationDTO> GetLocationByIdAsync(int id);
    public Task<CreatedLocationDTO> CreateLocationAsync(CreateLocationDTO dto);
    public Task<bool> UpdateLocationAsync(int id,CreateLocationDTO dto);
    public Task<bool> DeleteLocationAsync(int id);
}