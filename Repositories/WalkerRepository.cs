using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
	public class WalkerRepository : IWalkerRepository
	{
		private readonly IConfiguration _config;

		// The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
		public WalkerRepository(IConfiguration config)
		{
			_config = config;
		}

		public SqlConnection Connection
		{
			get
			{
				return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			}
		}

		public List<Walker> GetAllWalkers()
		{
			using (SqlConnection conn = Connection)
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
                        SELECT w.Id, w.[Name], w.ImageUrl, w.NeighborhoodId, n.[Name] AS Hoodname
                        FROM Walker AS w LEFT JOIN Neighborhood AS n ON w.NeighborhoodId = n.Id;
                    ";

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						List<Walker> walkers = new List<Walker>();
						while (reader.Read())
						{
							Walker walker = new Walker
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Name = reader.GetString(reader.GetOrdinal("Name")),
								ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
								NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
								Neighborhood = new Neighborhood()
								{
									Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
									Name = reader.GetString(reader.GetOrdinal("HoodName"))
								},
							};

							walkers.Add(walker);
						}

						return walkers;
					}
				}
			}
		}

		public Walker GetWalkerById(int id)
		{
			using (SqlConnection conn = Connection)
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
                        SELECT w.Id, w.[Name], w.ImageUrl, w.NeighborhoodId, n.[Name] AS Hoodname
                        FROM Walker AS w LEFT JOIN Neighborhood AS n ON w.NeighborhoodId = n.Id WHERE w.Id = @id
                    ";

					cmd.Parameters.AddWithValue("@id", id);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							Walker walker = new Walker
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Name = reader.GetString(reader.GetOrdinal("Name")),
								ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
								NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
								Neighborhood = new Neighborhood()
								{
									Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
									Name = reader.GetString(reader.GetOrdinal("HoodName"))
								},
							};

							return walker;
						}
						else
						{
							return null;
						}
					}
				}
			}
		}

		public List<Walker> GetWalkersInNeighborhood(int neighborhoodId)
		{
			using (SqlConnection conn = Connection)
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
                SELECT Id, [Name], ImageUrl, NeighborhoodId
                FROM Walker
                WHERE NeighborhoodId = @neighborhoodId
            ";

					cmd.Parameters.AddWithValue("@neighborhoodId", neighborhoodId);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						List<Walker> walkers = new List<Walker>();
						while (reader.Read())
						{
							Walker walker = new Walker
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Name = reader.GetString(reader.GetOrdinal("Name")),
								ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
								NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
							};

							walkers.Add(walker);
						}

						return walkers;
					}
				}
			}
		}

		public List<Walk> GetWalksByWalkerId(int id)
		{
			using (SqlConnection conn = Connection)
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SELECT Walks.Id, Walks.Date, Walks.Duration, Walks.WalkerId, Dog.Name AS DogName, Dog.Breed, Owner.Name AS OwnerName FROM Walks LEFT JOIN Dog ON Walks.DogId = Dog.Id LEFT JOIN Owner ON Owner.Id = Dog.OwnerId WHERE WalkerId = @id";
					cmd.Parameters.AddWithValue("@id", id);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						List<Walk> walks = new List<Walk>();
						while (reader.Read())
						{
							Walk walk = new Walk()
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Date = reader.GetDateTime(reader.GetOrdinal("Date")),
								Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
								WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
								Owner = new Owner()
								{
									Name = reader.GetString(reader.GetOrdinal("OwnerName"))
								},
								Dog = new Dog()
								{
									DogName = reader.GetString(reader.GetOrdinal("DogName")),
									Breed = reader.GetString(reader.GetOrdinal("Breed"))
								}
							};
							walks.Add(walk);
						}
						return walks;
					}
				}
			}
		}
	}
}