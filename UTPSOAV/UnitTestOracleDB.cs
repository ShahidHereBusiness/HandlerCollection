using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oracle.DataAccess.Client;
using SOAV;
using System;
using System.Configuration;
using System.Data;

namespace UTPSOAV
{
    [TestClass]
    public class UnitTestOracleDB
    {
        [TestMethod]
        public void TestMethodConnectedToFetch()
        {
            string response = string.Empty;// Response String | delimeted
            try
            {
                /// OracleDbType dataTypesList;// Info Enumeration Oracle Supported Database Types
                /// ParameterDirection directionList;// Info Enumeration Oracle Supported Direction Types

                EFOraclePLSQL orc = new EFOraclePLSQL();

                OracleConnection cn = new OracleConnection();// Oracle Connection
                OracleCommand cmd = new OracleCommand();// Oracle Command
                cmd.CommandType = CommandType.StoredProcedure;// Oracle Command Type
                string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();// Oracle Connection String
                string commandText = "PKG_INFO.FUNC_GET_INFO";// Oracle Function Command Text                
                // Get Tabular Content From Oracle Database
                DataTable dt;
                string[,] paramDynamic;// Oracle Parameters Dynamic Array
                string[,] rsl;// Resultant Dynamic Array
                /////////////////////////////////
                /// 1-Success Case ConnectionStatus
                /// 2-ConnectedToFetch
                /// 3-return response
                /////////////////////////////////
                if (orc.ConnectionStatus(ref cn, ref cmd, cmd.CommandType, connStr, commandText))//Get Connection Through
                {
                    paramDynamic = new string[,]
                           {
                                {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name
                                {"126","126","113","112","121"},// DataType
                                {"4000","8","19","1","0"},// Size
                                {"Islamabad","20231229","1000000","0","0"},// Value
                                {"6","1","3","2","2"}// Direction
                           };
                    // Fetch Results                    
                    rsl = orc.ConnectedToFetch(ref cmd, paramDynamic, out dt);
                    foreach (string r in rsl)
                    {
                        response += $"|{r}";
                    }
                    Console.WriteLine($"T{response}");
                }
                else { throw new Exception("Connection Couldn't be established"); }
                ////////////////////////////////////////
                /// 1-Failure Case Null
                /// 2-ConnectedToFetch
                /// 3-return response
                ////////////////////////////////////////
                if (!orc.ConnectionStatus(ref cn, ref cmd, cmd.CommandType, connStr, commandText))//Get Connection Through
                {
                    response = string.Empty;
                    paramDynamic = new string[,]
                           {
                                {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name
                                {"126","126","113","112","121"},// DataType
                                {"4000","8","19","1","0"},// Size
                                {"Islamabad","20231229","1000000","0",null},// Value
                                {"3","1","3","6","2"}// Direction
                           };
                    // Fetch Results                    
                    rsl = orc.ConnectedToFetch(ref cmd, paramDynamic, out dt);
                    foreach (string r in rsl)
                    {
                        response += $"|{r}";
                    }
                    Console.WriteLine($"T{response}");
                }
                else { throw new Exception("Connection Couldn't be established"); }
                ////////////////////////////////////////
                /// 1-Failure Case Empty
                /// 2-ConnectedToFetch
                /// 3-return response
                ////////////////////////////////////////
                if (!orc.ConnectionStatus(ref cn, ref cmd, cmd.CommandType, connStr, commandText))//Get Connection Through
                {
                    response = string.Empty;
                    paramDynamic = new string[,]
                           {
                                {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name
                                {"126","126","113","112","121"},// DataType
                                {"4000","8","19","1","0"},// Size
                                {"Islamabad","20231229","1000000","0",""},// Value
                                {"6","1","3","2","2"}// Direction
                           };
                    // Fetch Results                    
                    rsl = orc.ConnectedToFetch(ref cmd, paramDynamic, out dt);
                    foreach (string r in rsl)
                    {
                        response += $"|{r}";
                    }
                    Console.WriteLine($"T{response}");
                }
                else { throw new Exception("Connection Couldn't be established"); }
                ////////////////////////////////////////
                /// 1-Failure Case Empty Space(s)
                /// 2-ConnectedToFetch
                /// 3-return response
                ////////////////////////////////////////
                if (!orc.ConnectionStatus(ref cn, ref cmd, cmd.CommandType, connStr, commandText))//Get Connection Through
                {
                    response = string.Empty;
                    paramDynamic = new string[,]
                           {
                                {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name
                                {"126","126","113","112","121"},// DataType
                                {"4000","8","19","1","0"},// Size
                                {"Islamabad","20231229","1000000","0"," "},// Value
                                {"6","1","3","2","2"}// Direction
                           };
                    // Fetch Results                    
                    rsl = orc.ConnectedToFetch(ref cmd, paramDynamic, out dt);
                    foreach (string r in rsl)
                    {
                        response += $"|{r}";
                    }
                    Console.WriteLine($"T{response}");
                }
                else { throw new Exception("Connection Couldn't be established"); }
                ////////////////////////////////////////
                /// 1-Failure Case DataType Unknown
                /// 2-ConnectedToFetch
                /// 3-return response
                ////////////////////////////////////////
                if (!orc.ConnectionStatus(ref cn, ref cmd, cmd.CommandType, connStr, commandText))//Get Connection Through
                {
                    response = string.Empty;
                    paramDynamic = new string[,]
                           {
                                {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name
                                {"226","126","113","112","121"},// DataType
                                {"4000","8","19","1","0"},// Size
                                {"Islamabad","20231229","1000000","0","p_cursor"},// Value
                                {"6","1","3","2","2"}// Direction
                           };
                    // Fetch Results                    
                    rsl = orc.ConnectedToFetch(ref cmd, paramDynamic, out dt);
                    foreach (string r in rsl)
                    {
                        response += $"|{r}";
                    }
                    Console.WriteLine($"T{response}");
                }
                else { throw new Exception("Connection Couldn't be established"); }
                ////////////////////////////////////////
                /// 1-Failure Case Direction Unknown
                /// 2-ConnectedToFetch
                /// 3-return response
                ////////////////////////////////////////
                if (!orc.ConnectionStatus(ref cn, ref cmd, cmd.CommandType, connStr, commandText))//Get Connection Through
                {
                    response = string.Empty;
                    paramDynamic = new string[,]
                           {
                                {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name
                                {"126","126","113","112","121"},// DataType
                                {"4000","8","19","1","0"},// Size
                                {"Islamabad","20231229","1000000","0","p_cursor"},// Value
                                {"6","1","30","2","2"}// Direction
                           };
                    // Fetch Results                    
                    rsl = orc.ConnectedToFetch(ref cmd, paramDynamic, out dt);
                    foreach (string r in rsl)
                    {
                        response += $"|{r}";
                    }
                    Console.WriteLine($"T{response}");
                }
                else { throw new Exception("Connection Couldn't be established"); }
                ////////////////////////////////////////
                /// 1-Failure Case Max Size
                /// 2-ConnectedToFetch
                /// 3-return response
                ////////////////////////////////////////
                if (!orc.ConnectionStatus(ref cn, ref cmd, cmd.CommandType, connStr, commandText))//Get Connection Through
                {
                    response = string.Empty;
                    paramDynamic = new string[,]
                           {
                                {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name
                                {"126","126","113","112","121"},// DataType
                                {"4000","8","19",Double.MaxValue.ToString(),"0"},// Size
                                {"Islamabad","20231229","1000000","0","p_cursor"},// Value
                                {"6","1","3","2","2"}// Direction
                           };
                    // Fetch Results                    
                    rsl = orc.ConnectedToFetch(ref cmd, paramDynamic, out dt);
                    foreach (string r in rsl)
                    {
                        response += $"|{r}";
                    }
                    Console.WriteLine($"T{response}");
                }
                else { throw new Exception("Connection Couldn't be established"); }
            }
            catch (Exception ex)
            { if (ex.Message.Length > 0) response += $"p_error|{ex.Message}"; }
            Console.WriteLine($"Return of All Desired Method Results.");// return $"T{response}";
        }
    }
}