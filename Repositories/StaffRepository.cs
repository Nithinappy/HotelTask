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

    public Task<Staff> GetById(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Staff>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.staff}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Staff>(query)).AsList();

    }

    public Task<bool> Update(Staff Item)
    {
        throw new NotImplementedException();
    }
}