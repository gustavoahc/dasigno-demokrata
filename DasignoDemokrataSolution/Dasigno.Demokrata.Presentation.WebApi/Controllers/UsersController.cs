using AutoMapper;
using Dasigno.Demokrata.Core.Application.Services.Users;
using Dasigno.Demokrata.Presentation.WebApi.ApiModels.Requests;
using Dasigno.Demokrata.Presentation.WebApi.ApiModels.Responses;
using Dasigno.Demokrata.Presentation.WebApi.Helpers.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace Dasigno.Demokrata.Presentation.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<UserResponseModel> users = MappingConfiguration.ConvertListUsers(_mapper,
                await _userService.GetUsersAsync());

            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreationRequestModel userRequestModel)
        {
            var newUser = await _userService.InsertUserAsync(MappingConfiguration.ConvertUser(_mapper, userRequestModel));
            UserResponseModel userResponseModel = MappingConfiguration.ConvertUser(_mapper, newUser);

            return new CreatedAtRouteResult("GetUser", new { id = userResponseModel.Id }, userResponseModel);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            UserResponseModel userResponseModel = MappingConfiguration.ConvertUser(_mapper
                , user);
            return Ok(userResponseModel);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserUpdateRequestModel userRequestModel)
        {
            var user = await _userService.UpdateUserAsync(MappingConfiguration.ConvertUser(_mapper, userRequestModel));
            if (user is null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.DeleteUserAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
