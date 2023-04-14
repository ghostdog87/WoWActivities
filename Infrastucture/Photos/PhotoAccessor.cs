﻿using Application.Interfaces;
using Application.Photos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastucture.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;

        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var account = new Account()
            {
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecret,
            };

            _cloudinary = new Cloudinary(account);
        }

        public async Task<PhotoUploadResult> AddPhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using Stream stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };

                ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null) 
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                return new PhotoUploadResult()
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString(),
                };
            }

            return null;
        }

        public async Task<string> DeletePhoto(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            DeletionResult result = await _cloudinary.DestroyAsync(deletionParams);
            return result.Result == "ok" ? result.Result : null;
        }
    }
}
