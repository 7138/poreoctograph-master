using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI.DB;
using TShockAPI;
using MySql.Data.MySqlClient;
using Mono.Data.Sqlite;
using System.Data;

namespace ICMultiPurpose.Clans.DB
{
    public class ClanDB
    {
        private static IDbConnection connection;
        private static ClanDB _instance;
        
        public static ClanDB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClanDB();
                return _instance;
            }
        }
    }
}
