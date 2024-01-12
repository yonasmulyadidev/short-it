using Application.Features.Url.Commands.GenerateUrl;
using Application.Features.Url.DTOs;
using Application.Features.Url.Queries.GetAllUrls;
using Application.Features.Url.Queries.GetUrlByBase64PrefixQuery;
using Application.Features.Url.Queries.GetUrlById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortenerApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UrlController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UrlController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IReadOnlyList<UrlResponse>> GetAllUrls()
    {
        var response = await _mediator.Send(new GetAllUrlsQuery());

        return response;
    }
    
    [HttpGet("{urlId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetUrlById(string urlId)
    {
        var response = await _mediator.Send(new GetUrlByIdQuery(urlId));
    
        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GenerateUrl([FromBody] GenerateUrlCommand request)
    {
        var response = await _mediator.Send(request);
    
        return CreatedAtAction(nameof(GetAllUrls), response);
    }

    //Todo: check if 302 is required for the redirect status
    [HttpGet("redirect/{uriPrefix}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> RedirectUrl(string uriPrefix)
    {
        var response = await _mediator.Send(new GetUrlByBase64PrefixQuery(uriPrefix));

        return Redirect(response.LongUrl);
    }
}
