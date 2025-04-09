using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SignatureController : ControllerBase
{
    private readonly SignatureService _signatureService;

    public SignatureController(SignatureService signatureService)
    {
        _signatureService = signatureService;
    }

    [HttpGet("keys")]
    public IActionResult GetKeys()
    {
        var keys = _signatureService.GetKeys();
        return Ok(keys);
    }

    [HttpPost("sign")]
    public IActionResult Sign([FromBody] string data)
    {
        var signature = _signatureService.SignData(data);
        return Ok(new { Signature = signature });
    }

    [HttpPost("verify")]
    public IActionResult Verify([FromBody] SignatureRequest request)
    {
        var isValid = _signatureService.VerifySignature(request.Data, request.Signature);
        return Ok(new { IsValid = isValid });
    }
}

public class SignatureRequest
{
    public string Data { get; set; }
    public string Signature { get; set; }
}
