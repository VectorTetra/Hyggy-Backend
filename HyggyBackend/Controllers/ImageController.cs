using Imagekit.Sdk;
using Imagekit.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HyggyBackend.Controllers
{
    [ApiController]
    [Route("api/Image")]
    public class ImageController : ControllerBase
    {
        ImagekitClient imagekit = new ImagekitClient("public_M7rp5a+A03bMXqAwHwTbXJozb2Q=",
          "private_XDeaQHKGVMyTnXaAJfkHZuGc+nQ=", "https://ik.imagekit.io/viktochonov/");

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromForm] List<IFormFile> photos)
        {
            if (photos == null || photos.Count == 0)
            {
                return BadRequest("No files received.");
            }

            var responses = new List<string>();
            foreach (var file in photos)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    var request = new FileCreateRequest
                    {
                        file = fileBytes,
                        fileName = file.FileName
                    };
                    var resp = imagekit.Upload(request);
                    responses.Add(resp.url);
                }
            }

            return Ok(responses);
        }

        [HttpGet]
        [Route("getphoto")]
        public IActionResult GetPhotoById(string id)
        {
            Result res1 = imagekit.GetFileDetail(id);

            return Ok(res1);
        }
        [HttpGet]
        [Route("getphotobyurlAndDelete")]
        public IActionResult GetPhotoByUrl(string url)
        {
            var name = Path.GetFileNameWithoutExtension(url) + Path.GetExtension(url);
            GetFileListRequest model = new GetFileListRequest
            {
                Name = name
            };

            ResultList res = imagekit.GetFileListRequest(model);
            JArray jsonString = JArray.Parse(res.Raw);
            var firstElement = jsonString[0];
            string id = (string)firstElement["fileId"];
            ResultDelete res2 = imagekit.DeleteFile(id);

            return Ok(res2);
        }
        [HttpDelete]
        [Route("deletephoto")]
        public IActionResult Delete(string id)
        {
            ResultDelete res2 = imagekit.DeleteFile(id);

            return Ok(res2);
        }
    }
}
