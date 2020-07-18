using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
public partial class 登录_longin : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public class Class1
    {
        public static string constr = "Host=47.113.87.213;Database=shanhaijing ;UserName=postgres;Password=123456789";
        public static NpgsqlConnection connection = new NpgsqlConnection(constr);
    }
    [WebMethod]
    public string[] SelectByName(string name)//写成数组的方式存储数据
    {
        string str = "select * from monsterposition where name='" + name + "'";
        NpgsqlDataAdapter md = new NpgsqlDataAdapter(str, Class1.connection);
        DataSet ds = new DataSet();
        md.Fill(ds);
        DataTable dt = ds.Tables[0];
        string[] a = { dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString() };
        Class1.connection.Close();
        return a;
    }
    [WebMethod]
    public static int Login(string a, string b)
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
    public static int Logout(string a, string b, string c, string d)
    {
        int e;
        string ConnectionString = new NpgsqlConnectionStringBuilder()
        {
            Database = "house",

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
}