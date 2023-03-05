using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace AdoNet
{
    internal class StudentsVisitationService
    {
        public Visitation[] GetVisitations()
        {
            var connectionString = "Data Source=myapp.db";
            var sql = "select * from Visitation";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            var result = new List<Visitation>();
            foreach (IDataRecord row in reader)
            {
                var visit = new Visitation
                (
                    Id:     row.GetInt64(row.GetOrdinal("Id")),
                    Name:   row.GetString(row.GetOrdinal("Name")),
                    Date:   DateOnly.Parse(row.GetString(row.GetOrdinal("Date")))
                );
                result.Add(visit);
            }
            return result.ToArray();
        }
        public void CreateTable()
        {
            var connectionString = "Data Source=myapp.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "CREATE TABLE Visitations (" +
                "Id     INTEGER PRIMARY KEY NOT NULL," +
                "Name   TEXT NOT NULL," +
                "Date   DATE NOT NULL" +
                ")";
            command.ExecuteNonQuery();
        }

        public void AddVisit(int id, string name, DateOnly date)
        {
            var connectionString = "Data Source=myapp.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO Visitations(Id,Name,Date) VALUE('{id}','{name}','{date}')";
            command.ExecuteNonQuery();
        }
    }
}