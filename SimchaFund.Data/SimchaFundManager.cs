using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimchaFund.Data
{
    public class SimchaFundManager
    {
        private string _connectionString;
        public SimchaFundManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<Simchas> GetSimchas(int? id)
        {
            List<Simchas> simchas = new List<Simchas>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Simcha";
                if (id != null)
                {
                    command.CommandText += " WHERE Id = @id";
                    command.Parameters.AddWithValue("@id", id);
                }
                command.CommandText += " ORDER BY Id DESC";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    simchas.Add(new Simchas
                     {
                         Id = (int)reader["id"],
                         Name = (string)reader["Name"],
                         Date = (DateTime)reader["Date"],
                     });
                }
            }
            return simchas;
        }
        public Simchas GetASimchas(int? id)
        {
            Simchas simchas = new Simchas();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Simcha";
                if (id != null)
                {
                    command.CommandText += " WHERE Id = @id";
                    command.Parameters.AddWithValue("@id", id);
                }
                command.CommandText += " ORDER BY Id DESC";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    simchas.Id = (int)reader["id"];
                    simchas.Name = (string)reader["Name"];
                    simchas.Date = (DateTime)reader["Date"];

                }
            }
            return simchas;
        }
        public IEnumerable<Contributors> GetContributors(int? id)
        {
            List<Contributors> contributors = new List<Contributors>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Contributor";
                if (id != null)
                {
                    command.CommandText += " WHERE Id = @id";
                    command.Parameters.AddWithValue("@id", id);
                }
                command.CommandText += " ORDER BY Id DESC";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contributors.Add(new Contributors
                    {
                        Id = (int)reader["id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        PhoneNum = (string)reader["PhoneNum"],
                        AlwaysInclude = (bool)reader["AlwaysInclude"],
                    });
                }
            }
            return contributors;
        }

        public IEnumerable<SimchaAndContributors> GetSimchaAndContributors(int? contributorId, int? simchaId)
        {
            List<SimchaAndContributors> simchaAndContributors = new List<SimchaAndContributors>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM SimchaAndContributors";
                if (contributorId != null)
                {
                    command.CommandText += " WHERE ContributorId = @contributorId";
                    command.Parameters.AddWithValue("@contributorId", contributorId);
                }
                if (simchaId != null && contributorId != null)
                {
                    command.CommandText += " AND SimchaId = @sid";
                    command.Parameters.AddWithValue("@sid", simchaId);
                }
                else if(simchaId != null && contributorId == null)
                {
                    command.CommandText += " WHERE SimchaId = @sid";
                    command.Parameters.AddWithValue("@sid", simchaId);
                }

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    simchaAndContributors.Add(new SimchaAndContributors
                    {
                        SimchaId = (int)reader["SimchaId"],
                        ContributorId = (int)reader["ContributorId"],
                        Contribution = (decimal)reader["Contribution"],
                        Date = (DateTime)reader["Date"],
                    });
                }
            }
            return simchaAndContributors;
        }
        public IEnumerable<SimchaAndContributors> GetAllSimchasAndContributor()
        {
            List<SimchaAndContributors> simchaAndContributors = new List<SimchaAndContributors>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM SimchaAndContributors";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    simchaAndContributors.Add(new SimchaAndContributors
                    {
                        SimchaId = (int)reader["SimchaId"],
                        ContributorId = (int)reader["ContributorId"],
                        Contribution = (decimal)reader["Contribution"],
                        Date = (DateTime)reader["Date"],
                    });
                }
            }
            return simchaAndContributors;
        }
        public IEnumerable<Deposits> GetDeposits(int ContributorId)
        {
            List<Deposits> deposits = new List<Deposits>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Deposits WHERE ContributorId = @contributorId";
                command.Parameters.AddWithValue("@contributorId", ContributorId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    deposits.Add(new Deposits
                    {
                        Id = (int)reader["id"],
                        ContributorId = (int)reader["ContributorId"],
                        Amount = (decimal)reader["Amount"],
                        Date = (DateTime)reader["Date"],
                    });
                }
            }
            return deposits;
        }
        public int AddSimcha(Simchas simcha)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Simcha (Name, Date)VALUES (@name, @date) SELECT @@IDENTITY";
                command.Parameters.AddWithValue("@name", simcha.Name);
                command.Parameters.AddWithValue("@date", simcha.Date);
                connection.Open();
                return (int)(decimal)command.ExecuteScalar();
            }
        }
        public int AddContributor(Contributors contibutor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO Contributor (FirstName, LastName, PhoneNum, AlwaysInclude)
                                    VALUES (@firstName, @lastName, @phoneNum, @alwaysInclude) SELECT @@IDENTITY";
                command.Parameters.AddWithValue("@firstName", contibutor.FirstName);
                command.Parameters.AddWithValue("@lastName", contibutor.LastName);
                command.Parameters.AddWithValue("@phoneNum", contibutor.PhoneNum);
                command.Parameters.AddWithValue("@alwaysInclude", contibutor.AlwaysInclude);
                connection.Open();
                return (int)(decimal)command.ExecuteScalar();

            }
        }
        public int AddDeposit(int countributorId, double amount)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"INSERT INTO Deposits (ContributorId, Amount, Date)
                                    VALUES (@contributorId, @amount, @date) SELECT @@IDENTITY";
                command.Parameters.AddWithValue("@contributorId", countributorId);
                command.Parameters.AddWithValue("@amount", amount);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                connection.Open();
                return (int)(decimal)command.ExecuteScalar();

            }
        }
        public decimal GetTotalContributionsForSimcha(int simchaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ISNULL(SUM(contribution),0) FROM SimchaAndContributors WHERE SimchaId = @simchaId";
                command.Parameters.AddWithValue("@simchaId", simchaId);
                connection.Open();
                return (decimal)command.ExecuteScalar();

            }
        }
        public int GetTotalcontributors()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Count(*) FROM Contributor ";
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
        public int GetTotalPeopleContributed(int simchaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Count(*) FROM SimchaAndContributors WHERE SimchaId = @simchaId";
                command.Parameters.AddWithValue("@simchaId", simchaId);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }
        public decimal GetContributionAmount(int simchaId, int contributionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT ISNULL(SUM(sc.Contribution),0)FROM SimchaAndContributors sc WHERE sc.ContributorId = @contributorId and sc.SimchaId = @simchaId";
                command.Parameters.AddWithValue("@simchaId", simchaId);
                command.Parameters.AddWithValue("@contributorId", contributionId);
                connection.Open();
                return (decimal)command.ExecuteScalar();
            }
        }
        public decimal GetBalance(int contributorId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ISNULL(SUM(amount),0) FROM Deposits WHERE ContributorId = @contributorId";
                command.Parameters.AddWithValue("@contributorId", contributorId);
                connection.Open();
                decimal deposits = (decimal)command.ExecuteScalar();
                command.CommandText = "SELECT ISNULL(SUM(sc.contribution),0) FROM SimchaAndContributors sc WHERE sc.contributorId = @contributorId";
                decimal contributions = (decimal)command.ExecuteScalar();
                return deposits - contributions;
            }
        }
        public decimal GetTotalBalance()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ISNULL(SUM(amount),0) FROM Deposits";
                connection.Open();
                decimal deposits = (decimal)command.ExecuteScalar();
                command.CommandText = "SELECT ISNULL(SUM(sc.contribution),0) FROM SimchaAndContributors sc";
                decimal contributions = (decimal)command.ExecuteScalar();
                return deposits - contributions;
            }
        }
        public void UpdateContributor(Contributors contribtor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "update Contributor set FirstName = @firstName, LastName = @lastName, PhoneNum = @phoneNum, AlwaysInclude = @alwaysInclude  where id = @id";
                command.Parameters.AddWithValue("@firstName", contribtor.FirstName);
                command.Parameters.AddWithValue("@lastName", contribtor.LastName);
                command.Parameters.AddWithValue("@phoneNum", contribtor.PhoneNum);
                command.Parameters.AddWithValue("@alwaysInclude", contribtor.AlwaysInclude);
                command.Parameters.AddWithValue("@id", contribtor.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void AddContribution(int contributorId, double contribution, int simchaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO SimchaAndContributors VALUES(@sid ,@contributorId, @contribution, @date)";
                command.Parameters.AddWithValue("@contribution", contribution);
                command.Parameters.AddWithValue("@contributorId", contributorId);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@sid", simchaId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateContribution(int contributorId, double contribution, int simchaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE SimchaAndContributors SET Contribution = @contribution, Date = @date  where ContributorId = @contributorId AND SimchaId = @sid";
                command.Parameters.AddWithValue("@contribution", contribution);
                command.Parameters.AddWithValue("@contributorId", contributorId);
                command.Parameters.AddWithValue("@date", DateTime.Now);
                command.Parameters.AddWithValue("@sid", simchaId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteContribution(int contributorId, int simchaId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM SimchaAndContributors where ContributorId = @contributorId AND SimchaId = @sid";
                command.Parameters.AddWithValue("@contributorId", contributorId);
                command.Parameters.AddWithValue("@sid", simchaId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
