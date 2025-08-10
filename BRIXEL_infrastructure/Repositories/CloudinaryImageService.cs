using BRIXEL_core.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BRIXEL_infrastructure.Services
{
    public class CloudinaryImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly string _defaultFolder;
        private static readonly HashSet<string> Allowed = new()
        { "image/jpeg", "image/png", "image/webp", "image/gif" };

        private const long MaxBytes = 10 * 1024 * 1024; // 10MB

        public CloudinaryImageService(IConfiguration cfg)
        {
            var cloud = cfg["Cloudinary:CloudName"] ?? Environment.GetEnvironmentVariable("Cloudinary__CloudName");
            var key = cfg["Cloudinary:ApiKey"] ?? Environment.GetEnvironmentVariable("Cloudinary__ApiKey");
            var sec = cfg["Cloudinary:ApiSecret"] ?? Environment.GetEnvironmentVariable("Cloudinary__ApiSecret");
            _defaultFolder = cfg["Cloudinary:Folder"] ?? Environment.GetEnvironmentVariable("Cloudinary__Folder") ?? "brixel";

            if (string.IsNullOrWhiteSpace(cloud) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(sec))
                throw new InvalidOperationException("Cloudinary credentials are missing");

            _cloudinary = new Cloudinary(new Account(cloud, key, sec)) { Api = { Secure = true } };
        }

        public async Task<(string url, string publicId)> UploadAsync(IFormFile file, string? subFolder = null, CancellationToken ct = default)
        {
            if (file == null || file.Length == 0) throw new ArgumentException("Empty file");
            if (file.Length > MaxBytes) throw new ArgumentException("File too large");
            if (!Allowed.Contains(file.ContentType.ToLower())) throw new ArgumentException("Unsupported content type");

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = string.IsNullOrWhiteSpace(subFolder) ? _defaultFolder : $"{_defaultFolder}/{subFolder}",
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false,
                Transformation = new Transformation()
                    .Quality("auto")
                    .FetchFormat("auto")
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            if (result.Error != null) throw new Exception(result.Error.Message);

            return (result.SecureUrl?.ToString() ?? "", result.PublicId);
        }

        public async Task DeleteAsync(string publicId, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(publicId)) return;

            var deletionParams = new DeletionParams(publicId)
            {
                Invalidate = true,
                ResourceType = ResourceType.Image
            };

            // بعض إصدارات CloudinaryDotNet لا تدعم CancellationToken هنا
            var res = await _cloudinary.DestroyAsync(deletionParams);
            // إن حبيت تتأكد:
            // if (res.Result != "ok") throw new Exception("Failed to delete image");
        }
    }
}
