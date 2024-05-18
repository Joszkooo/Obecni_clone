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
            return null;
        }

        public async Task<JsonResult> ShowKlienci()
        {
            string query = "SELECT imie, nazwisko FROM Klienci;";
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
            return null;
        }

        JsonResult IPracownikService.GetPracownik()
        {
            throw new NotImplementedException();
        }

        JsonResult IPracownikService.ShowKlienci()
        {
            throw new NotImplementedException();
        }
    }
}