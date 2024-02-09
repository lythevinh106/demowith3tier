using AutoMapper;
using DTOShared.Modules.Room.Response;
using DTOShared.Modules.Room.RoomRequest;
using DTOShared.Modules.RoomMember.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using MydemoFirst.DataAccess.Infrastructure;
using MydemoFirst.Models.Modules.Room.Models;
using MydemoFirst.Models.Modules.RoomMember.Models;
using MydemoFirst.Models.Modules.User.Models;
using Serilog;

namespace MydemoFirst.Signal.DemoAppChat
{
    [Authorize]
    public class AppChat : Hub
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private static readonly Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();
        public static readonly List<UserInfo> _Connections = new List<UserInfo>();

        public AppChat(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

        private bool IsUserInGroup(string IdentityName, string roomName)
        {
            UserInfo user = _Connections.Where(u => u.UserName == IdentityName).FirstOrDefault();

            bool existInRoom = false;
            existInRoom = _unitOfWork.RoomGenericResponsitory
                    .Find(r => r.Name == roomName && r.RoomMembers.Any(rm => rm.UserId == user.Id))
                    .FirstOrDefault() != null;

            return user != null && existInRoom;
        }

        private UserInfo getUserInfo()
        {
            return _Connections.Where(u => u.UserName == IdentityName).FirstOrDefault();
        }

        public override async Task OnConnectedAsync()
        {
            Log.Information("kêt nối tới soccket thanh cong------");
            try
            {
                var user = await _userManager.FindByEmailAsync(IdentityName);

                UserInfo userInfo = _mapper.Map<UserInfo>(user);
                var exist = _Connections.Any(u => u.Email == userInfo.Email);
                if (!exist)
                {
                    _Connections.Add(userInfo);
                    _ConnectionsMap.Add(IdentityName, Context.ConnectionId);
                }

                await Clients?.Caller.SendAsync("getProfileInfo", user.Email);



                //add into group joined
                //var roomHasJoined = user.RoomMembers.Include(rm => rm.Room);
                List<RoomRequest> roomHasJoined = _unitOfWork.RoomMemberGenericResponsitory.Find(rm => rm.UserId == user.Id)
                  .Select(rm => new RoomRequest { Name = rm.Room.Name }).ToList();

                foreach (var room in roomHasJoined)
                {

                    await Groups.AddToGroupAsync(Context.ConnectionId, room.Name);
                }


                List<RoomMember> allRoomOfUser = user.RoomMembers;

                if (allRoomOfUser.Count > 0)
                {
                    var listRoomsWithCountMemBer = allRoomOfUser.Select(aru => new RoomsWithCountMemBer
                    {
                        countMember = aru.Room.RoomMembers.Count,
                        Room = _mapper.Map<RoomResponse>(aru.Room)
                    });


                    await Clients.Caller.SendAsync("GetAllRooms", listRoomsWithCountMemBer);
                }
            }
            catch (Exception ex)
            {
                Clients?.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Log.Information("đã ngắt kêt nối tới socket------");
            try
            {
                UserInfo user = getUserInfo();

                _Connections.Remove(user);

                // Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                _ConnectionsMap.Remove(IdentityName);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageIntoGroup(string message, string roomName)
        {
            bool IsExistUserInGroup = IsUserInGroup(IdentityName, roomName);

            try
            {
                if (IsExistUserInGroup)
                {
                    await Clients.Groups(roomName).SendAsync("newGroupMessage", getUserInfo(), message, roomName);
                }
                else
                {
                    await Clients.Caller.SendAsync("onError", "Hien tai ban k nam trong nhom nay nen k the gui tin nhan!");
                }
            }

            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "Gui tin nhan bi loi!" + ex.Message);
            }



        }

        public async Task CreateGroup(string roomName)
        {
            User user = await _userManager.FindByEmailAsync(IdentityName);

            try
            {
                if (user != null)
                {
                    bool exsitRoom = await _unitOfWork.RoomGenericResponsitory.CheckExist(r => r.Name.Equals(roomName));

                    if (!exsitRoom)
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

                        Room newRoom = new Room
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = roomName,
                        };

                        Room newRoomModel = await _unitOfWork.RoomGenericResponsitory.Add(newRoom);

                        RoomMember newRoomMember = _mapper.Map<RoomMember>(new RoomMemberRequest
                        {
                            RoomId = newRoomModel.Id,
                            UserId = user.Id,
                        });
                        var newRoomMemberModel = _unitOfWork.RoomMemberGenericResponsitory.Add(newRoomMember);

                        _unitOfWork.SaveChanges();

                        await Clients.Caller.SendAsync("newMessage", "Tao nhom có tên là " + newRoom.Name + "thanh cong");
                        int countMember = _unitOfWork.RoomMemberGenericResponsitory.CountMembers(newRoomMember.RoomId);

                        await Clients.Caller.SendAsync("addRoom", new RoomsWithCountMemBer
                        {
                            countMember = countMember,
                            Room = _mapper.Map<RoomResponse>(newRoomModel)
                        });
                    }
                    else
                    {
                        await Clients.Caller.SendAsync("onError", "Tao nhom that bai Tên Nhóm Đã Tồn Tại!");
                    }
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "Tao nhom that bai!" + ex.Message);
            }
        }

        public async Task joinGroup(string roomName)
        {
            try
            {
                bool IsExistUserInGroup = IsUserInGroup(IdentityName, roomName);

                if (!IsExistUserInGroup)
                {
                    Room room = await _unitOfWork.RoomGenericResponsitory.GetByName(roomName);
                    User user = await _userManager.FindByEmailAsync(IdentityName);


                    RoomMember newRoomMember = _mapper.Map<RoomMember>(new RoomMemberRequest
                    {
                        RoomId = room.Id,
                        UserId = user.Id,
                    });
                    var newRoomMemberModel = _unitOfWork.RoomMemberGenericResponsitory.Add(newRoomMember);




                    _unitOfWork.SaveChanges();

                    int countMember = _unitOfWork.RoomMemberGenericResponsitory.CountMembers(room.Id);

                    await Clients.Caller.SendAsync("addRoom", new RoomsWithCountMemBer
                    {
                        countMember = countMember,
                        Room = _mapper.Map<RoomResponse>(room)
                    });
                    List<RoomMember> allRoomOfUser = user.RoomMembers;

                    if (allRoomOfUser.Count > 0)
                    {
                        var listRoomsWithCountMemBer = allRoomOfUser.Select(aru => new RoomsWithCountMemBer
                        {
                            countMember = aru.Room.RoomMembers.Count,
                            Room = _mapper.Map<RoomResponse>(aru.Room)
                        });


                        await Clients.Caller.SendAsync("GetAllRooms", listRoomsWithCountMemBer);
                    }

                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                    await Clients.OthersInGroup(roomName).SendAsync("messageInRoom", getUserInfo(), room);
                    await Clients.OthersInGroup(roomName).SendAsync("addUser", getUserInfo());
                }
                else
                {
                    await Clients.Caller.SendAsync("onError", "Bạn da o trong phong nay k can phai tham gia lại");
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }
        public async Task LeaveGroup(string roomName)
        {
            bool IsExistUserInGroup = IsUserInGroup(IdentityName, roomName);

            try
            {
                if (IsExistUserInGroup)
                {
                    Room room = await _unitOfWork.RoomGenericResponsitory.GetByName(roomName);
                    User user = await _userManager.FindByEmailAsync(IdentityName);

                    RoomMember curentRoomMember = _unitOfWork.RoomMemberGenericResponsitory
                        .Find(rm => rm.RoomId == room.Id && rm.UserId == user.Id).FirstOrDefault();

                    if (curentRoomMember != null)
                    {
                        _unitOfWork.RoomMemberGenericResponsitory.Delete(curentRoomMember);


                        if ((room.RoomMembers.Count - 1) <= 0)
                        {
                            _unitOfWork.RoomGenericResponsitory.Delete(room);

                        }



                        _unitOfWork.SaveChanges();


                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

                        await Clients.Caller.SendAsync("removeRoom", roomName);

                    }


                }
                else
                {
                    await Clients.Caller.SendAsync("onError", "Ban k nam trong nhom nay nữa");

                }

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }
    }
}