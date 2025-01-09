using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GoateiCounter.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AudioController(IWebHostEnvironment webHostEnvironment) : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    [HttpGet()]
    public IActionResult GetByIndex(int index)
    {
        var folder = Path.Combine(_webHostEnvironment.WebRootPath, "sounds");
        var files = Directory.GetFiles(folder);

        Response.ContentType = new MediaTypeHeaderValue("audio/mp3").ToString();// Content type
        FileStream filestream = System.IO.File.OpenRead(files[index]);
        return File(filestream, contentType: "audio/mp3", fileDownloadName: "audio", enableRangeProcessing: true);
    }
}