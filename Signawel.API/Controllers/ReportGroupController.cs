using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signawel.API.Attributes;
using Signawel.Business.Abstractions.Services;
using Signawel.Domain;
using Signawel.Domain.Constants;
using Signawel.Domain.DataResults;
using Signawel.Dto.ReportGroup;
using Swashbuckle.AspNetCore.Annotations;

namespace Signawel.API.Controllers
{
    [ApiController]
    [JwtTokenAuthorize(Roles = Role.Constants.Admin)]
    [Route("api/reportgroups")]
    public class ReportGroupController : BaseController
    {
        private readonly IReportGroupService _reportGroupService;
        private readonly IUserService _userService;

        public ReportGroupController(IReportGroupService reportGroupService, IUserService userService)
        {
            _reportGroupService = reportGroupService;
            _userService = userService;
        }

        #region GetAllCities
        
        [HttpGet("cities")]
        [SwaggerOperation("getCities")]
        [SwaggerResponse(StatusCodes.Status200OK, "Cities found!", typeof(List<CityResponseDto>))]
        public async Task<ActionResult> GetAllCities()
        {
            var response = await _reportGroupService.GetAllCitiesAsync();
            return Ok(response.Entity);
        }
        
        #endregion
        
        #region DeleteReportGroup

        [HttpGet("users")]
        [SwaggerOperation("getUsers")]
        [SwaggerResponse(StatusCodes.Status200OK, "Users found!", typeof(List<UserResponseDto>))]
        public async Task<ActionResult> GetUsers([FromQuery] string user = null)
        {
            var response = await _userService.GetAllUsersAsync();
            var filteredResponse = response.Entity.Where(u => u.UserName.ToLower().Contains(user.ToLower())).ToList();
            if (response.HasError(ErrorCodes.InvalidOperationError))
            {
                return NotFound(response);
            }
            return Ok(filteredResponse);
        }

        [HttpDelete]
        [SwaggerOperation("deleteReportGroup")]
        [SwaggerResponse(StatusCodes.Status200OK, "ReportGroup is deleted!")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Error,ReportGroup isn't deleted!")]
        public async Task<ActionResult> DeleteReportGroup([FromQuery] string id = null)
        {
            var response = await _reportGroupService.DeleteReportGroupAsync(id);
            if(response.HasError(ErrorCodes.NotFoundError))
            {
                return NotFound(response);
            }
            
            return Ok(response);
        }
        
        #endregion

        #region GetReportGroupsByParameters

        [HttpGet]
        [SwaggerOperation("getReportGroupsByParameters")]
        [SwaggerResponse(StatusCodes.Status200OK, "ReportGroups is found!", typeof(List<ReportGroupResponseDto>))]
        public async Task<ActionResult> GetReportGroupsByParameters([FromQuery] string city = null, [FromQuery] string mail = null,[FromQuery] string user = null)
        {
            var response = await _reportGroupService.GetReportGroupsAsync(city, mail,user);
            return Ok(response.Entity);
        }
        
        #endregion
        
        #region GetReportGroupById

        [HttpGet("{id}")]
        [SwaggerOperation("getReportGroupById")]
        [SwaggerResponse(StatusCodes.Status200OK, "ReportGroup is found!", typeof(ReportGroupResponseDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Error, something went wrong!", typeof(IList<DataError>))]
        public async Task<ActionResult> GetReportGroupById(string id)
        {
            var response = await _reportGroupService.GetReportGroupByIdAsync(id);
            if(response.HasError(ErrorCodes.NotFoundError))
            {
                return NotFound(response.Errors);
            }
            return Ok(response.Entity);
        }
        
        #endregion
        
        #region AddReportGroup

        [HttpPost]
        [SwaggerOperation("addReportGroup")]
        [SwaggerResponse(StatusCodes.Status200OK, "ReportGroup created.", typeof(ReportGroupResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error, Something went wrong!", typeof(IList<DataError>))]
        public async Task<ActionResult> AddReportGroup([FromBody] ReportGroupCreationRequestDto reportGroup)
        {
            var response = await _reportGroupService.SetReportGroupAsync(reportGroup);
            if(response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }
            return Ok(response.Entity);
        }
        
        #endregion

        #region ModifyReportGroup
        
        [HttpPut("{id}")]
        [SwaggerOperation("modifyReportGroup")]
        [SwaggerResponse(StatusCodes.Status200OK, "ReportGroup modified.", typeof(ReportGroupResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error, Something went wrong!", typeof(IList<DataError>))]
        public async Task<ActionResult> ModifyReportGroup(string id,[FromBody] ReportGroupCreationRequestDto reportGroup)
        {
            var response = await _reportGroupService.ModifyReportGroupAsync(id,reportGroup);
            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }
            return Ok(response.Entity);
        }
        
        #endregion
    }
}
