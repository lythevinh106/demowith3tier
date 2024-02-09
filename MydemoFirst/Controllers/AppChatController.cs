using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MydemoFirst.DataAccess.Infrastructure;
using MydemoFirst.Models.Modules.User.Models;
using MydemoFirst.Signal.DemoAppChat;

namespace MydemoFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AppChatController : GenericBaseController
    {
        private readonly IHubContext<AppChat> _appChat;

        private readonly UserManager<User> _userManager;

        private readonly IUnitOfWork _unitOfWork;


        public AppChatController(IMediator mediator, IMapper mapper
            , IHubContext<AppChat> appChat, UserManager<User> usermanager

            , IUnitOfWork unitOfWork


            ) : base(mediator, mapper)
        {
            _appChat = appChat;
            _userManager = usermanager;
            _unitOfWork = unitOfWork;

        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRoom([FromQuery] string conectionId, [FromQuery] string userId, RoomRequest roomRequest)

        //{




        //    try
        //    {
        //        User user = await _userManager.FindByIdAsync(userId);
        //        if (user != null)
        //        {
        //            bool exsitRoom = await _unitOfWork.RoomGenericResponsitory.CheckExist(r => r.Name.Equals(roomRequest.Name));

        //            if (!exsitRoom)
        //            {


        //                Room newRoom = new Room
        //                {
        //                    Name = roomRequest.Name,
        //                    Id = new Guid().ToString(),

        //                };


        //                Room newRoomModel = await _unitOfWork.RoomGenericResponsitory.Add(newRoom);

        //                RoomMember newRoomMember = _mapper.Map<RoomMember>(new RoomMemberRequest
        //                {
        //                    RoomId = newRoom.Id,
        //                    UserId = user.Id,

        //                });
        //                _unitOfWork.RoomMemberGenericResponsitory.Add(newRoomMember);
        //                _unitOfWork.SaveChanges();

        //                //  await _appChat.Groups.AddToGroupAsync(conectionId, roomRequest.Name);
        //                //   await _appChat.Clients.Client(conectionId).SendAsync("newMessage", "Tao nhom có tên là " + newRoom.Name + "thanh cong");
        //            }
        //            else
        //            {
        //                await _appChat.Clients.Client(conectionId).SendAsync("onError", "Tao nhom that bai Tên Nhóm Đã Tồn Tại!");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await _appChat.Clients.Client(conectionId).SendAsync("onError", "Tao nhom that bai!" + ex.Message);
        //    }
        //    return Accepted();

        //}

    }
}
