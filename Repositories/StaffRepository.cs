using Dapper;
using HotelTask.Models;
using HotelTask.Utilities;

namespace HotelTask.Repositories;

public interface IStaffRepository
{
    Task<Staff> Create(Staff Item);
    Task<bool> Update(Staff Item);
    Task<bool> Delete(int Id);
    Task<List<Staff>> GetList();
    Task<List<Staff>> GetStaffRoomById(int Id);
    Task<Staff> GetById(int Id);
}

public class StaffRepository : BaseRepository, IStaffRepository
{
    public StaffRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Staff> Create(Staff Item)
    {
        var query = $@"INSERT INTO {TableNames.staff} 
        (name, date_of_birth, gender, mobile,shift) 
        VALUES (@Name, @DateOfBirth, @Gender, @Mobile,@Shift) 
        RETURNING *";
        using (var con = NewConnection)
            return await con.QuerySingleAsync<Staff>(query, Item);

    }

    public async Task<bool> Delete(int Id)
    {
        var query = $@"DELETE FROM {TableNames.staff} WHERE id = @Id";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { Id }) > 0;

    }

    public async Task<Staff> GetById(int Id)
    {
         var query = $@"SELECT * FROM {TableNames.staff} WHERE staff_id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Staff>(query, new { Id });

    }

    public async Task<List<Staff>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.staff}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Staff>(query)).AsList();

    }

    public async Task<List<Staff>> GetStaffRoomById(int Id)
    {
        var query = $@"SELECT s.* FROM {TableNames.staff} s 
        LEFT JOIN {TableNames.room} r ON r.staff_id = s.staff_id 
        WHERE r.staff_id = @Id";
        using (var con = NewConnection)
        
            return (await con.QueryAsync<Staff>(query, new { Id })).AsList();
    }

    public async Task<bool> Update(Staff Item)
    {
         var query = $@"UPDATE {TableNames.staff} 
        SET name = @Name, mobile = @Mobile, 
        date_of_birth = @DateOfBirth ,gender = @Gender,shift=@Shift WHERE staff_id = @StaffId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }
}