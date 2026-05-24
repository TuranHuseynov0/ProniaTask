namespace ProniaHomeTask.Services
{
    public interface IFileUploadService
    {
        Task<string> SaveImageAsync(IFormFile file, string subFolder = "uploads/products");
        void DeleteImage(string? relativePath);
    }
}
