﻿using DTOs;
using Exam.DAL;
using Exam.Domain;
using FinalExam.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Image = Exam.Domain.Image;

namespace Exam.BL
{
    public class ImageService : IImageService
    {
        private readonly IDbRepository _dbRepository;
        public ImageService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public async Task<Image> AddImageAsync(byte[] imageBytes, string contentType)
        {
            var image = new Image
            {
                ImageBytes = imageBytes,
                ContentType = contentType
            };

            await _dbRepository.AddImageAsync(image);
            await _dbRepository.SaveChangesAsync();

            return image;
        }

        public async Task<Image> GetImageAsync(int id)
        {
            return await _dbRepository.GetImageAsync(id);
        }
        public async Task<byte[]> ResizeImage(byte[] imageBytes, string contentType)
        {
            using var memoryStream = new MemoryStream(imageBytes);
            using var originalBitmapImage = new Bitmap(memoryStream);
            var resizedImage = new Bitmap(200, 200);

            using var graphics = Graphics.FromImage(resizedImage);

            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            graphics.DrawImage(originalBitmapImage, 0, 0, 200, 200);

            using var stream = new MemoryStream();
            //var imageFormatList = contentType.Split('/');
            //var imageFormat = imageFormatList[1];
            //var test = char.ToUpper(imageFormat[0]) + imageFormat.Substring(1);
            resizedImage.Save(stream, ImageFormat.Jpeg);
            imageBytes = stream.ToArray();

            return imageBytes;
        }

        public async Task<byte[]> GetImageBytesAsync(SignupDto dto)
        {
            using var memoryStream = new MemoryStream();
            dto.PersonalInfo.ProfilePic.CopyTo(memoryStream);
            var imageBytes = memoryStream.ToArray();
            var resizedImage = await ResizeImage(imageBytes, dto.PersonalInfo.ProfilePic.ContentType);
            return resizedImage;
        }
        public async Task<byte[]> GetImageBytesForProfilePicChangeAsync(ImageUploadDto imageDto)
        {
            using var memoryStream = new MemoryStream();
            imageDto.ProfilePic.CopyTo(memoryStream);
            var imageBytes = memoryStream.ToArray();
            var resizedImage = await ResizeImage(imageBytes, imageDto.ProfilePic.ContentType);

            return resizedImage;
        }
        public async Task ChangeProfilePicAsync(int userId, byte[] imageBytes, string contentType)
        {
            await _dbRepository.ChangeProfilePicAsync(userId, imageBytes, contentType);
            await _dbRepository.SaveChangesAsync();
        }
    }
}
