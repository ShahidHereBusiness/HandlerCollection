using Oracle.DataAccess.Client;
using System.Data;
using System;

namespace SOAV
{
    /// <summary>
    /// Solution Developer:
    /// Entity Framework based Oracle PL\SQL Connectivity
    /// </summary>
    public class EFOraclePLSQL
    {
        //string[,] paramDynamic = new string[,]
        //       {        
        //            {"p_city","p_date","p_amount","p_flag","p_cursor"},// Name @ paramDynamic[0,j]
        //            {"126","126","113","112","121"},// DataType 134:Boolean, 126:Varchar, 111:Int16, 112:Int32, 113:Int64 @ paramDynamic[1,j]
        //            {"4000","8","19","1","0"},// Size @ paramDynamic[2,j]
        //            {"Islamabad","20231229","1000000","0","0"},// Value @ paramDynamic[3,j]
        //            {"6","1","3","2","2"}// Direction 1:Input, 2:Output, 6:ReturnValue, 3: InputOutput @ paramDynamic[4,j]
        //       };
        /// <summary>
        /// Solution Developers:
        /// Method to Connect Oracle Database Dynamically.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="paramDynamic">Parameters Collection</param>
        /// <param name="dataTable">Database Table Content</param>
        /// <param name="executeType">Command Execute: ES, EQ, ER</param>        
        /// <returns>2D String Collection</returns>
        public string[,] ConnectedToFetch(
            ref OracleCommand cmd, string[,] paramDynamic,
            out DataTable dataTable, string executeType = "EQ"
            )
        {
            string[,] resultant = null;// Return 2D Array
            string msg = string.Empty;// Return Exception Message
            OracleParameter addParam;// New Oracle Parameter Instance
            int OracleDbTypeSize = 8;// Integer Tryparse and OracleParameter Size
            int OracleDbTypeEnum = 126;// Integer Tryparse and OracleParameter OracleDbType
            int directionEnum = 1;// Integer Tryparse and OracleParameter Direction
            dataTable = null;// DataTable Initialize
            int resultantSize = 0;// Resultant Array Size
            bool paramCursor = false;
            try
            {
                if (paramDynamic != null && (paramDynamic.Length / 5) > 0)
                {
                    // Traverse Through Multidimension Array
                    foreach (string nullStr in paramDynamic)
                    {
                        if (nullStr == null || nullStr.Trim() == string.Empty)// Null Params Not Allowed
                        { throw new Exception("Null/Blank Inputs Not Allowed"); }
                    }
                    // Add Input Direction Parameters for paramString            
                    for (int j = 0; j < (paramDynamic.Length / 5); j++)
                    {
                        //Add New Oracle Parameter
                        addParam = new OracleParameter();

                        //#1 Validate for empty ParameterName
                        if (paramDynamic[0, j].ToString().Length <= 0)
                        { throw new Exception($"ParameterName {paramDynamic[0, j]} Empty Not Allowed"); }
                        try
                        { addParam.ParameterName = paramDynamic[0, j].ToString();/*Name: p_date*/ }
                        catch { throw new Exception($"ParameterName {paramDynamic[0, j]} Invalid"); }

                        //#2 Explicit Casting for Parameter OracleDbType
                        if (!int.TryParse(paramDynamic[1, j], out OracleDbTypeEnum))
                        { throw new Exception($"OracleDbType {paramDynamic[0, j]}:{paramDynamic[1, j]} Numeric Verify Failed"); }
                        try
                        { addParam.OracleDbType = (OracleDbType)OracleDbTypeEnum;/*OracleDbType: OracleDbType.Varchar2*/ }
                        catch { throw new Exception($"OracleDbType {paramDynamic[0, j]}:{paramDynamic[1, j]} Invalid"); }

                        //#3 Explicit Casting for Parameter Size
                        if (!int.TryParse(paramDynamic[2, j], out OracleDbTypeSize))
                        { throw new Exception($"Size {paramDynamic[0, j]}:{paramDynamic[2, j]} Numeric Verify Failed"); }
                        try
                        { addParam.Size = OracleDbTypeSize;/*Size: 8*/ }
                        catch { throw new Exception($"OracleDbTypeSize {paramDynamic[0, j]}:{paramDynamic[2, j]} Invalid"); }

                        //#4 Validate for empty Parameter Value
                        if (paramDynamic[3, j].ToString().Length <= 0)
                        { throw new Exception($"Value {paramDynamic[0, j]}:{paramDynamic[3, j]} Empty Not Allowed"); }
                        try
                        { addParam.Value = paramDynamic[3, j];/*Value: 20230419*/ }
                        catch { throw new Exception($"OracleDbTypeValue {paramDynamic[0, j]}:{paramDynamic[3, j]} Invalid"); }

                        //#5 Explicit Casting for Parameter Direction
                        if (!int.TryParse(paramDynamic[4, j], out directionEnum))
                        { throw new Exception($"Direction {paramDynamic[0, j]}:{paramDynamic[4, j]} Numeric Verify Failed"); }
                        try
                        { addParam.Direction = (ParameterDirection)directionEnum;/*Direction: ParameterDirection.Input*/ }
                        catch { throw new Exception($"OracleDbTypeDirection {paramDynamic[0, j]}:{paramDynamic[4, j]} Invalid"); }

                        cmd.Parameters.Add(addParam);/*Add*/
                        addParam = null;// Reset Object.
                        if (directionEnum != 1)// Resultant Size
                        { ++resultantSize; }
                    }
                }
                // Verify Input/Output Parameters Count
                if (cmd.Parameters.Count > 0)
                {
                    // Execute Query for Data Retrieval
                    switch (executeType)
                    {
                        case "ER":
                            cmd.ExecuteReader();// ExecuteReader
                            break;
                        case "ES":
                            cmd.ExecuteScalar();// ExecuteScalar
                            break;
                        case "EQ":
                            cmd.ExecuteNonQuery();// ExecuteNonQuery
                            break;
                        case "SM":
                            cmd.ExecuteStream();// ExecuteStream
                            break;
                        default:
                            break;
                    }
                    resultant = new string[resultantSize, resultantSize];// Make Resultant Collection                    
                    for (int i = 0; i < (paramDynamic.Length / 5); i++)
                    {
                        if (paramDynamic[4, i].ToString() != "1")
                        {
                            resultant[0, i] = paramDynamic[0, i];
                            resultant[1, i] = cmd.Parameters[paramDynamic[0, i]].Value.ToString();
                        }
                        if (paramDynamic[1, i] == "121") { paramCursor = true; }// RefCursor Required
                    }
                    // Make Reference Cursor Output into Table
                    if (paramCursor)
                    {
                        OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(cmd);
                        oracleDataAdapter.Fill(dataTable);
                    }
                }
                // Parameters Not Found
                else { throw new Exception("Parameters Required"); }
            }
            catch (Exception ex)
            {
                msg = ex.Message;// Exception Handling
            }
            finally
            {
                if (msg.Length > 0)
                {
                    // Exception Response
                    resultant = new string[,] { { "p_error" }, { msg } };
                }
            }
            // Return Resultant Collection
            return resultant;
        }
        /// <summary>
        /// Solution Developers:
        /// Reset/Close/New Oracle Connection
        /// </summary>
        /// <param name="cn">OracleConnection</param>
        /// <param name="cmd">OracleCommand</param>
        /// <param name="cmdType">System.Data.CommandType</param>
        /// <param name="connStr">Connection String data source; user id; password</param>
        /// <param name="CommandText">Reference Schema/Package/Procedure/Function/View</param>
        /// <returns></returns>
        public bool ConnectionStatus(ref OracleConnection cn, ref OracleCommand cmd, CommandType cmdType, string connStr, string CommandText)
        {
            bool flag = false;
            try
            {
                /// Close and Dispose Oracle Connection
                if (cn.State != ConnectionState.Open)
                {
                    cmd.Parameters.Clear();// Clear Parameters
                    cmd.Dispose();// Dispose OracleCommand
                    cn.Close();// Close Connection
                    cn.Dispose();// Dispose Connection
                }
                // Reinstate and Refresh Existing Connection
                if (cn.State == ConnectionState.Open)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandText;
                    cmd.Connection = cn;
                    flag = true;
                }
                else
                {
                    cn = new OracleConnection(connStr);
                    cmd = new OracleCommand();
                    cmd.Parameters.Clear();
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandText;
                    cmd.Connection = cn;
                    cn.Open();
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
    }
}