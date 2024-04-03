using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Users;

namespace Acme.BookStore
{
    [Authorize]
    public class ProfileAppService : ApplicationService
    {
        private readonly IBlobContainer<ProfilePictureContainer> _blobContainer;

        public ProfileAppService(IBlobContainer<ProfilePictureContainer> blobContainer)
        {
            _blobContainer = blobContainer;
        }

        public async Task SaveProfilePictureAsync(byte[] bytes)
        {
            var blobName = CurrentUser.GetId().ToString();
            await _blobContainer.SaveAsync(blobName, bytes);
        }

        public async Task<byte[]> GetProfilePictureAsync()
        {
            var blobName = CurrentUser.GetId().ToString();
            return await _blobContainer.GetAllBytesOrNullAsync(blobName);
        }
    }


}
