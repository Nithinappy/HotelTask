using Dapper;
using HotelTask.Models;
using HotelTask.Utilities;

namespace HotelTask.Repositories;

public interface IRoomRepository
{
    Task<Room> Create(Room Item);
    Task Update(Room Item);
    Task<bool> Delete(int Id);
    Task<List<Room>> GetList();
    Task<Room> GetById(int Id);
    Task<List<Room>> GetListByGuestId(int GuestId);
}

public class RoomRepository : BaseRepository, IRoomRepository
{
    public RoomRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Room> Create(Room Item)
    {
        var query = $@"INSERT INTO {TableNames.room} 
        (type, size, price, staff_id) 
        VALUES (@Type, @Size, @Price, @StaffId) 
        RETURNING *";
        using (var con = NewConnection)
            return await con.QuerySingleAsync<Room>(query, Item);

    }

    public async Task<bool> Delete(int Id)
    {
        var query = $@"DELETE FROM {TableNames.room} WHERE id = @Id";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { Id }) > 0;

    }

    public async Task<Room> GetById(int Id)
    {
        var query = $@"SELECT r.*, s.name AS staff_name FROM {TableNames.room} r
        LEFT JOIN {TableNames.staff} s ON s.id = r.staff_id 
        WHERE r.id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Room>(query, new { Id });
    }

    public async Task<List<Room>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.room}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Room>(query)).AsList();

    }

    public async Task<List<Room>> GetListByGuestId(int GuestId)
    {
        var query = $@"SELECT r.* FROM {TableNames.schedule} s 
        LEFT JOIN {TableNames.room} r ON r.room_id = s.room_id 
        WHERE s.guest_id = @GuestId";

        // LEFT JOIN {TableNames.guest} g ON g.id = s.guest_id 

        using (var con = NewConnection)
            return (await con.QueryAsync<Room>(query, new { GuestId })).AsList();
    }

    public Task Update(Room Item)
    {
        throw new NotImplementedException();
    }
}