using DotNetEnv;
using HighScoreAPI.Entities;
using Npgsql;

namespace HighScoreAPI.Services
{
    public class ConnectionService
    {
        private string? _connectionString;
        private NpgsqlConnection? _connection;

        public ConnectionService()
        {
            Env.Load();

            var host = Environment.GetEnvironmentVariable("PGHOST");
            var database = Environment.GetEnvironmentVariable("PGDATABASE");
            var username = Environment.GetEnvironmentVariable("PGUSER");
            var password = Environment.GetEnvironmentVariable("PGPASSWORD");

            _connectionString = $"Host={host};Database={database};Username={username};Password={password}";
        }

        public void OpenConnection()
        {
            if (_connection == null || _connection.State == System.Data.ConnectionState.Closed)
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public List<ScoreEntity> GetAllScores()
        {
            List<ScoreEntity> scores = [];
            try
            {
                OpenConnection();

                string query = "SELECT * FROM score_games;";
                var cmd = new NpgsqlCommand(query, _connection);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    scores.Add(new ScoreEntity
                    {
                        Id = reader.GetInt32(0),
                        GameCode = reader.GetString(1),
                        PlayerName = reader.GetString(2),
                        PlayerScore = reader.GetInt32(3),
                        CreateTime = reader.GetDateTime(4)
                    });
                }
            }
            catch
            { }
            finally
            {
                CloseConnection();
            }

            return scores;
        }
    }
}
