using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace backend.Controllers
{
    public class PracownikController : ControllerBase
    {
        private readonly IPracownikService _characterService;

        public PracownikController(IPracownikService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        [Route("GetPracownik")]
        public JsonResult GetPracownik()
        {
            
            return new JsonResult(_characterService.GetPracownik());
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
            string query = "SELECT id, CONVERT(VARCHAR(10), kiedy, 120) AS kiedy, nazwa FROM dniWolne WHERE CAST(kiedy AS DATE) = CAST(GETDATE() AS DATE) OR kiedy BETWEEN GETDATE() AND DATEADD(DAY, 5, GETDATE());";
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
            string query = "SELECT * FROM Urlopy WHERE idpracownika = @UserId AND CAST(GETDATE() AS DATE) BETWEEN CAST(od_kiedy AS DATE) AND CAST(do_kiedy AS DATE);";
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
        [Route("GetUrlopNotification")]
        public JsonResult GetUrlopNotification(int UserId)
        {
            string query = " SELECT od_kiedy, do_kiedy FROM Urlopy WHERE idpracownika = 3 AND od_kiedy BETWEEN GETDATE() AND DATEADD(day, 5, GETDATE());";
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
                // Sprawdź czy użytkownik jest aktualnie na urlopie
                string queryCurrent = "SELECT od_kiedy, do_kiedy FROM Urlopy WHERE idpracownika = @UserId AND CAST(GETDATE() AS DATE) BETWEEN CAST(od_kiedy AS DATE) AND CAST(do_kiedy AS DATE);";
                DataTable tableCurrent = new DataTable();

                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    SqlCommand myCommand = new SqlCommand(queryCurrent, myCon);
                    myCommand.Parameters.AddWithValue("@UserId", UserId);
                    myCon.Open();
                    myreader = myCommand.ExecuteReader();
                    tableCurrent.Load(myreader);
                    myreader.Close();
                    myCon.Close();
                }

                if (tableCurrent.Rows.Count == 0)
                {
                    return new JsonResult("Nie masz zaplanowanych urlopów na najbliższe 5 dni.");
                }
                else
                {
                    DateTime odKiedy = (DateTime)tableCurrent.Rows[0]["od_kiedy"];
                    DateTime doKiedy = (DateTime)tableCurrent.Rows[0]["do_kiedy"];

                    return new JsonResult($"Jesteś aktualnie na urlopie od {odKiedy:yyyy-MM-dd} do {doKiedy:yyyy-MM-dd}.");
                }
            }
            else
            {   
                string selectQuery = "SELECT DATEDIFF(day, GETDATE(), od_kiedy) AS DniDoRozpoczecia FROM Urlopy WHERE idpracownika = @UserId AND od_kiedy BETWEEN GETDATE() AND DATEADD(day, 5, GETDATE());";
                int dni2 = 0;

                using (SqlConnection myCon = new SqlConnection(sqlDatasource))
                {
                    SqlCommand myCommand = new SqlCommand(selectQuery, myCon);
                    myCommand.Parameters.AddWithValue("@UserId", UserId);
                    myCon.Open();
                    SqlDataReader dniReader = myCommand.ExecuteReader();

                    if (dniReader.Read())
                    {
                        dni2 = dniReader.GetInt32(0);
                    }

                    dniReader.Close();
                    myCon.Close();

                    if(dni2==1){
                        return new JsonResult($"Zaczynasz urlop za {dni2} dzień");
                    }
                    else{
                        return new JsonResult($"Zaczynasz urlop za {dni2} dni");
                    }
                }

 
            }
        }


        [HttpGet]
        [Route("ShowStatus")]
        public JsonResult ShowStatus(int id)
        {
            string selectQuery = "SELECT TOP 1 Id, PracownicyId, FORMAT(Wejscie, 'HH:mm:ss') AS Wejscie, FORMAT(Wyjscie, 'HH:mm:ss') AS Wyjscie, Status, Status2 FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
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
            
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();

                try
                    {
                        //wez id ostatniego rejestru
                        string getLastRejestrId = "SELECT TOP 1 Id, Status2 FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
                        SqlCommand lastRejestrId = new SqlCommand(getLastRejestrId, myCon);
                        lastRejestrId.Parameters.AddWithValue("@id", id);
                        SqlDataReader lastRejestr = lastRejestrId.ExecuteReader();
                        int rejestrId=0;
                        string status2 = null;
                        if (lastRejestr.Read()){
                            rejestrId = lastRejestr.GetInt32(0);
                            status2 = lastRejestr.GetString(1);
                        }
                        lastRejestr.Close();

                        if(rejestrId == 0){
                            string newStatus = "DECLARE @currentDateTime AS VARCHAR(19); SET @currentDateTime = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120); INSERT INTO Rejestr (PracownicyId, Wejscie, Wyjscie, Status) VALUES (@id, @currentDateTime, NULL, 'wejscie', 'w biurze');";
                            SqlCommand command = new SqlCommand(newStatus, myCon);
                            command.Parameters.AddWithValue("@rejestrId", rejestrId);
                            command.ExecuteNonQuery();
                            return new JsonResult("User nie mial zadnego statusu, ustawiono nowy status na prace w biurze");
                        }

                        switch (status2)
                        {
                            case "w biurze":

                                string updateQuery1 = "UPDATE Rejestr SET [Status2]='zdalnie' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand1 = new SqlCommand(updateQuery1, myCon);
                                updateCommand1.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand1.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na zdalną pracę.");

                            case "zdalnie":

                                string updateQuery2 = "UPDATE Rejestr SET [Status2]='wyjscie' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand2 = new SqlCommand(updateQuery2, myCon);
                                updateCommand2.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand2.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na wyjscie.");


                            case "wyjscie":

                                string updateQuery4 = "UPDATE Rejestr SET [Status2]='L4' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand4 = new SqlCommand(updateQuery4, myCon);
                                updateCommand4.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand4.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na L4.");

                            case "L4":
                                string updateQuery5 = "UPDATE Rejestr SET [Status2]='przerwa' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand5 = new SqlCommand(updateQuery5, myCon);
                                updateCommand5.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand5.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na przerwe.");

                            case "urlop":

                                return new JsonResult("Nie możesz zmienić statusu w trakcie urlopu.");

                            case "wyjazd do klienta":

                                return new JsonResult("Nie możesz zmienić statusu w trakcie wyjazdu do klienta.");

                            case "przerwa":
                                string updateQuery6 = "UPDATE Rejestr SET [Status2]='w biurze' WHERE Id = @rejestrID;";
                                SqlCommand updateCommand6 = new SqlCommand(updateQuery6, myCon);
                                updateCommand6.Parameters.AddWithValue("@rejestrId", rejestrId);
                                updateCommand6.ExecuteNonQuery();
                                return new JsonResult("Status został zmieniony na prace w biurze.");


                            default:

                                return new JsonResult($"Nie można zmienić aktualnego statusu {status2}");
                        }

                    }
                    catch (Exception ex)
                    {
                        return new JsonResult($"Wystąpił błąd: {ex.Message}");
                    }

            }
        }

        [HttpPost]
        [Route("ConfirmStatus")]
        public JsonResult ConfirmStatus(int id)
        {
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                {
                    try
                    {
                        string getLastRejestrId = "SELECT TOP 1 Id, Status2, Status FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
                        SqlCommand lastRejestrId = new SqlCommand(getLastRejestrId, myCon);
                        lastRejestrId.Parameters.AddWithValue("@id", id);
                        SqlDataReader lastRejestr = lastRejestrId.ExecuteReader();
                        int rejestrId=0;
                        string status2 = "nic";
                        string status = "nic";
                        if (lastRejestr.Read()){
                            rejestrId = lastRejestr.GetInt32(0);
                            status2 = lastRejestr.GetString(1);   
                            status = lastRejestr.GetString(2);                        
                        }
                        lastRejestr.Close();



                        switch(status2){

                            case "nic":
                                return new JsonResult("nie mozna nic zrobic, brak rejestru w bazie");

                            case "w biurze":
                                if(status=="wyjscie"){

                                    string newStatus = "DECLARE @currentDateTime AS VARCHAR(19); SET @currentDateTime = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120); INSERT INTO Rejestr (PracownicyId, Wejscie, Wyjscie, Status, Status2) VALUES (@id, @currentDateTime, NULL, 'wejscie', 'w biurze');";
                                    SqlCommand updateCommand = new SqlCommand(newStatus, myCon);
                                    updateCommand.Parameters.AddWithValue("@id", id);
                                    updateCommand.ExecuteNonQuery();

                                    return new JsonResult("Status został zmieniony.");
                                }
                                if(status=="przerwa"){
                                    string updateQuery = "UPDATE Rejestr SET Wyjscie = NULL, Status = 'wejscie' WHERE Id = @rejestrId;";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, myCon);
                                    updateCommand.Parameters.AddWithValue("@rejestrId", rejestrId);
                                    updateCommand.ExecuteNonQuery();

                                    return new JsonResult("Praca kontunuowana po przerwie");
                                }

                                return new JsonResult("Ostatni status zostal już rozpoczety.");

                            case "zdalnie":
                                if(status=="wyjscie"){
                                    string newStatus = "DECLARE @currentDateTime AS VARCHAR(19); SET @currentDateTime = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120); INSERT INTO Rejestr (PracownicyId, Wejscie, Wyjscie, Status, Status2) VALUES (@id, @currentDateTime, NULL, 'wejscie', 'zdalnie');";
                                    SqlCommand updateCommand = new SqlCommand(newStatus, myCon);
                                    updateCommand.Parameters.AddWithValue("@id", id);
                                    updateCommand.ExecuteNonQuery();

                                    return new JsonResult("Status został zmieniony.");
                                }
                                if(status=="przerwa"){
                                    string updateQuery = "UPDATE Rejestr SET Wyjscie = NULL, Status = 'wejscie' WHERE Id = @rejestrId;";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, myCon);
                                    updateCommand.Parameters.AddWithValue("@rejestrId", rejestrId);
                                    updateCommand.ExecuteNonQuery();

                                    return new JsonResult("Praca kontunuowana po przerwie");
                                }
                                return new JsonResult("Ostatni status zostal już rozpoczety.");

                            case "wyjscie":

                                if(status=="wejscie" || status=="przerwa" ){
                                    string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjscie' WHERE Id = @rejestrId;";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, myCon);
                                    updateCommand.Parameters.AddWithValue("@rejestrId", rejestrId);
                                    updateCommand.ExecuteNonQuery();

                                    return new JsonResult("Status został zmieniony.");
                                }

                                return new JsonResult("Ostatni status zostal już zakonczony.");

                            case "L4":
                                if(status=="wejscie" || status2=="przerwa"){
                                    string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjscie' WHERE Id = @rejestrId;";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, myCon);
                                    updateCommand.Parameters.AddWithValue("@rejestrId", rejestrId);
                                    updateCommand.ExecuteNonQuery();

                                    return new JsonResult("Status został zakonczony.");
                                }

                                return new JsonResult("Ostatni status zostal już zakonczony.");


                            case "urlop":
                                return new JsonResult("nie mozna zmieniac statusu podczas urlopu");

                            case "wyjazd do klienta":
                                return new JsonResult("nie mozna zmieniac statusu podczas wyjazdu do klienta");

                            case "przerwa":
                                if(status=="wejscie"){
                                    string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'przerwa' WHERE Id = @rejestrId;";
                                    SqlCommand updateCommand = new SqlCommand(updateQuery, myCon);
                                    updateCommand.Parameters.AddWithValue("@rejestrId", rejestrId);
                                    updateCommand.ExecuteNonQuery();

                                    return new JsonResult("Status został przerwany.");
                                }
                                if(status=="wyjscie"){
                                    string updateQuery6 = "UPDATE Rejestr SET [Status2]='wyjście' WHERE Id = @rejestrID;";
                                    SqlCommand updateCommand6 = new SqlCommand(updateQuery6, myCon);
                                    updateCommand6.Parameters.AddWithValue("@rejestrId", rejestrId);
                                    updateCommand6.ExecuteNonQuery();
                                    return new JsonResult("Nie można ustawić przerwy na zakonczonym rejestrze.");
                                }

                                return new JsonResult("Ostatni status zostal już zakonczony.");

                            default:
                                return new JsonResult("nie mozna nic zrobic, niepoprawny status");



                        }
                        

                        
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult($"Wystąpił błąd: {ex.Message}");
                    }
                }
            }
        }

        [HttpPost]
        [Route("CheckUrlop")]
        public JsonResult CheckUrlop()
        {
            string sqlDatasource = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                {
                    try
                    {
                        string getAllIds = "SELECT id FROM Pracownicy;";
                        List<int> idList = new List<int>();
                        using (SqlCommand cmd = new SqlCommand(getAllIds, myCon))
                        {
                            // Wykonaj zapytanie i pobierz dane
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Dodaj id do listy
                                    idList.Add(reader.GetInt32(0));
                                }
                            }
                        }

                        if(idList.Count > 0){
                            foreach (int id in idList){
                                //wez id ostatniego rejestru
                                string getLastRejestrId = "SELECT TOP 1 Id FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
                                SqlCommand lastRejestrId = new SqlCommand(getLastRejestrId, myCon);
                                lastRejestrId.Parameters.AddWithValue("@id", id);
                                SqlDataReader lastRejestr = lastRejestrId.ExecuteReader();
                                int rejestrId=0;
                                if (lastRejestr.Read()){
                                    rejestrId = lastRejestr.GetInt32(0);
                                }
                                lastRejestr.Close();

                                // Sprawdzenie, czy użytkownik jest aktualnie na urlopie
                                string checkQuery = "IF EXISTS (SELECT 1 FROM Urlopy WHERE idpracownika = @id AND CONVERT(DATE, GETDATE()) BETWEEN od_kiedy AND do_kiedy) SELECT 1 ELSE SELECT 0;";
                                SqlCommand checkCommand = new SqlCommand(checkQuery, myCon);
                                checkCommand.Parameters.AddWithValue("@id", id);
                                bool isOnVacation = (int)checkCommand.ExecuteScalar() == 1;

                                if (!isOnVacation)
                                {
                                    //sprawdzamy czy ostatni rejestr dotyczy urlopu
                                    string checkLastRejestr = "SELECT TOP 1 Status2, Wyjscie FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
                                    SqlCommand lastRejestr2 = new SqlCommand(checkLastRejestr, myCon);
                                    lastRejestr2.Parameters.AddWithValue("@id", id);
                                    SqlDataReader lastRecordReader = lastRejestr2.ExecuteReader();

                                    if (lastRecordReader.Read()){
                                        string status2 = lastRecordReader.GetString(0);
                                        DateTime? wyjscie = lastRecordReader.IsDBNull(1) ? (DateTime?)null : lastRecordReader.GetDateTime(1);
                                        lastRecordReader.Close();

                                        if(status2=="urlop"){
                                            if(wyjscie==null){
                                                string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjscie', Status2 = 'wyjscie' WHERE Id = @rejestrId;";
                                                SqlCommand updateCommand1 = new SqlCommand(updateQuery, myCon);
                                                updateCommand1.Parameters.AddWithValue("@rejestrId", rejestrId);
                                                updateCommand1.ExecuteNonQuery();

                                                
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    // Sprawdzenie, czy ostatni rejestr dotyczy urlopu
                                    string lastRecordQuery = "SELECT TOP 1 Status2, Wyjscie, Status FROM Rejestr WHERE PracownicyId = @id ORDER BY Id DESC;";
                                    SqlCommand lastRecordCommand = new SqlCommand(lastRecordQuery, myCon);
                                    lastRecordCommand.Parameters.AddWithValue("@id", id);
                                    SqlDataReader lastRecordReader = lastRecordCommand.ExecuteReader();

                                    if (lastRecordReader.Read())
                                    {
                                        string status2 = lastRecordReader.GetString(0);
                                        DateTime? wyjscie = lastRecordReader.IsDBNull(1) ? (DateTime?)null : lastRecordReader.GetDateTime(1);
                                        string status = lastRecordReader.GetString(0);

                                        lastRecordReader.Close();

                                        if (status2 == "urlop" && !wyjscie.HasValue)
                                        {
                                            
                                        }
                                        else{
                                            //jezeli ostatni rejestr został zakonczony
                                            if (status == "wyjscie"){
                                                string newRecordQuery1 = "INSERT INTO Rejestr (PracownicyId, Wejscie, Status, Status2) VALUES (@id, CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), 'wejscie', 'urlop');";
                                                SqlCommand newRecordCommand1 = new SqlCommand(newRecordQuery1, myCon);
                                                newRecordCommand1.Parameters.AddWithValue("@id", id);
                                                newRecordCommand1.ExecuteNonQuery();

                                                

                                            }
                                            //jezeli ostatni rejestr nie zostal zakonczony
                                            else{
                                                string updateQuery = "UPDATE Rejestr SET Wyjscie = CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), Status = 'wyjscie', Status2 = 'wyjscie' WHERE Id = @rejestrId;";
                                                SqlCommand updateCommand1 = new SqlCommand(updateQuery, myCon);
                                                updateCommand1.Parameters.AddWithValue("@rejestrId", rejestrId);
                                                updateCommand1.ExecuteNonQuery();

                                                string newRecordQuery1 = "INSERT INTO Rejestr (PracownicyId, Wejscie, Status, Status2) VALUES (@id, CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), 'wejscie', 'urlop');";
                                                SqlCommand newRecordCommand1 = new SqlCommand(newRecordQuery1, myCon);
                                                newRecordCommand1.Parameters.AddWithValue("@id", id);
                                                newRecordCommand1.ExecuteNonQuery();

                                                
                                            }
                                        }

                                    }
                                    else
                                    {
                                        lastRecordReader.Close();

                                        string newRecordQuery = "INSERT INTO Rejestr (PracownicyId, Wejscie, Status, Status2) VALUES (@id, CONVERT(VARCHAR(19), DATEADD(HOUR, 2, GETDATE()), 120), 'wejście', 'urlop');";
                                        SqlCommand newRecordCommand = new SqlCommand(newRecordQuery, myCon);
                                        newRecordCommand.Parameters.AddWithValue("@id", id);
                                        newRecordCommand.ExecuteNonQuery();

                                        
                                    }
                                    }
                                }

                        
                        }else{
                            return new JsonResult("Brak użytkowników w bazie");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult($"Wystąpił błąd: {ex.Message}");
                    }
                }
            }
            return new JsonResult("Operacja zakończona");
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