<template>
  <div id="tabela">
    <table>
      <thead class="rounded-header">
      <tr>
        <th>Pseudonim</th>
        <th>Czas Pracy</th>
        <th>Wejście</th>
        <th>Wyjście</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="(note,  index) in notes" :key="note.id" :class="{ 'odd-row': index % 2 === 0, 'even-row': index % 2 === 1 }">
        <td>{{ note.Imie}}</td>
        <td>{{ note.worktime }}</td>
        <td>{{ note.wejscie }}</td>
        <td>{{ note.wyjscie }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import {decodeCredential, GoogleLogin, googleLogout} from "vue3-google-login";
import axios from "axios";
import moment from "moment";

export default {
  components: {GoogleLogin},
  data() {
    return {
      currentDate: moment(),
      urlop: {
        start: "",
        end: "",
        info: ""
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
      duration: 0,
      selectedUserId: null,
      freeDay: '',
      callback: (response) => {
        console.log("zalogowano")
        this.loggedIn = true
        console.log(response)
        this.user = decodeCredential(response.credential)

      }
    }
  },
  methods: {
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
      axios.get(`https://localhost:7285/api/ToDoApp/ShowStatus?id=` + id + `&dzien=` + day_date)
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
              };

              // Uruchom setInterval, aby co sekundę aktualizować czas pracy
              updateWorkTime(); // Wywołaj raz, aby zainicjować czas pracy
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

    addUrlop(od_kiedy, do_kiedy) {
      axios.post("https://localhost:7285/api/ToDoApp/AddUrlop?id=" + this.selectedUserId + "&od_kiedy=" + od_kiedy + "&do_kiedy=" + do_kiedy).then(
          (response) => {
            alert(response.data);
            console.log(this.selectedUserId);

          }
      )
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

<style scoped>
#tabela {
  margin-top: 50px;
  margin-right: 2.5%;
  margin-left: 2.5%;
}

table {
  width: 100%;
  border-collapse: collapse;
  color: white;
  font-size: 1.4vw;
  font-weight: lighter;
  font-family: "Open sans";
  margin-top: 15%;
}

th, td {
  padding: 20px;
}

td {
  text-align: center;
}

th {
  font-weight: lighter;
}

thead {
  background-color: #101936;
  text-align: center;
}

tbody {
  border-radius: 20%;
}

.rounded-header th:first-child {
  border-top-left-radius: 10px; /* Zaokrąglamy lewy górny róg */
  border-bottom-left-radius: 10px;
}

.tbody-margin {
  margin-top: 10px; /* Dodaj margines między thead a tbody */
}

.rounded-header th:last-child {
  border-top-right-radius: 10px; /* Zaokrąglamy prawy górny róg */
  border-bottom-right-radius: 10px; /* Zaokrąglamy prawy górny róg */
}

.odd-row {
  background-color: #212B4E; /* Kolor tła dla wierszy o nieparzystych indeksach */
}

.even-row {
  background-color: #101936; /* Kolor tła dla wierszy o parzystych indeksach */
}
</style>
