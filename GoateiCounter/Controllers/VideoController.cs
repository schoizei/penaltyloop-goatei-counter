﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace GoateiCounter.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoController(IWebHostEnvironment webHostEnvironment) : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    [HttpGet()]
    public IActionResult GetByIndex(int index)
    {
        var folder = Path.Combine(_webHostEnvironment.WebRootPath, "videos");
        var files = Directory.GetFiles(folder);

        Response.ContentType = new MediaTypeHeaderValue("video/mp4").ToString();// Content type
        FileStream filestream = System.IO.File.OpenRead(files[index]);
        return File(filestream, contentType: "video/mp4", fileDownloadName: "video", enableRangeProcessing: true);
    }
}