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
        [Route("ShowWolne")]
        public JsonResult ShowWolne()
        {
            string query = "SELECT id, CONVERT(VARCHAR(10), kiedy, 120) AS kiedy, nazwa FROM dniWolne WHERE kiedy BETWEEN GETDATE() AND DATEADD(DAY, 5, GETDATE());";
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
        [Route("GetUrlop")]
        public JsonResult GetUrlop(int UserId)
        {
            string query = "SELECT * FROM Urlopy WHERE idpracownika = @UserId AND GETDATE() BETWEEN od_kiedy AND do_kiedy;";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                SqlCommand myCommand = new SqlCommand(query, myCon);
                myCommand.Parameters.AddWithValue("@UserId", UserId);
                myCon.Open();
                myreader = myCommand.ExecuteReader();
                table.Load(myreader);
                myreader.Close();
                myCon.Close();
            }
            
            if (table.Rows.Count == 0)
                {
                    return new JsonResult("Brak aktywnych urlopow");
                }

            return new JsonResult(table);
        }


        [HttpGet]
        [Route("ShowStatus")]
        public JsonResult ShowStatus(int id)
        {
            string selectQuery = "SELECT TOP 1 Id, PracownicyId, FORMAT(Wejscie, 'HH:mm:ss') AS Wejscie, FORMAT(Wyjscie, 'HH:mm:ss') AS Wyjscie, Status FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
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



        [HttpGet]
        [Route("GetHD")]
        public JsonResult GetHD(string data)
        {
            string selectQuery = "select * from listaHD where kiedy = @data;";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myreader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                SqlCommand myCommand = new SqlCommand(selectQuery, myCon);
                myCommand.Parameters.AddWithValue("@data", data);
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
                    return new JsonResult($"Nikt nie jest na HD dnia {data}"); 
                }
        }


        [HttpPost]
        [Route("ChangeStatus")]
        public JsonResult ChangeStatus(int id)
        {
            string selectQuery = "SELECT TOP 1 Wyjscie FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
            string selectIdQuery = "SELECT TOP 1 Id FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
            string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjście' WHERE Id = @id;";
            string newStatus = "DECLARE @currentDateTime AS VARCHAR(19); SET @currentDateTime = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120); INSERT INTO Rejestr (PracownicyId, Wejscie, Wyjscie, Status) VALUES (@id, @currentDateTime, NULL, 'wejście');";


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
        [Route("ChangeStatus2")]
        public JsonResult ChangeStatus2(int id)
        {
            string selectQuery = "SELECT TOP 1 Id, Status FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
            string selectIdQuery = "SELECT TOP 1 Id FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
            string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjście' WHERE Id = @id;";
            string newStatusQuery = "DECLARE @currentDateTime AS VARCHAR(19); SET @currentDateTime = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120); INSERT INTO Rejestr (PracownicyId, Wejscie, Wyjscie, Status) VALUES (@id, @currentDateTime, NULL, 'w biurze');";


            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();

                SqlCommand selectCommand = new SqlCommand(selectQuery, myCon);
                selectCommand.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {   
                        //zapisujemy obecny status i jego id
                        string currentStatus = reader.GetString(reader.GetOrdinal("Status"));
                        int rejestrId = reader.GetInt32(reader.GetOrdinal("Id"));
                        string newStatus;
                        reader.Close();

                        switch (currentStatus)
                        {
                            case "w biurze":
                                newStatus = "zdalnie";
                                string updateQuery1 = "UPDATE Rejestr SET [Status]='zdalnie' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand1 = new SqlCommand(updateQuery1, myCon);
                                updateCommand1.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand1.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na zdalną pracę.");

                            case "zdalnie":
                                newStatus = "wyjscie do klienta";
                                string updateQuery2 = "UPDATE Rejestr SET [Status]='wyjscie do klienta' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand2 = new SqlCommand(updateQuery2, myCon);
                                updateCommand2.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand2.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na wyjscie do klienta.");

                            case "wyjscie do klienta":
                                newStatus = "wyjscie";
                                string updateQuery3 = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjscie' WHERE Id = @rejestrId;";
                                SqlCommand updateCommand3 = new SqlCommand(updateQuery3, myCon);
                                updateCommand3.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand3.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na wyjscie.");

                            case "wyjscie":
                                newStatus = "L4";
                                string updateQuery4 = "UPDATE Rejestr SET [Status]='L4' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand4 = new SqlCommand(updateQuery4, myCon);
                                updateCommand4.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand4.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na L4.");

                            default:

                                newStatus = "w biurze";
                                SqlCommand newCommand5 = new SqlCommand(newStatusQuery, myCon);
                                newCommand5.Parameters.AddWithValue("@id", @id);
                                newCommand5.ExecuteNonQuery();
                                return new JsonResult("Nowy status został dodany.");
                        }

                    }
                    else
                    {
                        reader.Close();
                        SqlCommand newCommand6 = new SqlCommand(newStatusQuery, myCon);
                        newCommand6.Parameters.AddWithValue("@id", @id);
                        newCommand6.ExecuteNonQuery();
                        return new JsonResult("Status został dodany.");
                    }
                }
                else
                {
                    reader.Close();
                    SqlCommand newCommand7 = new SqlCommand(newStatusQuery, myCon);
                    newCommand7.Parameters.AddWithValue("@id", @id);
                    newCommand7.ExecuteNonQuery();

                    return new JsonResult("Statusu nie bylo, został dodany.");
                }
            }
        }

        [HttpPost]
        [Route("AddUrlop")]
        public JsonResult AddUrlop(int id, string od_kiedy, string do_kiedy)
        {
            string checkQuery = "SELECT * FROM Urlopy WHERE idPracownika = @id AND ((@od_kiedy BETWEEN od_kiedy AND do_kiedy) OR (@do_kiedy BETWEEN od_kiedy AND do_kiedy) OR (od_kiedy BETWEEN @od_kiedy AND @do_kiedy) OR (do_kiedy BETWEEN @od_kiedy AND @do_kiedy));";
            string query = "INSERT INTO Urlopy (idpracownika, od_kiedy, do_kiedy) VALUES (@id, @od_kiedy, @do_kiedy);";
            string connectionString = _configuration.GetConnectionString("DefaultConnection");


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@id", id);
                        checkCommand.Parameters.AddWithValue("@od_kiedy", od_kiedy);
                        checkCommand.Parameters.AddWithValue("@do_kiedy", do_kiedy);
                        int? existingCount = checkCommand.ExecuteScalar() as int?;

                        if (existingCount != null)
                        {
                            return new JsonResult($"Nie mozna ustawić urlopu w dniach {od_kiedy}-{do_kiedy} dla pracownika {id}");
                        }
                    }

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@od_kiedy", od_kiedy);
                command.Parameters.AddWithValue("@do_kiedy", do_kiedy);
                command.ExecuteNonQuery();
                connection.Close();
            }

            return new JsonResult("Dodano urlop");
        }


        [HttpPost]
        [Route("AddWolne")]
        public JsonResult AddWolne(string kiedy, string nazwa)
        {
            string checkQuery = "SELECT COUNT(*) FROM dniWolne WHERE kiedy = @kiedy";
            string addQuery = "INSERT INTO dniWolne (kiedy, nazwa) VALUES (@kiedy, @nazwa);";
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection myCon = new SqlConnection(connectionString))
                {
                    myCon.Open();

                    // Sprawdź, czy dzień już istnieje w tabeli dniWolne
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, myCon))
                    {
                        checkCommand.Parameters.AddWithValue("@kiedy", kiedy);
                        int existingCount = (int)checkCommand.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            return new JsonResult($"Dzień {kiedy} jest już uznany za dzień wolny.");
                        }
                    }

                    // Dodaj nowy dzień wolny
                    using (SqlCommand addCommand = new SqlCommand(addQuery, myCon))
                    {
                        addCommand.Parameters.AddWithValue("@kiedy", kiedy);
                        addCommand.Parameters.AddWithValue("@nazwa", nazwa);
                        int rowsAffected = addCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return new JsonResult($"Dodano dzień wolny: {kiedy}");
                        }
                        else
                        {
                            return new JsonResult("Błąd podczas dodawania dnia wolnego.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("AddHD")]
        public JsonResult AddHD(int id, string kiedy)
        {
            string checkQuery = "SELECT * FROM listaHD WHERE idPracownika = @id AND kiedy = @kiedy;";
            string addQuery = "INSERT INTO listaHD (idPracownika, kiedy) VALUES (@id, @kiedy);";
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", id);
                checkCommand.Parameters.AddWithValue("@kiedy", kiedy);
                SqlDataReader reader = checkCommand.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Close();
                    connection.Close();
                    return new JsonResult("Pracownik ma tego dnia HD");
                }
                else
                {
                    reader.Close();
                    SqlCommand addCommand = new SqlCommand(addQuery, connection);
                    addCommand.Parameters.AddWithValue("@id", id);
                    addCommand.Parameters.AddWithValue("@kiedy", kiedy);
                    addCommand.ExecuteNonQuery();
                    connection.Close();
                    return new JsonResult("Dodano pracownika HD");
                }
            }
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


        [HttpDelete]
        [Route("DeleteHD")]
        public JsonResult DeleteNotes(int id, string kiedy)
        {
            string query = "DELETE FROM listaHD WHERE idPracownika = @id AND kiedy = @kiedy";
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id", id);
                        myCommand.Parameters.AddWithValue("@kiedy", kiedy);
                        int rowsAffected = myCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return new JsonResult($"Usunięto pracownika z dnia {kiedy}");
                        }
                        else
                        {
                            return new JsonResult($"Pracownika o id {id} nie bylo zapisanego dnia {kiedy}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("DeleteWolne")]
        public JsonResult DeleteWolne(string kiedy)
        {
            string query = "DELETE FROM dniWolne WHERE kiedy = @kiedy";
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@kiedy", kiedy);
                        int rowsAffected = myCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return new JsonResult($"Usunięto wolne z dnia {kiedy}");
                        }
                        else
                        {
                            return new JsonResult($"Dnia {kiedy} nie bylo zapisanego wolnego");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

    }
}
