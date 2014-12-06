using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Library.Catalog.Model;

namespace Library.Catalog.Repository
{
    public class MagazineRepository : IRepository<Magazine>
    {
        readonly string _connStr = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;

        public List<Magazine> GetItemsList()
        {
            var magazines = new List<Magazine>();
            using (var cn = new SqlConnection()) {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("GetMagazines", cn)) {
                        cn.Open();
                        using (var dr = cmd.ExecuteReader()) {
                            while (dr.Read()) {
                                magazines.Add(new Magazine {
                                    Id = int.Parse(dr["LibraryItemID"].ToString()),
                                    Name = dr["Name"].ToString(),
                                    CreationYear = (DateTime)dr["CreationDate"],
                                    NumberOfIssue = int.Parse(dr["IssueNumber"].ToString()),
                                });
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    cn.Close();
                }
            }
            return magazines;
        }

        /*
        public Magazine GetItem(int id)
        {
            var magazine = new Magazine();
            using (var cn = new SqlConnection()) {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("GetMagazine", cn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var param = new SqlParameter {
                            ParameterName = "@LibraryItemID",
                            SqlDbType = SqlDbType.Int,
                            Value = id,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        cn.Open();
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read()) {
                                magazine.Id = int.Parse(dr["LibraryItemID"].ToString());
                                magazine.Name = dr["Name"].ToString();
                                magazine.CreationYear = (DateTime)dr["CreationDate"];
                                magazine.NumberOfIssue = int.Parse(dr["IssueNumber"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    cn.Close();
                }
            }
            return magazine;
        }
        */

        public void Create(ref Magazine item)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("InsertMagazine", cn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var param = new SqlParameter {
                            ParameterName = "@Name",
                            SqlDbType = SqlDbType.NVarChar,
                            Value = item.Name,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        param = new SqlParameter {
                            ParameterName = "@CreationDate",
                            SqlDbType = SqlDbType.Date,
                            Value = item.CreationYear,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        param = new SqlParameter {
                            ParameterName = "@IssueNumber",
                            SqlDbType = SqlDbType.Int,
                            Value = item.NumberOfIssue,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        cn.Open();
                        using (var dr = cmd.ExecuteReader()) {
                            while (dr.Read()) {
                                item.Id = int.Parse(dr["LibraryItemID"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    cn.Close();
                }
            }
        }

        public void Update(Magazine item)
        {
            using (var cn = new SqlConnection()) {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("UpdateMagazine", cn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var param = new SqlParameter {
                            ParameterName = "@LibraryItemID",
                            SqlDbType = SqlDbType.Int,
                            Value = item.Id,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        param = new SqlParameter {
                            ParameterName = "@Name",
                            SqlDbType = SqlDbType.NVarChar,
                            Value = item.Name,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        param = new SqlParameter {
                            ParameterName = "@CreationDate",
                            SqlDbType = SqlDbType.Date,
                            Value = item.CreationYear,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        param = new SqlParameter
                        {
                            ParameterName = "@IssueNumber",
                            SqlDbType = SqlDbType.Int,
                            Value = item.NumberOfIssue,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    cn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (var cn = new SqlConnection()) {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("DeleteMagazine", cn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var param = new SqlParameter {
                            ParameterName = "@LibraryItemID",
                            SqlDbType = SqlDbType.Int,
                            Value = id,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
                finally {
                    cn.Close();
                }
            }
        }
    }
}
