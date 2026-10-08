using Microsoft.AspNetCore.Mvc;
using UnitConverter.Services;

namespace UnitConverter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitConverterController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new
            {
               status = "Working" 
            });
        }
        [HttpGet("length-converter")]
        public IActionResult LengthConversion(string from, string to, float originalValue)
        {
            List<string> errors = [];
            if (!UnitConverterService.IsValidLengthUnit(from))
            {
                errors.Add($"from unit is not valid (Valid units: {UnitConverterService.ValidLengthUnits()})");
            }
            if (!UnitConverterService.IsValidLengthUnit(from))
            {
                errors.Add($"to unit is not valid (Valid units: {UnitConverterService.ValidLengthUnits()})");
            }

            
            if(0 < errors.Count)
            {
                return BadRequest(new
                {
                    reasons = errors 
                });
            }
            
            return Ok(new
            {
                from,
                originalValue,
                to,
                convertedValue = UnitConverterService.ConvertLengthUnit(from,to,originalValue)
            });
        }

        [HttpGet("weight-converter")]
        public IActionResult WeightConversion(string from, string to, float originalValue)
        {
            List<string> errors = [];
            if (!UnitConverterService.IsValidWeightUnit(from))
            {
                errors.Add($"from unit is not valid (Valid units: {UnitConverterService.ValidWeightUnits()})");
            }
            if (!UnitConverterService.IsValidWeightUnit(from))
            {
                errors.Add($"to unit is not valid (Valid units: {UnitConverterService.ValidWeightUnits()})");
            }

            if(0 < errors.Count)
            {
                return BadRequest(new
                {
                    reasons = errors 
                });
            }

             return Ok(new
            {
                from,
                originalValue,
                to,
                convertedValue = UnitConverterService.ConvertWeightUnit(from,to,originalValue)
            });
        }

        [HttpGet("temperature-converter")]
        public IActionResult TemperatureConversion(string from, string to, float originalValue)
        {
            List<string> errors = [];
            if (!UnitConverterService.IsValidTemperatureUnit(from))
            {
                errors.Add($"from unit is not valid (Valid units: {UnitConverterService.ValidTemperatureUnits})");
            }
            if (!UnitConverterService.IsValidTemperatureUnit(from))
            {
                errors.Add($"to unit is not valid (Valid units: {UnitConverterService.ValidTemperatureUnits()})");
            }

            if(0 < errors.Count)
            {
                return BadRequest(new
                {
                    reasons = errors 
                });
            }

            return Ok(new
            {
                from,
                originalValue,
                to,
                convertedValue = UnitConverterService.ConvertTemperatureUnits(from,to,originalValue)
            });
        }
    }    
}