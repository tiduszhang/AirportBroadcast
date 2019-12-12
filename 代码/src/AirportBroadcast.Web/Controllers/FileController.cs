using System.IO;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.UI;
using Abp.Web.Mvc.Authorization;
using AirportBroadcast.Dto;
using AirportBroadcast.Storage;
using AirportBroadcast.Net.MimeTypes;
using Abp;
using AirportBroadcast.IO;
using Abp.Runtime.Session;
using Abp.Web.Models;

namespace AirportBroadcast.Web.Controllers
{
    public class FileController : AbpZeroTemplateControllerBase
    {
        private readonly IAppFolders _appFolders; 
        private readonly IBinaryObjectManager _binaryObjectManager;
        public FileController(IAppFolders appFolders, 
            IBinaryObjectManager binaryObjectManager)
        {
            _appFolders = appFolders; 
            _binaryObjectManager = binaryObjectManager; 
        }

        [AbpMvcAuthorize]
        [DisableAuditing]
        public ActionResult DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(fileBytes, file.FileType, file.FileName);
        }

       
          

        private string GetContentType(string mediaExtention) {
            if (mediaExtention.ToLower().Contains("mp4"))
            {
                return MimeTypeNames.VideoMp4;
            }
            else if (mediaExtention.ToLower().Contains("wma"))
            {
                return MimeTypeNames.VideoXMsWmv;
            }
            else if (mediaExtention.ToLower().Contains("gif")) {
                return MimeTypeNames.ImageGif;
            }
            else
            {
                return MimeTypeNames.ImageJpeg;
            }
        }



        /// <summary>
        /// 上传视频文件
        /// </summary>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public JsonResult UploadVideo()
        {
            try
            {
                //Check input
                if (Request.Files.Count <= 0 || Request.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Files[0];

                if (file.ContentLength > 1048576 * 100 * 5) //100MB.
                {
                    throw new UserFriendlyException("上传的视频不得超过500兆");
                }
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "video_" + AbpSession.GetUserId());

                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                var tempFileName = "video_" + AbpSession.GetUserId() +RandomHelper.GetRandom(100000)+ fileInfo.Extension;
                var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
                file.SaveAs(tempFilePath);

                return Json(new AjaxResponse(new { fileName = tempFileName }));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

    }
}