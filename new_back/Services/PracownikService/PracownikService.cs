using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace new_back.Services.PracownikService
{
    public class PracownikService : ControllerBase, IPracownikService
    {
        private readonly IConfiguration _configuration;

        public PracownikService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly IMapper _mapper;

        public PracownikService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<JsonResult> GetPracownik()
        {
            string query = "SELECT * FROM dbo.Pracownicy";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                SqlCommand myCommand = new SqlCommand(query, myCon);
                myCon.Open();
                myreader = myCommand.ExecuteReader();
                table.Load(myreader);
                myreader.Close();
                myCon.Close();
            }
            return table;
        }

        public async Task<JsonResult> ShowKlienci()
        {
            throw new NotImplementedException();
        }
    }
}