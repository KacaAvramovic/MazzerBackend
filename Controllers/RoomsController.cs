using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers.States;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomService _roomService;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public RoomsController(
            IRoomService roomService,
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _roomService = roomService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost()]
        public IActionResult Create([FromBody]Room room)
        {

            LobbyState lobbyState = new LobbyState();
            lobbyState.DoAction(room);

            var createdRoom = _roomService.Create(room);

            if (createdRoom == null)
                return BadRequest(new { message = "Room could not be created." });

            return Ok(createdRoom);
        }


        [HttpPost()]
        [Route("AddUser")]
        public IActionResult AddPlayer([FromBody]int roomId, int playerId)
        {
            var room = _roomService.GetById(roomId);
            var player = _userService.GetById(playerId);
            room.Players.Add(player);

            //if all players here
            InGameState inGameState = new InGameState();
            inGameState.DoAction(room);


            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rooms =  _roomService.GetAll();
            var roomDtos = _mapper.Map<IList<RoomDto>>(rooms);
            return Ok(roomDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room =  _roomService.GetById(id);
            var roomDto = _mapper.Map<UserDto>(room);
            return Ok(roomDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]RoomDto roomDto)
        {
            // map dto to entity and set id
            var room = _mapper.Map<Room>(roomDto);
            room.Id = id;

            try 
            {
               
                // save 
                _roomService.Update(room);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roomService.Delete(id);
            return Ok();
        }
    }
}
