namespace ProniaHomeTask.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _env;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024;

        public FileUploadService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveImageAsync(IFormFile file, string subFolder = "uploads/products")
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("Şəkil faylı seçilməyib.");

            if (file.Length > MaxFileSizeBytes)
                throw new InvalidOperationException("Şəkil ölçüsü 5 MB-dan böyük ola bilməz.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                throw new InvalidOperationException("Yalnız .jpg, .jpeg, .png, .gif və .webp faylları qəbul olunur.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, subFolder.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"{subFolder}/{fileName}".Replace('\\', '/');
        }

        public void DeleteImage(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath) || relativePath.Contains("no-image", StringComparison.OrdinalIgnoreCase))
                return;

            var fullPath = Path.Combine(_env.WebRootPath, relativePath.Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
