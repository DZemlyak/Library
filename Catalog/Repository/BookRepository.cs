using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Library.Catalog.Model;

namespace Library.Catalog.Repository
{
    public class BookRepository : IRepository<Book>
    {
        readonly string _connStr = ConfigurationManager.ConnectionStrings["LibraryConnectionString"].ConnectionString;

        public List<Book> GetItemsList()
        {
            var books = new List<Book>();
            using (var cn = new SqlConnection()) {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("GetBooks", cn)) {
                        cn.Open();
                        using (var dr = cmd.ExecuteReader()) {
                            while (dr.Read()) {
                                books.Add(new Book
                                {
                                    Id = int.Parse(dr["LibraryItemID"].ToString()),
                                    Name = dr["Name"].ToString(),
                                    CreationYear = (DateTime) dr["CreationDate"],
                                    Author = dr["Author"].ToString(),
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
            return books;
        }

        /* 
        public Book GetItem(int id)
        {
            var book = new Book();
            using (var cn = new SqlConnection()) {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("GetBook", cn)) {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var param = new SqlParameter {
                            ParameterName = "@LibraryItemID",
                            SqlDbType = SqlDbType.Int,
                            Value = id,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        cn.Open();
                        using (var dr = cmd.ExecuteReader()) {
                            while (dr.Read())                             {
                                book.Id = int.Parse(dr["LibraryItemID"].ToString());
                                book.Name = dr["Name"].ToString();
                                book.CreationYear = (DateTime) dr["CreationDate"];
                                book.Author = dr["Author"].ToString();
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
            return book;
        }
        */

        public void Create(ref Book item)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("InsertBook", cn)) {
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
                            ParameterName = "@Author",
                            SqlDbType = SqlDbType.NVarChar,
                            Value = item.Author,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        cn.Open();
                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
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

        public void Update(Book item)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("UpdateBook", cn)) {
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
                        param = new SqlParameter {
                            ParameterName = "@Author",
                            SqlDbType = SqlDbType.NVarChar,
                            Value = item.Author,
                            Direction = ParameterDirection.Input
                        };
                        cmd.Parameters.Add(param);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (var cn = new SqlConnection()) {
                cn.ConnectionString = _connStr;
                try {
                    using (var cmd = new SqlCommand("DeleteBook", cn))  {
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
