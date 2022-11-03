using Exam.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.BL
{
    public interface IImageService
    {
        Task<Image> AddImageAsync(byte[] imageBytes,  string contentType);
        Task<Image> GetImageAsync(int id);
        Task<byte[]> ResizeImage(byte[] imageBytes, string contentType);
    }
}
