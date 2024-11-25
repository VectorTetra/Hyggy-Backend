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
        ImagekitClient imagekit = new ImagekitClient("public_0O6fWt547b835DVrknwauJAFZpQ=",
			"private_Ik1bzDk1h5m3KMW4v5bZw8pmQzk=", "https://ik.imagekit.io/aoy2r8vra7/");

        [HttpPost]
        [Route("upload")]
        public IActionResult Upload([FromForm] IFormFileCollection photos)
        {
            try
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
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getphoto")]
        public IActionResult GetPhotoById(string id)
        {
            try
            {

                Result res1 = imagekit.GetFileDetail(id);

                return Ok(res1);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("getphotobyurlAndDelete")]
        public IActionResult GetPhotoByUrl(string url)
        {
            try
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
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        [Route("deletephoto")]
        public IActionResult Delete(string id)
        {
            try
            {
                ResultDelete res2 = imagekit.DeleteFile(id);

                return Ok(res2);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(500, ex.InnerException.Message);
                }
                return StatusCode(500, ex.Message);
            }

        }
    }
}
