using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAll();
        Room GetById(int id);
        Room Create(Room room);
        void Update(Room room);
        void Delete(int id);
    }

    public class RoomService : IRoomService
    {
        private DataContext _context;

        public RoomService(DataContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Room> GetAll()
        {
            return _context.Rooms;
        }

        public Room GetById(int id)
        {
            return _context.Rooms.Find(id);
        }

        public Room Create(Room room)
        {
            if (_context.Rooms.Any(x => x.Name == room.Name))
                throw new AppException("Room with name \"" + room.Name + "\" already exists...");
            
            _context.Rooms.Add(room);
            _context.SaveChanges();

            return room;
        }

        public void Update(Room roomParam)
        {
            var room = _context.Rooms.Find(roomParam.Id);

            if (room == null)
                throw new AppException("Room not found");

            if (room.Name != room.Name)
            {
                // username has changed so check if the new username is already taken
                if (_context.Rooms.Any(x => x.Name == roomParam.Name))
                    throw new AppException("Room " + roomParam.Name + " is already taken");
            }

            // update user properties
            room.Name = roomParam.Name;
            room.MaxPlayersCount = roomParam.MaxPlayersCount;

            _context.Rooms.Update(room);
            _context.SaveChanges();
        }

        public Room AddPlayer(int roomId, int userId)
        {
            var room = _context.Rooms.Find(roomId);

            if (room == null)
                throw new AppException("Room not found");

            var user = _context.Users.Find(userId);

            if (user == null)
                throw new AppException("User not found");

            room.Players.Add(user);

            _context.Rooms.Update(room);
            _context.SaveChanges();

            return room;
        }

        public Room RemovePlayer(int roomId, int userId)
        {
            var room = _context.Rooms.Find(roomId);

            if (room == null)
                throw new AppException("Room not found");

            room.Players.Remove(room.Players.FirstOrDefault(user => user.Id == userId));

            _context.Rooms.Update(room);
            _context.SaveChanges();

            return room;
        }

        public void Delete(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
        }     
    }
}