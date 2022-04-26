using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAppTwo.Data;
using ServerAppTwo.DTO;
using ServerAppTwo.Helpers;
using ServerAppTwo.Models;

namespace ServerAppTwo.Controllers
{
    // [ServiceFilter(typeof(LastActiveActionFilterr))]
   [ServiceFilter(typeof(LastActiveActionFilter))]
   [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly ISocialRepository _socialRepository;
        private readonly IMapper _mapper;

        public UserController(ISocialRepository socialRepository,IMapper mapper)
        {
            _socialRepository=socialRepository;
            _mapper=mapper;
        }
        


        public async Task<IActionResult> GetUsers([FromQuery] UserQueryParams userParams){
                userParams.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var users = await _socialRepository.GetUsers(userParams);
                var result = _mapper.Map<IEnumerable<UserForListDTO>>(users);
                return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id){
            var user = await _socialRepository.GetUser(id);
            var result =  _mapper.Map<UserForDetailDTO>(user);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,UserForUpdateDTO userForUpdateDTO)
        {
            if(id!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return BadRequest("Yetkisiz güncelleme");
            
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var user = await _socialRepository.GetUser(id);

            _mapper.Map(userForUpdateDTO,user);
        if(await _socialRepository.SaveChanges())
        return Ok();

        throw new System.Exception("Güncelleme sırasında bir hata oluştu");
        }
        
        [HttpPost("{followerUserId}/follow/{userId}")]
        public async Task<IActionResult> FollowUser(int followerUserId,int userId)
        {
            if(followerUserId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            if(followerUserId==userId)
            return BadRequest("Kendinizi takip edemezsiniz");

            var IsAllReadyFollowed= await _socialRepository.IsAllReadyFollowed(followerUserId,userId);
            if(IsAllReadyFollowed)
            return BadRequest("Zaten Takip Ediyorsunuz");

            if(await _socialRepository.GetUser(userId)==null)
            return NotFound();

            var follow = new UserToUser(){
                UserId=userId,
                FollowerId=followerUserId
            };

            _socialRepository.Add<UserToUser>(follow);

            if(await _socialRepository.SaveChanges())
            return Ok();

            return BadRequest("Bilinmeyen bir hata oluştu");
            

            

        }
    }
}