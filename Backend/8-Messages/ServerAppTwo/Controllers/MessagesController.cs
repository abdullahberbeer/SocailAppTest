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
    [ServiceFilter(typeof(LastActiveActionFilter))]
    [Authorize]
    [ApiController]
    [Route("api/user/{userId}/[controller]")]
    public class MessagesController:ControllerBase
    {
           private ISocialRepository _socialRepository;
        private IMapper _mapper;
        public MessagesController(ISocialRepository socialRepository,IMapper mapper)
        {
            _socialRepository=socialRepository;
            _mapper=mapper;
        }
        [HttpPost]
    public async Task<IActionResult> CreateMessage(int userId,MessageForCreateDTO messageForCreateDTO){
        if(userId!=int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        return Unauthorized();

        messageForCreateDTO.SenderId=userId;

        var recipient = await _socialRepository.GetUser(messageForCreateDTO.RecipientId);

        if(recipient==null)
        return BadRequest("Mesaj göndereceğiniz kişi bulunamadı");

        var message = _mapper.Map<Message>(messageForCreateDTO);

        _socialRepository.Add(message);

        if(await _socialRepository.SaveChanges()){
 var messageDTO= _mapper.Map<MessageForCreateDTO>(message);
  return Ok();
        }
       throw new System.Exception("error");
       
    }
    }
}