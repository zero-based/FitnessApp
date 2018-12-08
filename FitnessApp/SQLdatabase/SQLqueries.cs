using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.SQLdatabase
{
    class SQLqueries
    {
        // SQL Connection string.
        // [IMPORTANT] Add your server name to data source.
        SqlConnection Connection = new SqlConnection("data source=MICHA\\SQLEXPRESS; database=FITNESSAPP; integrated security=SSPI");

    }
}
