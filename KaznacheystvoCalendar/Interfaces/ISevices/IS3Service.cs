namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface IS3Service
{
    public Task<string> UploadFileAsync(IFormFile file, int id);
}