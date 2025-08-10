using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRIXEL_core.Interface
{
    public interface IImageService
    {
        Task<(string url, string publicId)> UploadAsync(IFormFile file, string? subFolder = null, CancellationToken ct = default);
        Task DeleteAsync(string publicId, CancellationToken ct = default);
    }
}
