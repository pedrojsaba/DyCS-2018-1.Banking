using System.Configuration;

namespace Banking.Infrastructure.Common
{
    public class Functions
    {
        public static string GetConnectionString()
        {
            return "Data Source=9a45b3ea-babb-49ac-9302-a8fb0124febf.sqlserver.sequelizer.com;Initial Catalog=db9a45b3eababb49ac9302a8fb0124febf;User ID=mdbobdbanlsktiug;Password=GMWWfKEBsmEGAYMVisAtBHGUGqkW8zGJ6ihGt2a2KGoZKBGNW5rA2CV7fbpXyRC2";// ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
