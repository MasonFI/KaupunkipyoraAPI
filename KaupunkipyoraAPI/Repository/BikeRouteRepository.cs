using AutoMapper;
using Dapper;
using KaupunkipyoraAPI.Context;
using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Models.DTO;
using KaupunkipyoraAPI.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace KaupunkipyoraAPI.Repository
{
    public class BikeRouteRepository : IBikeRouteRepository
    {
        private readonly DapperContext _context;
        private readonly IMapper _mapper;

        private static string Table => "Route";
        private static string PrimaryKey => "Id";
        private static List<string> Columns => new()
        {
            PrimaryKey,
            "DepartureTime",
            "ReturnTime",
            "DepartureStationId",
            "DepartureStationName",
            "ReturnStationId",
            "ReturnStationName",
            "CoveredDistanceInMeters",
            "DurationInSeconds",
            "Created",
            "CreatedById",
            "Updated",
            "UpdatedById"
        };

        public BikeRouteRepository(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BikeRoute?> GetByIdAsync(int id)
        {
            var query = $"SELECT {String.Join(",", Columns)} FROM {Table} WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            var route = await connection.QuerySingleOrDefaultAsync<BikeRoute?>(query, new { id });
            return route;
        }

        public async Task<IEnumerable<BikeRoute>> GetAllAsync()
        {
            var query = $"SELECT {String.Join(",", Columns)} FROM {Table}";
            using var connection = _context.CreateConnection();
            var routes = await connection.QueryAsync<BikeRoute>(query);
            return routes.ToList();
        }

        public async Task<BikeRoute> AddAsync(BikeRoute entity)
        {
            var query = @$"INSERT INTO {Table}
                (DepartureTime,
                ReturnTime, 
                DepartureStationId,
                DepartureStationName,
                ReturnStationId,
                ReturnStationName,
                CoveredDistanceInMeters,
                DurationInSeconds,
                Created,
                CreatedById) 
        
                VALUES
                (@DepartureTime,
                @ReturnTime, 
                @DepartureStationId,
                @DepartureStationName,
                @ReturnStationId,
                @ReturnStationName,
                @CoveredDistanceInMeters,
                @DurationInSeconds,
                @Created,
                @CreatedById);

                SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("DepartureTime", entity.DepartureTime, DbType.DateTime);
            parameters.Add("ReturnTime", entity.DepartureTime, DbType.DateTime);
            parameters.Add("DepartureStationId", entity.DepartureStationId, DbType.Int32);
            parameters.Add("DepartureStationName", entity.DepartureStationName, DbType.String);
            parameters.Add("ReturnStationId", entity.ReturnStationId, DbType.Int32);
            parameters.Add("ReturnStationName", entity.ReturnStationName, DbType.String);
            parameters.Add("CoveredDistanceInMeters", entity.CoveredDistanceInMeters, DbType.Int32);
            parameters.Add("DurationInSeconds", entity.DurationInSeconds, DbType.Int32);
            parameters.Add("Created", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedById", entity.CreatedById, DbType.Int32);

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(query, parameters);
            
            var createdBikeRoute = _mapper.Map<BikeRoute>(entity);
            createdBikeRoute.Id = id;
            
            return createdBikeRoute;
        }

        public async Task<BikeRoute> UpdateAsync(BikeRoute entity)
        {
            var query = @$"UPDATE {Table} SET
                DepartureTime = @DepartureTime,
                ReturnTime = @ReturnTime, 
                DepartureStationId = @DepartureStationId,
                DepartureStationName = @DepartureStationName,
                ReturnStationId = @ReturnStationId,
                ReturnStationName = @ReturnStationName,
                CoveredDistanceInMeters = @CoveredDistanceInMeters,
                DurationInSeconds = @DurationInSeconds,
                Created = @Created,
                CreatedById = @CreatedById
                
                WHERE Id = @Id;

                SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("DepartureTime", entity.DepartureTime, DbType.DateTime);
            parameters.Add("ReturnTime", entity.DepartureTime, DbType.DateTime);
            parameters.Add("DepartureStationId", entity.DepartureStationId, DbType.Int32);
            parameters.Add("DepartureStationName", entity.DepartureStationName, DbType.String);
            parameters.Add("ReturnStationId", entity.ReturnStationId, DbType.Int32);
            parameters.Add("ReturnStationName", entity.ReturnStationName, DbType.String);
            parameters.Add("CoveredDistanceInMeters", entity.CoveredDistanceInMeters, DbType.Int32);
            parameters.Add("DurationInSeconds", entity.DurationInSeconds, DbType.Int32);
            parameters.Add("Created", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedById", entity.CreatedById, DbType.Int32);
            parameters.Add("Id", entity.Id, DbType.Int32);

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(query, parameters);

            var createdBikeRoute = _mapper.Map<BikeRoute>(entity);
            createdBikeRoute.Id = id;

            return createdBikeRoute;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = @$"DELETE FROM {Table} WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { id });
        }
    }
}
