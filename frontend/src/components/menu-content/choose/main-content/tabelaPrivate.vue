<script >

import {decodeCredential, GoogleLogin, googleLogout} from "vue3-google-login";
import axios from "axios";
import moment from "moment";
export default {
  components: {GoogleLogin},
  data(){
    return{
      formularzPokazany: false,
      loggedIn: false,
      user: null,
      notes: [],
      status1: [],
      selectedDateStart: '',
      selectedDateEnd: '',
      duration: 0,
      selectedUserId: null,
      callback:(response)=> {
        console.log("zalogowano")
        this.loggedIn = true
        console.log(response)
        this.user = decodeCredential(response.credential)

      }
    }
  },
  methods:{
    calculateDuration() {
      if (this.selectedDateStart && this.selectedDateEnd && this.selectedDateStart<this.selectedDateEnd) {
        const startDate = new Date(this.selectedDateStart);
        const endDate = new Date(this.selectedDateEnd);
        const diffTime = Math.abs(endDate - startDate);
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
        this.duration = diffDays;
      } else {
        this.duration = 0;
      }},
    pokazFormularz(id) {
      this.selectedUserId = id;
      this.formularzPokazany = !this.formularzPokazany;
      if(!this.formularzPokazany) {
        this.selectedDateStart =  '';
        this.selectedDateEnd='';
        this.duration='';
      }
      console.log(this.selectedUserId)
    },
    async showTable() {
      axios.get("https://localhost:7285/api/ToDoApp/GetPracownik")
          .then(response => {
            this.notes = response.data.map(note => ({ ...note, status: '',wejscie: '', wyjscie: '' }));
            this.notes.forEach(note => this.showlaststatus(note.Id));
          })
          .catch(error => {
            console.error('Wystąpił błąd podczas pobierania notatek:', error);
          });
    },
    showlaststatus(id) {
      axios.get("https://localhost:7285/api/ToDoApp/ShowStatus?id=" + id)
          .then(response => {
            const status = response.data[0].Status;
            const wejscie = response.data[0].Wejscie;
            const godzina_wejscia = moment(wejscie);
            const wyjscie = response.data[0].Wyjscie;
            const godzina_wyjscia = wyjscie ? moment(wyjscie) : null;
            const note = this.notes.find(note => note.Id === id);

            if (note) {
              note.status = status;
              note.wejscie = godzina_wejscia.format('HH:mm');
              note.wyjscie = godzina_wyjscia ? godzina_wyjscia.format('HH:mm') : null;

              // Jeśli nie ma czasu wyjścia, korzystaj z setInterval() do aktualizacji czasu pracy
              if (!wyjscie) {
                setInterval(() => {
                  const start_time = godzina_wejscia;
                  const end_time = moment();
                  const duration = moment.duration(end_time.diff(start_time));
                  const hours = duration.hours();
                  const minutes = duration.minutes();
                  const seconds = duration.seconds();
                  const czasString = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
                  note.worktime = czasString;
                }, 1000);
              } else { // Jeśli jest czas wyjścia, oblicz czas pracy normalnie
                const duration = moment.duration(godzina_wyjscia.diff(godzina_wejscia));
                const hours = duration.hours();
                const minutes = duration.minutes();
                const seconds = duration.seconds();
                const czasString = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
                note.worktime = czasString;
              }
            }
          })
          .catch(error => {
            console.error('Wystąpił błąd podczas pobierania statusu:', error);
          });
    },

    changeStatus(id){
      axios.post("https://localhost:7285/api/ToDoApp/ChangeStatus?id="+id).then(
          (response)=>{
            alert(response.data);
            this.showTable();
          }

      )
    },
    addUrlop(od_kiedy,do_kiedy){
      axios.post("https://localhost:7285/api/ToDoApp/AddUrlop?id="+this.selectedUserId+"&od_kiedy="+od_kiedy+"&do_kiedy="+do_kiedy).then(
          (response)=>{
            alert(response.data);
            console.log(this.selectedUserId);

          }

      )
    },
    logout(){
      googleLogout()
      this.loggedIn = false
    },

    verify(email){
      axios.get("https://localhost:7285/api/ToDoApp/Verify?email="+email).then(
          (response)=>{
            alert(response.data);
          }

      )
    }
  },mounted:function() {
    this.showTable()
  }
}
</script>
<template>
  <div id="tabela-container" >
    <table class="tabelacroll">
      <thead class="rounded-header">
      <tr>
        <th>Pracownik</th>
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
        <td>{{ note.Imie }} {{ note.Nazwisko }}</td>
        <td><a @click="changeStatus(note.Id)">{{ note.status }}</a></td>
        <td>{{ note.worktime }}</td>
        <td>{{ note.wejscie }}</td>
        <td>{{ note.wyjscie }}</td>
        <td><img v-if="note.status === 'wejscie'" style="width: 2vw" src="@/assets/ikony/stop-button.png">
          <img v-else-if="note.status === 'wyjscie'" style="width: 2vw" src="@/assets/ikony/briefcase.png"></td>
        <td>
          <div class="blur" v-if="formularzPokazany" style="position: fixed; right: 1px;bottom: 1px"></div>
          <div class="urlop-formularz" v-if="formularzPokazany">
            <form class="formurlop" @submit.prevent="addUrlop(selectedDateStart,selectedDateEnd)">
              <label>Ile masz dni urlopu: {{20-duration}}</label><br>
              <label>Wybierz urlop</label><br>
              <input type="date" id="poczatek_urlop" v-model="selectedDateStart" @input="calculateDuration">
              <input type="date" id="koniec_urlopu" v-model="selectedDateEnd" @input="calculateDuration"><br>
              <label>{{selectedDateStart}}</label>
              <label>{{selectedDateEnd}}</label><br>
              <input class="submitbutton" type="submit" value="Enjoy your holiday!" >
              <input class="closebutton" type="button" value="Zamknij" @click="pokazFormularz(note.Id)">
            </form>
          </div>
          <a @click="pokazFormularz(note.Id)">
            <img src="@/assets/ikony/calendar.png" style="width: 3vw">
          </a>
        </td>
      </tr>
      </tbody>
    </table>
  </div>

</template>

<style scoped>


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
tbody tr:first-child{
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

#tabela-container::-webkit-scrollbar-button:vertical{
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
.urlop-formularz {
  background-color: #101936;
  border-radius: 10px;
  border: 1px solid white;
  margin-top: 10%;
  color: white;
  padding: 10px;
}
.submitbutton, .closebutton{
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

</style>
