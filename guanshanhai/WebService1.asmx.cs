using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Npgsql;
using System.Data;
using System.Web.Services;

namespace 观山海
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public class Class1
        {
            public static string constr = "Host=47.113.87.213;Database=shanhaijing ;UserName=postgres;Password=123456789";
            public static NpgsqlConnection connection = new NpgsqlConnection(constr);
        }
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string[] SelectByID(string id)//写成数组的方式存储数据
        {
            string str = "select * from monsterpoint where id = '" + id + "'";
            NpgsqlDataAdapter md = new NpgsqlDataAdapter(str, Class1.connection);
            DataSet ds = new DataSet();
            md.Fill(ds);
            DataTable dt = ds.Tables[0];
            string[] a = { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString() };
            Class1.connection.Close();
            return a;
        }
        [WebMethod]
        public string SelectByNametojson(string name)
        {
            string str = "select * from monsterpoint where name like '%" + name + "%'or ad like '%"+ name+"%' or p like '%"+name+"%'";
            NpgsqlDataAdapter md = new NpgsqlDataAdapter(str, Class1.connection);
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            md.Fill(ds1);
            dt = ds1.Tables[0];
            Class1.connection.Close();
            StringBuilder Json = new StringBuilder();
            //if (string.IsNullOrEmpty(jsonName))
            //    jsonName = dt.TableName; 
            //Json.Append("{\"" + "result" + "\":[");
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" +"\""+ dt.Rows[i][j].ToString()+"\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            //Json.Append("]}");
            string re;
            re = Json.ToString();
            return re;
        }
        [WebMethod]
        public int deletebyid(string a)
        {
            int c;
            string ConnectionString = new NpgsqlConnectionStringBuilder()
            {
                Database = "shanhaijing",

                Host = "47.113.87.213",
                Port = 5432,

                UserName = "postgres",
                Password = "123456789"
            }.ToString();
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            string Sqls = "Delete from monsterpoint where id='" + a + "'";
            NpgsqlCommand da1 = new NpgsqlCommand(Sqls, conn);
            da1.ExecuteNonQuery();
            conn.Close();
            return 1;
        }
        [WebMethod]
        public int Login(string a, string b)
        {
            int c;
            string ConnectionString = new NpgsqlConnectionStringBuilder()
            {
                Database = "shanhaijing",

                Host = "47.113.87.213",
                Port = 5432,

                UserName = "postgres",
                Password = "123456789"
            }.ToString();
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            string Sqls = "Select*from users where username='" + a + "' and password='" + b + "'";
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(Sqls, conn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            conn.Close();
            if (ds1.Tables[0].Rows.Count != 0)
            {
                c = 1;

            }
            else
            {
                c = 0;
            }
            return c;
        }
        [WebMethod]//注册
        public int Logout(string a, string b, string c, string d)
        {
            int e;
            string ConnectionString = new NpgsqlConnectionStringBuilder()
            {
                Database = "shanhaijing",

                Host = "47.113.87.213",
                Port = 5432,

                UserName = "postgres",
                Password = "123456789"
            }.ToString();
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            string Sqls = "Select*from users where username='" + a + "'";
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(Sqls, conn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);

            conn.Close();
            if (ds1.Tables[0].Rows.Count != 0)
            {
                e = 0;
            }
            else
            {
                conn.Open();
                string InsertSql = "insert into users(username,password,phone,email)values('" + a + "','" + b + "','" + c + "','" + d + "')";
                NpgsqlCommand cmd = new NpgsqlCommand(InsertSql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                e = 1;
            }
            return e;
        }
        [WebMethod]//创建新的妖怪
        public int monsterinsert(string a, string b, string c, double d,double e,string f)
        {
            int ff;
            string ConnectionString = new NpgsqlConnectionStringBuilder()
            {
                Database = "shanhaijing",

                Host = "47.113.87.213",
                Port = 5432,

                UserName = "postgres",
                Password = "123456789"
            }.ToString();
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            string Sqls = "Select*from monsterpoint where name='" + a + "'";
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(Sqls, conn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);

            conn.Close();
            if (ds1.Tables[0].Rows.Count != 0)
            {
                ff = 0;
            }
            else
            {
                conn.Open();
                string InsertSql = "insert into monsterpoint(name,p,ad,lng,lat,introduction)values('" + a + "','" + b + "','" + c + "','" + d + "','" +e+"','" + f + "')";
                NpgsqlCommand cmd = new NpgsqlCommand(InsertSql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                ff = 1;
            }
            return ff;
        }
        [WebMethod]//更新妖怪
        public int monsterupdate(string a, string b, string c, string d, string e, string f)
        {
            int ff;
            double d2, e2;
            d2 = 0.0;
            e2 = 0.0;
            string ConnectionString = new NpgsqlConnectionStringBuilder()
            {
                Database = "shanhaijing",

                Host = "47.113.87.213",
                Port = 5432,

                UserName = "postgres",
                Password = "123456789"
            }.ToString();
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            string Sqls = "Select*from monsterpoint where name='" + a + "'";
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(Sqls, conn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            DataTable dt = ds1.Tables[0];
            string[] table = { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString() };
            if ( b ==string.Empty)
            { b = table[2];
            }
            if (c == string.Empty)
            { c = table[3]; }
            if (d==string.Empty)
            { d2 = double.Parse(table[4]); }
            else if(d !=string.Empty){ d2 = double.Parse(d); }
            if (e==string.Empty)
            { e2 = double.Parse(table[5]); }
            else if(e!=string.Empty){ e2 = double.Parse(e); }
            if (f == string.Empty)
            { f = table[5]; }
            conn.Close();
            if (ds1.Tables[0].Rows.Count == 0)
            {
                ff = 0;
            }
            else
            {
                conn.Open();
                string InsertSql = "update monsterpoint set p='"+b+"',ad='"+c+"',lng='"+d2+"',lat='"+e2+"',introduction='"+f+"'where name='"+a+"'";
                NpgsqlCommand cmd = new NpgsqlCommand(InsertSql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                ff = 1;
            }
            return ff;
        }
        private static string StringFormat(string str, Type type)               
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        private static string String2Json(String s)                    
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

    }
}
