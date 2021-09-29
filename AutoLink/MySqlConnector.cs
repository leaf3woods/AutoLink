using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AutoLink
{
    class MySqlConnector
    {
        public MySqlConnector()
        {

        }

        /// <summary>
        /// 存放IP地址和端口的数据库的IP端口
        /// 用户名等默认值只读属性
        /// </summary>
        const string strDefaultIP = "";
        const string strDefaultPort = "";
        const string strDefaultUid = "";
        const string strDefaultPsw = "";
        const string strDefaultDb = "";
        

        public string GetNullFlagStorageId(out string strPlcName)
        {
            strPlcName = "";
            return "";
        }

        /// <summary>
        /// 在查询到异常数据库数据后写入数据
        /// 与数据库建立连接需提供对应的参数
        /// 端口号使用参数缺省,默认为3306,以避免使用重载
        /// </summary>
        /// <param name="strDb"></param>
        /// <param name="strIp"></param>
        /// <param name="strPort"></param>
        /// <param name="strUid"></param>
        /// <param name="strPsw"></param>
        /// <param name="strCmd"></param>
        /// <returns></returns>
        public int UpdateDatabase(string strDb, string strIp, string strUid, string strPsw, string strCmd, string strPort = "3306")
        {
            string strConn = string.Format("Database={0};Server={1};Port={2};Uid={3};Password={4};", strDb, strIp, strPort, strUid, strPsw);
            MySqlConnection conn = new MySqlConnection(strConn);
            int nResult = -1;
            try
            {
                //建立连接
                conn.Open();
                //成功后发送数据库更新语句,已在连接池的连接
                MySqlCommand cmd = new MySqlCommand(strCmd, conn);
                //查询数据库是否已经有数据被更改,返回值为受影响行号
                nResult = cmd.ExecuteNonQuery();                
            }
            catch(Exception ex)
            {
                nResult = -1;
            }
            finally
            {
                //确保操作结束连接关闭
                conn.Close();
            }
            //检查数据库是否修改成功
            return nResult;
        }

        /// <summary>
        /// 数据库读取操作，用list存获取的数据
        /// 端口号可缺省返回值为list
        /// </summary>
        /// <param name="strDb"></param>
        /// <param name="strIp"></param>
        /// <param name="strUid"></param>
        /// <param name="strPsw"></param>
        /// <param name="strCmd"></param>
        /// <param name="strPort"></param>
        public List<string> ReadDatabase(string strDb, string strIp, string strUid, string strPsw, string strCmd, string strPort = "3306")
        {
            string strConn = string.Format("Database={0}; Server={1}; Port={2}; Uid={3}; Password={4};", strDb, strIp, strPort, strUid, strPsw);
            List<string> listSqlOut = new List<string>();
            //使用using语句执行完后续内容后将释放连接资源，以确保连接关闭
            //连接失败或库中没有异常数据都无法得到datatable，故使用using简化代码
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                conn.Open();
                //查询指令,似乎后加不加conn影响不大？
                MySqlCommand cmd = new MySqlCommand(strCmd,conn);
                //建立连接后查询数据库,调用Read()方法之后方法返回一个Bool值，表明下一行是否可用，返回True则可用，返回False则到达结果集末尾,
                //命令结束后返回一个SqlDataReader对象
                MySqlDataReader myReader = cmd.ExecuteReader();
                //将返回的MySqlDataReader存入list链表
                //List<string> listSqlOut = new List<string>();
                int index = 0;
                while(myReader.Read())
                {
                    listSqlOut.Add(myReader.GetString(index));
                    index++;
                }
            }
            return listSqlOut;
        }





    }
}
