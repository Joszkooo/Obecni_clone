using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoAppController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ToDoAppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetPracownik")]
        public JsonResult GetPracownik()
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

            return new JsonResult(table);
        }

        [HttpGet]
        [Route("Verify")]
        public JsonResult Verify(string email)
        {
            string query = "SELECT * FROM dbo.Pracownicy WHERE Email = @email;";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@email", email);
                myCon.Open();
                myreader = myCommand.ExecuteReader();
                table.Load(myreader);
                myreader.Close();
                myCon.Close();
            }

                if (table.Rows.Count > 0)
                {
                    return new JsonResult("Zweryfikowano wlasciwie");
                }
                else
                {
                    return new JsonResult("Brak weryfikacji"); 
                }
        }

        [HttpGet]
        [Route("ShowClients")]
        public JsonResult ShowKlienci()
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

            return new JsonResult(table);
        }


        [HttpGet]
        [Route("ShowStatus")]
        public JsonResult ShowStatus(int id)
        {
            string selectQuery = "SELECT TOP 1 * FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                SqlCommand myCommand = new SqlCommand(selectQuery, myCon);
                myCommand.Parameters.AddWithValue("@id", id);
                myCon.Open();
                myreader = myCommand.ExecuteReader();
                table.Load(myreader);
                myreader.Close();
                myCon.Close();
            }

                if (table.Rows.Count > 0)
                {
                    return new JsonResult(table);
                }
                else
                {
                    return new JsonResult("Brak rejestru"); 
                }
        }

[HttpPost]
[Route("ChangeStatus")]
public JsonResult ChangeStatus(int id)
{
    string selectQuery = "SELECT TOP 1 Wyjscie FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
    string selectIdQuery = "SELECT TOP 1 Id FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
    string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(16), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjście' WHERE Id = @id;";
    string newStatus = "DECLARE @currentDateTime AS VARCHAR(16); SET @currentDateTime = CONVERT(VARCHAR(16), DATEADD(HOUR, 2, GETDATE()), 120); INSERT INTO Rejestr (PracownicyId, Wejscie, Wyjscie, Status) VALUES (@id, @currentDateTime, NULL, 'wejście');";



    string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");

    using (SqlConnection myCon = new SqlConnection(sqlDatasource))
    {
        myCon.Open();

        SqlCommand selectCommand = new SqlCommand(selectQuery, myCon);
        selectCommand.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = selectCommand.ExecuteReader();

        if (reader.Read())
        {
            if (reader.IsDBNull(0))
            {
                reader.Close();

                SqlCommand selectIdCommand = new SqlCommand(selectIdQuery, myCon);
                selectIdCommand.Parameters.AddWithValue("@id", id);
                int id2 = (int)selectIdCommand.ExecuteScalar();

                SqlCommand updateCommand = new SqlCommand(updateQuery, myCon);
                updateCommand.Parameters.AddWithValue("@id", id2);
                updateCommand.ExecuteNonQuery();

                return new JsonResult("Status został zmieniony.");
            }
            else
            {
                reader.Close();
                SqlCommand newCommand = new SqlCommand(newStatus, myCon);
                newCommand.Parameters.AddWithValue("@id", @id);
                newCommand.ExecuteNonQuery();

                return new JsonResult("Status został dodany.");
            }
        }
        else
        {
            reader.Close();
            SqlCommand newCommand = new SqlCommand(newStatus, myCon);
                newCommand.Parameters.AddWithValue("@id", @id);
                newCommand.ExecuteNonQuery();

                return new JsonResult("Statusu nie bylo, został dodany.");
        }
    }
}


    [HttpPost]
    [Route("AddUrlop")]
    public JsonResult AddUrlop(int id, string od_kiedy, string do_kiedy)
    {
        string query = "INSERT INTO Urlopy (idpracownika, od_kiedy, do_kiedy) VALUES (@id, @od_kiedy, @do_kiedy);";
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@od_kiedy", od_kiedy);
            command.Parameters.AddWithValue("@do_kiedy", do_kiedy);
            command.ExecuteNonQuery();
            connection.Close();
        }

            return new JsonResult(new { id = id, od_kiedy = od_kiedy, do_kiedy = do_kiedy });
        }

    [HttpPost]
    [Route("AddWyjazd")]
    public JsonResult AddWyjazd(int idPracownika, int idKlienta, string data, string od_kiedy, string do_kiedy)
    {
        string query = "INSERT INTO WyjazdyDoKlientow (idPracownika, idKlienta, dataWyjazdu, godzinaOd, godzinaDo) VALUES (@idPracownika, @idKlienta, @data, @od_kiedy, @do_kiedy);";
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@idPracownika", idPracownika);
            command.Parameters.AddWithValue("@idKlienta", idKlienta);
            command.Parameters.AddWithValue("@data", data);
            command.Parameters.AddWithValue("@od_kiedy", od_kiedy);
            command.Parameters.AddWithValue("@do_kiedy", do_kiedy);
            command.ExecuteNonQuery();
            connection.Close();
        }

        return new JsonResult("Dodano wyjazd do klienta");
    }

        [HttpDelete]
        [Route("DeleteNotes")]
        public JsonResult  DeleteNotes(int id)
        {
            string query = "delete from dbo.notes where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@id",id);
                myreader = myCommand.ExecuteReader();
                table.Load(myreader);
                myreader.Close();
                myCon.Close();
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
