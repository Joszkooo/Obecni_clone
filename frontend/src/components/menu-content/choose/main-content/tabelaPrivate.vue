<script >

import {decodeCredential, GoogleLogin, googleLogout} from "vue3-google-login";
import axios from "axios";
import moment from "moment";
export default {
  components: {GoogleLogin},
  data(){
    return {
      opened: [],
      rows: [
        { id: 1, name: 'Bill', handle: 'bill' },
      ],
      currentDate: moment(),
      urlop: {
        start: "",
        end: "",
        info: ""
      },
      events: [],
      gapiLoaded: false,
      clientId: '261479002576-vvtpb4ctt25gtd6rtlhgfsi72nuj4ipv.apps.googleusercontent.com', // Podaj swój Client ID
      apiKey: 'AIzaSyC7S4P4TH1BdF2blBb9Jz3IaQl8cvTd-p8', // Podaj swój API Key
      discoveryDocs: ["https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest"],
      scopes: "https://www.googleapis.com/auth/calendar",
      calendarId: '59a6ad313c7f550c6797e8a37a562d234918fbf16ba1f3a12b0d1b8935585c0a@group.calendar.google.com', // ID kalendarza
      newEvent: {
        summary: '',
        startDateTime: '',
        endDateTime: '',
      },
      allstatus: [],
      day_date1: '2024-05-30',
      statusPokazany: false,
      formularzPokazany: false,
      loggedIn: false,
      user: null,
      notes: [],
      status1: [],
      selectedDateStart: '',
      selectedDateEnd: '',
      kto: '',
      duration: 0,
      selectedUserId: null,
      freeDay: '',
      expandedRows: [],
      callback: (response) => {
        console.log("zalogowano")
        this.loggedIn = true
        console.log(response)
        this.user = decodeCredential(response.credential)

      }
    }
  },
  methods: {
    toggle(id) {
      const index = this.opened.indexOf(id);
      if (index > -1) {
        this.opened.splice(index, 1);
      } else {
        this.opened.push(id);
      }
    },
    statusofwork(id) {
      axios.post("https://localhost:7285/api/ToDoApp/ChangeStatus2?id=" + id)
          .then((response) => {
            console.log(response.data)
            this.showTable()
          })

    },
    confirmstatus(id) {
      axios.post("https://localhost:7285/api/ToDoApp/ConfirmStatus?id=" + id)
          .then((response) => {
            alert(response.data)
            this.showTable()
          })

    },
    calculateDuration() {
      if (this.selectedDateStart && this.selectedDateEnd && this.selectedDateStart < this.selectedDateEnd) {
        const startDate = new Date(this.selectedDateStart);
        const endDate = new Date(this.selectedDateEnd);
        const diffTime = Math.abs(endDate - startDate);
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
        this.duration = diffDays;
      } else {
        this.duration = 0;
      }
    },
    pokazFormularz(id) {
      this.selectedUserId = id;
      this.formularzPokazany = !this.formularzPokazany;
      if (!this.formularzPokazany) {
        this.selectedDateStart = '';
        this.selectedDateEnd = '';
        this.duration = '';
        this.urlop.end = '';
        this.urlop.start = '';
      }
      console.log(this.selectedUserId)
    },
    pokazallstatus(id) {
      this.selectedUserId = id;
      this.statusPokazany = !this.statusPokazany
    },
    async showTable() {
      axios.get("https://localhost:7285/api/ToDoApp/GetPracownik")
          .then(response => {
            this.notes = response.data.map(note => ({...note, status: '', wejscie: '', wyjscie: '', status2: ''}));
            this.notes.forEach(note => this.showlaststatus(note.Id));
          })
          .catch(error => {
            console.error('Wystąpił błąd podczas pobierania notatek:', error);
          });
    },
    getUrlop(id) {
      axios.get("https://localhost:7285/api/ToDoApp/GetUrlop?UserId=" + id)
          .then(response => {
            console.log(response.data)
            this.urlop.start = moment(response.data[0].od_kiedy).format('YYYY-MM-DD');
            this.urlop.end = moment(response.data[0].do_kiedy).format('YYYY-MM-DD');

          })
    },
    getUrlopnoti(id) {
      axios.get('https://localhost:7285/api/ToDoApp/GetUrlopNotification?UserId=' + id)
          .then(response => {
            this.urlop.info = response.data
          })
    },
    showlaststatus(id) {
      const day_date = this.currentDate.format('YYYY-MM-DD');
      axios.get(`https://localhost:7285/api/ToDoApp/ShowStatus?id=${id}&dzien=${day_date}`)
          .then(response => {
            const data = response.data[0];
            const status = data.Status;
            const status2 = data.Status2;
            const entry = data.Wejscie;
            const entryTime = moment(entry, 'HH:mm:ss');
            const exit = data.Wyjscie;
            const exitTime = exit ? moment(exit, 'HH:mm:ss') : null; // Użyj bieżącego czasu, jeśli nie ma czasu wyjścia

            const note = this.notes.find(note => note.Id === id);

            if (note) {
              note.status2 = status2;
              note.status = status;
              note.wejscie = entryTime.format('HH:mm');
              note.wyjscie = exitTime ? exitTime.format('HH:mm') : null; // Użyj czasu wyjścia lub bieżącego czasu

              // Oblicz i zaktualizuj czas pracy
              const updateWorkTime = () => {
                // Ustaw worktime na 08:00:00 gdy status2 === 'L4'
                if (status2 === 'urlop') {
                  note.worktime = '08:00:00';
                } else {
                  const now = moment();
                  const start = entryTime;
                  const end = exitTime || now;

                  // Jeśli czas wyjścia jest wcześniejszy niż czas wejścia, dodaj jeden dzień do czasu wyjścia
                  if (exitTime && exitTime.isBefore(entryTime)) {
                    end.add(1, 'day');
                  }

                  const duration = moment.duration(end.diff(start));
                  const hours = Math.max(duration.hours(), 0).toString().padStart(2, '0');
                  const minutes = Math.max(duration.minutes(), 0).toString().padStart(2, '0');
                  const seconds = Math.max(duration.seconds(), 0).toString().padStart(2, '0');
                  note.worktime = `${hours}:${minutes}:${seconds}`;
                }
              };

              // Wywołaj updateWorkTime raz na początku
              updateWorkTime();

              // Uruchom setInterval, aby co sekundę aktualizować czas pracy
              setInterval(updateWorkTime, 1000);
            }
          })
          .catch(error => {
            console.error('Wystąpił błąd podczas pobierania statusu:', error);
          });
    },


    changeStatus(id) {
      axios.post("https://localhost:7285/api/ToDoApp/ChangeStatus?id=" + id).then(
          (response) => {
            alert(response.data);
            this.showTable();
          }
      )
    },

    addUrlop(od_kiedy, do_kiedy,summary) {
      axios.post("https://localhost:7285/api/ToDoApp/AddUrlop?id=" + this.selectedUserId + "&od_kiedy=" + od_kiedy + "&do_kiedy=" + do_kiedy).then(
          (response) => {
            alert(response.data);
            console.log(this.selectedUserId);
            this.addEventToCalendar(summary, od_kiedy, do_kiedy);
            this.showTable()
          }
      )
    },
    addEventToCalendar(summary, startDateTime, endDateTime) {
      const authInstance = gapi.auth2.getAuthInstance();
      if (!authInstance.isSignedIn.get()) {
        authInstance.signIn().then(() => {
          this.createEventToCalendar(summary, startDateTime, endDateTime);
        }).catch(error => {
          console.error('Error during sign in: ', error);
        });
      } else {
        this.createEventToCalendar(summary, startDateTime, endDateTime);
      }
    },

    createEventToCalendar(summary, startDateTime, endDateTime) {
      // Parsowanie dat
      const start = moment(startDateTime);
      const end = moment(endDateTime);

      // Sprawdzenie, czy daty są prawidłowe
      if (!start.isValid() || !end.isValid()) {
        console.error('Invalid date values:', { startDateTime, endDateTime });
        return;
      }

      // Formatowanie dat w ISO
      const event = {
        summary: summary,
        start: {
          dateTime: start.toISOString(),
          timeZone: 'UTC',
        },
        end: {
          dateTime: end.toISOString(),
          timeZone: 'UTC',
        },
      };

      // Dodanie wydarzenia do kalendarza
      gapi.client.calendar.events.insert({
        calendarId: this.calendarId,
        resource: event,
      }).then(response => {
        console.log('Event created in Google Calendar: ', response);
      }).catch(error => {
        console.error('Error creating event in Google Calendar: ', error);
      });
    },

    Checkurlop() {
      axios.post("https://localhost:7285/api/ToDoApp/CheckUrlop").then(
          (response) => {
            alert(response.data);
          }
      )
    },
    logout() {
      googleLogout()
      this.loggedIn = false
    },

    getallrejestr(id, day_date) {
      axios.get('https://localhost:7285/api/ToDoApp/GetRejestrPracownika?idPracownika=' + id + '&dzien=' + day_date).then(
          (response) => {
            console.log(response.data);
            this.allstatus = response.data;
          }
      )
    },
    prevDay() {
      this.currentDate = this.currentDate.subtract(1, 'days');
      console.log(this.currentDate)
    },
    nextDay() {
      this.currentDate = this.currentDate.add(1, 'days');
      console.log(this.currentDate)

    }

  }, mounted: function () {
    this.showTable()
    this.Checkurlop()
  },
  computed: {
    formattedDate() {
      return moment(this.currentDate).format('YYYY-MM-DD');
    }
  },
}
</script>
<template>
  <div id="tabela-container">
    <div class="date-navigation">
      <button class="pervday" @click="prevDay"><img src="@/assets/ikony/left-arrow.png" style="width: 1vw"></button>
      <span class="currentdate">{{ currentDate.format('YYYY-MM-DD') }}</span>
      <button class="nextday" @click="nextDay"><img src="@/assets/ikony/right-arrow.png" style="width: 1vw"></button>
    </div>
    <table class="tabelacroll">
      <thead class="rounded-header">
      <tr>
        <th><img src="@/assets/ikony/user.png" style="width: 1.3vw; margin-right: 4%">Pracownik</th>
        <th>Status</th>
        <th>Czas Pracy</th>
        <th>Wejście</th>
        <th>Wyjście</th>
        <th>Zmiana statusu</th>
        <th>Urlop</th>
      </tr>
      </thead>
      <tbody style="border-radius: 5px">
      <tr v-for="(note,  index) in notes" :key="note.id" :class="{ 'odd-row': index % 2 === 0, 'even-row': index % 2 === 1 }">
        <!-- ...existing table cells... -->
        <td>
          <template v-for="row in rows" :key="row.id">

            <a @click="toggle(note.Id) ; getallrejestr(note.Id,currentDate.format('YYYY-MM-DD'))">{{ note.Imie }} {{ note.Nazwisko }} </a>
            <tr @click="toggle(note.Id)" :class="{ opened: opened.includes(note.Id) }">

            </tr>
            <tr v-if="opened.includes(note.Id)">
              <td colspan="7" class="opened">
                Status:
                <div v-for="status in allstatus" :key="status.Id">
                  {{ status.Wejscie }} ->
                  {{ status.Status }}<br>
                  {{ status.Wyjscie }} ->
                  {{ status.Status2 }}
                </div>
              </td>
            </tr>
          </template>
        </td>
        <td>{{ note.status }}
          <template v-for="row in rows" :key="row.id">
            <tr @click="toggle(note.Id)" :class="{ opened: opened.includes(note.Id) }">
            </tr>
            <tr v-if="opened.includes(note.Id)">
              <td colspan="7">
                <div v-for="status in allstatus" :key="status.Id">
                  <br>
                  <br>
                </div>
              </td>
            </tr>
          </template>
        </td>
        <td>{{ note.worktime }}
          <tr v-if="opened.includes(note.Id)">
            <td colspan="7">
              <div v-for="status in allstatus" :key="status.Id">
                <br>
                <br>
              </div>
            </td>
          </tr>
        </td>
        <td>{{ note.wejscie }}
          <tr v-if="opened.includes(note.Id)">
            <td colspan="7">
              <div v-for="status in allstatus" :key="status.Id">
                <br>
                <br>
              </div>
            </td>
          </tr>
        </td>
        <td>{{ note.wyjscie }}
          <tr v-if="opened.includes(note.Id)">
            <td colspan="7">
              <div v-for="status in allstatus" :key="status.Id">
                <br>
                <br>
              </div>
            </td>
          </tr>
        </td>
        <td>
          <a @click="statusofwork(note.Id)">
            <img v-if="note.status2 === 'zdalnie'" style="width: 2vw" src="@/assets/ikony/zdalnie.png">
            <img v-if="note.status2 === 'wyjscie'" style="width: 2vw" src="@/assets/ikony/wyjscie.png">
            <img v-if="note.status2 === 'L4'" style="width: 2vw" src="@/assets/ikony/l4.png">
            <img v-if="note.status2 === 'przerwa'" style="width: 2vw" src="@/assets/ikony/przerwa.png">
            <img v-if="note.status2 === 'w biurze'" style="width: 2vw" src="@/assets/ikony/w_biurze.png">
            <img v-if="note.status2 === 'urlop'" style="width: 2vw" src="@/assets/ikony/urlop.png">
          </a>
          <a @click="confirmstatus(note.Id)">
            <img style="width: 2vw; margin-left: 4%" src="@/assets/ikony/check.png">
          </a>
          <tr v-if="opened.includes(note.Id)">
              <div v-for="status in allstatus" :key="status.Id">
                <br>
                <br>
              </div>
          </tr>
        </td>
        <td>
          <div class="blur" v-if="formularzPokazany" style="position: fixed; right: 1px;bottom: 1px"></div>
          <div class="urlop-formularz" v-if="formularzPokazany">
            <form class="formurlop" @submit.prevent="addUrlop(selectedDateStart,selectedDateEnd,kto+selectedUserId)">
              <label></label>
              <input type="text" v-model="kto">
              <label>Wybierz urlop</label><br>
              <input type="date" id="poczatek_urlop" v-model="selectedDateStart" @input="calculateDuration">
              <input type="date" id="koniec_urlopu" v-model="selectedDateEnd" @input="calculateDuration"><br>
              <input class="submitbutton" type="submit" value="Enjoy your holiday!" @click="addEventToCalendar">
              <input class="closebutton" type="button" value="Zamknij" @click="pokazFormularz(this.selectedUserId)">
            </form>
          </div>
          <a @click="pokazFormularz(note.Id);getUrlopnoti(note.Id)">
            <img src="@/assets/ikony/calendar.png" style="width: 3vw">
          </a>
          <tr v-if="opened.includes(note.Id)">
            <div v-for="status in allstatus" :key="status.Id">
              <br>
              <br>
            </div>

          </tr>
        </td>
      </tr>
      </tbody>
    </table>
  </div>

</template>

<style scoped>

.currentdate {
  background-color: #101936;
  padding: 5px 3%;
  border-radius: 10px;
  color: white;
  margin-left: 1%;
  margin-right: 1%;
}

.pervday, .nextday {
  background-color: #101936;
  padding: 5px;
  border-radius: 10px;
  border: none;
  text-align: center;
}


.tabelacroll {
  overflow-x: auto;
  border-radius: 10px;
}

#tabela-container {
  margin-top: 50px;
  margin-right: 2.5%;
  margin-left: 2.5%;
  display: flow-root;
  max-height: 400px; /* Maksymalna wysokość kontenera tabeli */
  overflow-y: auto; /* Dodanie paska przewijania pionowego */
}

.formurlop {
  padding: 50px;
}

table {
  width: 100%;
  border-collapse: collapse;
  color: white;
  font-size: 1.4vw;
  font-weight: lighter;
  font-family: "Open sans";
}

th, td {
  padding: 15px;
  text-align: center;
}

th {
  font-weight: lighter;
}

thead {
  background-color: #101936;
  text-align: center;
  position: sticky;
  top: 0;
  z-index: 1;
}

thead th {
  position: sticky;
}

tbody tr:first-child {
  border-top-left-radius: 10px;
  border-bottom-left-radius: 10px;
}

.rounded-header th:first-child {
  border-top-left-radius: 10px;
  border-bottom-left-radius: 10px;
}

.rounded-header th:last-child {
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
}

.odd-row {
  background-color: #212B4E;
}

.even-row {
  background-color: #101936;
}

thead th {
  position: sticky;
  top: 0;
  z-index: 1;
}

#tabela-container::-webkit-scrollbar {
  width: 12px; /* Szerokość suwaka */
}

#tabela-container::-webkit-scrollbar-track {
  background: #101936; /* Kolor tła ścieżki suwaka */
}

#tabela-container::-webkit-scrollbar-thumb {
  background-color: #6B72FF; /* Kolor suwaka */
}

#tabela-container::-webkit-scrollbar-button:vertical {
  height: 20px; /* Wysokość przycisku suwaka */
}

#tabela-container::-webkit-scrollbar-button:horizontal {
  width: 20px; /* Szerokość przycisku suwaka */
}

#tabela-container::-webkit-scrollbar-button:vertical {
  background-color: #101936;
}

.urlop-formularz {
  position: fixed;
  top: 50px;
  left: 50%;
  transform: translateX(-50%);
  z-index: 1000;
}

.blur {
  z-index: 999;
  backdrop-filter: blur(5px);
  width: 100%;
  height: 100%;
}

.allstatus {
  width: 30%;
  height: 30%;
  background-color: #101936;
  z-index: 1000;
  position: fixed;
  top: 40%;
  left: 50%;
  transform: translateX(-50%);
  border: 1px solid white;
  border-radius: 10px;
}

.urlop-formularz {
  background-color: #101936;
  border-radius: 10px;
  border: 1px solid white;
  margin-top: 10%;
  color: white;
  padding: 10px;
}

.submitbutton, .closebutton {
  text-decoration: none;
  background-color: #6B72FF;
  padding: 14px;
  border-radius: 10px;
  border: none;
  margin-top: 3%;
  color: white;
  margin-right: 3%;
}

#poczatek_urlop, #koniec_urlopu {
  padding: 14px;
  border-radius: 10px;
  margin-top: 3%;
  color: white;
  background-color: #212B4E;
  box-shadow: none;
  border: 1px solid black;
}

.formurlop {
  padding: 60px;
}

.date-navigation {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-bottom: 2%;
}


</style>