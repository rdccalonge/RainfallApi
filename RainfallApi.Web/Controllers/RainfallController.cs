using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RainfallApi.Application.Helpers;
using RainfallApi.Core.Entities;
using RainfallApi.Core.Error;
using RainfallApi.Core.Interfaces;
using RainfallApi.Web.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RainfallApi.Web.Controllers
{
    /// <summary>
    /// Provides methods for testing the Rainfall API.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [ApiController]
    [Route("api/[controller]")]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallService _rainfallService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Get rainfall readings by station Id
        /// </summary>
        /// <param name="rainfallService">The api service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public RainfallController(IRainfallService rainfallService, IMapper mapper)
        {
            _rainfallService = rainfallService ?? throw new ArgumentNullException(nameof(rainfallService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get rainfall readings by station Id.
        /// </summary>
        /// <param name="stationId">The id of the reading station.</param>
        /// <param name="count">The number of readings to return.</param>
        [SwaggerOperation(
        Summary = "Get rainfall readings by station Id",
            Description = "Retrieve the latest readings for the specified stationId",
            OperationId = "get-rainfall",
            Tags = new[] { "Rainfall" }
        )]
        [Produces("application/json")]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of rainfall readings successfully retrieved", typeof(RainfallReadingResponse))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Reading is empty", typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No readings found for the specified stationId", typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error", typeof(ErrorResponse))]
        [HttpGet("id/{stationId}/readings")]
        public async Task<IActionResult> GetRainfallReadings([FromRoute] string stationId, [Range(1, 100)] int count = 10)
        {
            try
            {
                if (!ValidationHelpers.IsValidStationId(stationId, out int validStationId))
                {
                    return BadRequest(new ErrorResponse
                    {
                        Error = new Error
                        {
                            Message = "Invalid request parameters",
                            Details = new ErrorDetail { PropertyName = nameof(stationId), Message = "Invalid station Id." }
                        }
                    });
                }

                var result = await _rainfallService.GetRainfallReadingsAsync(validStationId, count);

                if (result == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        Error = new Error
                        {
                            Message = "No readings found for the specified stationId",
                            Details = new ErrorDetail { Message = $"No reading found for specified stationId { stationId }" }
                        }
                    });
                }

                if (!result.Readings.Any())
                    return NoContent();

                return Ok(_mapper.Map<RainfallReadingResponseModel, RainfallReadingResponse>(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Error = new Error
                    {
                        Message = "Internal server error",
                        Details = new ErrorDetail { Message = ex.Message }
                    }
                });
            }
        }
    }
}
