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
        [Route("GetNotes")]
        public JsonResult GetNotes()
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
    string updateQuery = "UPDATE Rejestr SET Wyjscie = GETDATE(), Status = 'wyjście' WHERE Id = @id;";
    string newStatus = "INSERT INTO Rejestr (PracownicyId, Wejscie, Wyjscie, Status) VALUES (@id, GETDATE(), NULL, 'wejście');";

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
