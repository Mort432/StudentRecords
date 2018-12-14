using Oracle.ManagedDataAccess.Client;
using StudentRecordsModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordsRepositories.Repos.Oracle
{
    public abstract class OracleRepo<T> : IRepo<T> where T : Identifiable
    {
        //Connection string
        public string ConnectionString { get; } = "User Id=s1509508;Password=s1509508!;Data Source=apollo01.glos.ac.uk:1521/orcl";

        //Tables
        public string Assignments { get; } = "ASSIGNMENTS";
        public string Courses { get; } = "COURSES";
        public string Enrollments { get; } = "ENROLLMENTS";
        public string Modules { get; } = "MODULES";
        public string Results { get; } = "RESULTS";
        public string ModuleRuns { get; } = "MODULERUNS";
        public string Users { get; } = "USERS";

        //Functionality strings
        public abstract string SelectCommandText { get; }
        public abstract string InsertCommandText { get; }
        public abstract string UpdateCommandText { get; }
        public abstract string Table { get; }

        //Abstract functionality
        public abstract T ToModel(DbDataReader reader);
        public abstract OracleParameter[] ToOracleParameters(T item);

        public async Task<IEnumerable<T>> SelectAll()
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = SelectCommandText;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return await ToEnumerable(reader);
                    }
                }
            }
        }

        public async Task<IEnumerable<T>> Select(Expression<Func<T, bool>> predicate)
        {
            var items = await SelectAll();
            return items.Where(predicate.Compile());
        }

        public async void Delete(T item)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $"DELETE FROM {Table} WHERE ID = :id";
                    command.Parameters.Add(new OracleParameter("id", item.Id));

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async void Insert(T item)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $"{InsertCommandText} RETURNING ID INTO :id";
                    command.Parameters.AddRange(ToOracleParameters(item));
                    command.Parameters.Add(new OracleParameter("id", OracleDbType.Int32));

                    await command.ExecuteNonQueryAsync();

                    item.Id = command.Parameters["id"].Value;
                }
            }
        }

        public async Task<T> SelectById(object id)
        {
            return (await SelectAll()).Where(x => x.Id.Equals(id)).ToList().FirstOrDefault();
        }

        public virtual async void Update(T item)
        {
            using (var connection = new OracleConnection(ConnectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.BindByName = true;
                    command.CommandText = $"{UpdateCommandText} WHERE ID = :id";
                    command.Parameters.AddRange(ToOracleParameters(item));
                    command.Parameters.Add(new OracleParameter("id", item.Id));

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public virtual async Task<IEnumerable<T>> ToEnumerable(DbDataReader reader)
        {
            var items = new List<T>();

            while (await reader.ReadAsync())
            {
                items.Add(ToModel(reader));
            }

            return items;
        }
    }
}
