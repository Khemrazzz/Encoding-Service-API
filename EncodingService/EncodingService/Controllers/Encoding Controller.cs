using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EncodingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncodingController : ControllerBase
    {
        // POST api/encoding/encode
        [HttpPost("encode")]
        public ActionResult<string> Encode([FromBody] string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Input text cannot be null or empty.");
            }

            // Convert the input text to a byte array and encode it as Base64
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            string encodedText = Convert.ToBase64String(textBytes);

            return Ok(encodedText);
        }

        // POST api/encoding/decode
        [HttpPost("decode")]
        public ActionResult<string> Decode([FromBody] string base64Text)
        {
            if (string.IsNullOrEmpty(base64Text))
            {
                return BadRequest("Input Base64 text cannot be null or empty.");
            }

            try
            {
                // Decode the Base64 string back to a byte array and convert it to the original string
                byte[] textBytes = Convert.FromBase64String(base64Text);
                string decodedText = Encoding.UTF8.GetString(textBytes);

                return Ok(decodedText);
            }
            catch (FormatException)
            {
                return BadRequest("Input is not a valid Base64-encoded string.");
            }
        }
    }
}
