namespace GoodPractices.DatabaseLayer.AdoDotNet
{
    using System;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;

    public class DALBase
    {
        public static int CommandTimeout { get; set; }

        public static object GetValue(object value)
        {
            if ((value == null))
            {
                return DBNull.Value;
            }

            return value;
        }

        public static DataSet GetDataSet(SqlConnection connection, string spName, Action<SqlCommand> addParamFunc)
        {
            return Exec(connection, spName, addParamFunc, CreateDataSet);
        }

        public static DataSet CreateDataSet(SqlCommand cmd)
        {
            var ds = new DataSet();
            var adapter = new SqlDataAdapter
            {
                SelectCommand = cmd
            };

            adapter.Fill(ds);

            return ds;
        }

        public static int ExecNonQuery(SqlConnection connection, string spName, Action<SqlCommand> addParamFunc)
        {
            return Exec(connection, spName, addParamFunc, (cmd) => cmd.ExecuteNonQuery());
        }

        public static T Exec<T>(SqlConnection connection, string spName, Action<SqlCommand> addParamFunc, Func<SqlCommand, T> executeFunc)
        {
            var result = default(T);

            SqlCommand cmd = null;
            try
            {
                if ((connection == null))
                {
                    throw new ArgumentException("The connection object cannot be null");
                }

                if ((connection.State != ConnectionState.Closed))
                {
                    throw new ArgumentException("The connection must be closed when calling this method.");
                }

                connection.Open();

                cmd = new SqlCommand(spName, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = CommandTimeout
                };

                addParamFunc?.Invoke(cmd);

                if (executeFunc != null)
                    result = executeFunc(cmd);
            }
            catch (Exception ex)
            {
                var builder = new StringBuilder();
                if (cmd != null)
                {
                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        builder.AppendLine($"Parameter Name: {param.ParameterName}, Value: {param.Value}, DbType: {param.DbType}");
                    }
                }
                var errData = new
                {
                    Desc = "DB Error",
                    StoredProcedure = spName,
                    Parameters = builder.ToString()
                };

                // TODO: Log error

                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            return result;
        }

        public static T ExecReader<T>(SqlConnection connection, string spName, Action<SqlCommand> addParamFunc, Func<SqlDataReader, T> executeFunc)
        {
            var result = default(T);

            SqlCommand cmd = null;
            try
            {
                if ((connection == null))
                {
                    throw new ArgumentException("The connection object cannot be null");
                }

                if ((connection.State != ConnectionState.Closed))
                {
                    throw new ArgumentException("The connection must be closed when calling this method.");
                }

                connection.Open();

                cmd = new SqlCommand(spName, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = CommandTimeout
                };

                addParamFunc?.Invoke(cmd);

                if (executeFunc != null)
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        result = executeFunc(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                var builder = new StringBuilder();
                if (cmd != null)
                {
                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        builder.AppendLine($"Parameter Name: {param.ParameterName}, Value: {param.Value}, DbType: {param.DbType}");
                    }
                }

                // TODO: Log error

                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();

                if (connection != null && connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            return result;
        }
    }

    public class HowToUseDalBase
    {
        public void ExecuteAll()
        {
            using (var conn = new SqlConnection("Data Source=.;Initial Catalog=AdventureWorks2016CTP3;Integrated Security=SSPI"))
            {
                ExecuteScalar(conn);
                GetReturnValueParam(conn);
                GetOutputParams(conn);
                GetDataSet(conn);
                GetDataSetWithReturnParam(conn);
            }
        }

        // return only first record from first column
        public static void ExecuteScalar(SqlConnection conn)
        {
            var asd = DALBase.Exec(
               conn,
               "uspGetOrderTrackingBySalesOrderID",
               cmd =>
               {
                   cmd.Parameters.Add("@SalesOrderID", SqlDbType.Int).Value = 57812;
               },
               cmd =>
               {
                   var test = cmd.ExecuteScalar();

                   return test;
               });
        }

        // this take return value
        public static void GetReturnValueParam(SqlConnection conn)
        {
            var returnValue = DALBase.Exec(
                   conn,
                   "uspGetOrderTrackingBySalesOrderID",
                   cmd =>
                   {
                       cmd.Parameters.Add("@SalesOrderID", SqlDbType.Int).Value = 57812;
                       cmd.Parameters.Add("@RetValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                   },
                   cmd =>
                   {
                       var test = cmd.ExecuteScalar(); // first column - first row record
                       var outputParam = cmd.Parameters["@RetValue"].Value; // return value (the name should be the same as added param but not as the same as sql procedure

                       return new
                       {
                           outputParam = outputParam,
                           test = 1
                       };
                   });
        }

        public static void GetOutputParams(SqlConnection conn)
        {
            DALBase.Exec(
                  conn,
                  "Testoutputparam",
                  cmd =>
                  {
                      cmd.Parameters.Add("@SalesOrderID", SqlDbType.Int).Value = 57812;
                      cmd.Parameters.Add("@OutputParam", SqlDbType.Int).Direction = ParameterDirection.Output; // required to be as the same as in procedure param
                      cmd.Parameters.Add("@RetValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                  },
                  cmd =>
                  {
                      var test = cmd.ExecuteScalar(); // or ExecuteNonQuery - should execute procedure to be generated outputparams
                      var outputParam = cmd.Parameters["@OutputParam"].Value; // name should be exacltly as the same as in procedure name
                      var returnValue = cmd.Parameters["@RetValue"].Value; // return value (the name should be the same as added param but not as the same as sql procedure

                      return new
                      {
                          outputParam = outputParam,
                          returnValue = returnValue
                      };
                  });
        }

        public static void GetDataSet(SqlConnection conn)
        {
            var dataset = DALBase.GetDataSet(
                  conn,
                  "uspGetOrderTrackingBySalesOrderID",
                  cmd =>
                  {
                      cmd.Parameters.Add("@SalesOrderID", SqlDbType.Int).Value = 578121; // for records use 57812
                      cmd.Parameters.Add("@RetValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                  });
            var table = dataset.Tables[0]; // always has table collection if no records returned
        }

        public static void GetDataSetWithReturnParam(SqlConnection conn)
        {
            var dataset = DALBase.Exec(
                  conn,
                  "uspGetOrderTrackingBySalesOrderID",
                  cmd =>
                  {
                      cmd.Parameters.Add("@SalesOrderID", SqlDbType.Int).Value = 578121; // for records use 57812
                      cmd.Parameters.Add("@RetValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                  },
                  cmd =>
                  {
                      var ds = DALBase.CreateDataSet(cmd);
                      var returnValue = cmd.Parameters["@RetValue"].Value;

                      return ds;
                  });
            var table = dataset.Tables[0]; // always has table collection if no records returned
        }
    }
}
