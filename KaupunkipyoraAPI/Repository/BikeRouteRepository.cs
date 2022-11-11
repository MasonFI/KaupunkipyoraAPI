using AutoMapper;
using Dapper;
using KaupunkipyoraAPI.Context;
using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Models.Entity;
using System.Data;

namespace KaupunkipyoraAPI.Repository
{
    public class BikeRouteRepository : BaseRepository<BikeRoute>, IBikeRouteRepository
    {
        protected override string Table => "BikeRoute";
        protected override List<string> Columns => new()
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

        public BikeRouteRepository(DapperContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<BikeRoute> AddAsync(BikeRoute entity)
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

        public override async Task<BikeRoute> UpdateAsync(BikeRoute entity)
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
    }
}
