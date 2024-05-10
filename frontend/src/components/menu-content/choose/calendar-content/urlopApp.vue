<template>
  <div id="app">
    <div id="urlop">
      <div id="rightleft" v-if="isMonthlyView">
        <button id="previousMonth" @click="previousMonth"> <img id="leftarrow" src="@/assets/ikony/left-arrow.png"> </button>
        <span>{{ monthNames[currentMonth] }}-</span>
        <span>{{ currentYear }}</span>
        <button id="nextMonth" @click="nextMonth"><img id="rightarrow" src="@/assets/ikony/right-arrow.png"> </button>
      </div>
      <div>
        <input id="addNewEventTitle" type="text" v-model="newEvent" placeholder="Dodaj nowe wydarzenie">
        <input id="NewEventDate" type="date" v-model="selectedDate">
        <button @click="addEvent">Dodaj</button>
        <div class="color-picker">
        </div>
      </div>
      <div v-if="isMonthlyView" class="calendar">
        <div class="daysOfWeek">
          <div v-for="(day, index) in computedDaysOfWeek" :key="index" class="dayOfWeek">{{ day }}</div>
        </div>
        <div v-for="(week, index) in weeks" :key="index" class="week">
          <div v-for="day in week" :key="day.date" class="day">
            <div v-if="day.date">{{ day.date }}</div>
            <div v-for="(event, eventId) in day.events" :key="eventId" class="event">
              <div>{{ event.summary }}</div> <!-- Poprawiono wyświetlanie nazwy wydarzenia -->
              <button @click="removeEvent(day.fullDate, eventId)">Usuń</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { gapi } from 'gapi-script';

export default {
  components: {
  },
  data() {
    return {
      isMonthlyView: true,
      newEvent: '',
      selectedDate: '',
      daysOfWeek: ['Poniedziałek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota', 'Niedziela'],
      events: [],
      currentYear: null,
      currentMonth: null,
      monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień']
    }
  },
  mounted() {
    this.setCurrentDate();
    this.initGoogleAPI();
  },
  methods: {
    initGoogleAPI() {
      gapi.load('client:auth2', () => {
        gapi.client.init({
          apiKey: 'AIzaSyAtzhKh_QUR1JpPY5RAignmGd-dp4kxrWk',
          clientId: '261479002576-f0i7fvh46sf28l0v4h5v0emfsfqjn78n.apps.googleusercontent.com',
          discoveryDocs: ['https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest'],
          scope: 'https://www.googleapis.com/auth/calendar.events'
        }).then(() => {
          // Autoryzacja użytkownika po załadowaniu klienta
          return gapi.auth2.getAuthInstance().signIn();
        }).catch(error => {
          console.error('Wystąpił błąd podczas inicjalizacji Google API Client:', error);
        });
      });
    },
    setCurrentDate() {
      const currentDate = new Date();
      this.currentYear = currentDate.getFullYear();
      this.currentMonth = currentDate.getMonth();
      this.generateCalendar();
    },
    previousMonth() {
      this.currentMonth--;
      if (this.currentMonth < 0) {
        this.currentMonth = 11;
        this.currentYear--;
      }
      this.generateCalendar();
    },
    nextMonth() {
      this.currentMonth++;
      if (this.currentMonth > 11) {
        this.currentMonth = 0;
        this.currentYear++;
      }
      this.generateCalendar();
    },
    async removeEvent(fullDate, eventId) {
      try {
        const response = await gapi.client.calendar.events.delete({
          calendarId: '59a6ad313c7f550c6797e8a37a562d234918fbf16ba1f3a12b0d1b8935585c0a@group.calendar.google.com\n',
          eventId: eventId
        });

        if (response.status === 204) {
          alert('Wydarzenie zostało usunięte!');
          this.generateCalendar();
        } else {
          alert('Wystąpił problem podczas usuwania wydarzenia.');
        }
      } catch (error) {
        console.error('Wystąpił błąd:', error);
        alert('Wystąpił problem podczas usuwania wydarzenia.');
      }
    },
    async addEvent() {
      try {
        let selectedDate = new Date(this.selectedDate);
        if (!selectedDate || isNaN(selectedDate.getTime())) {
          alert("Please select a valid date.");
          return;
        }

        const startDateTime = new Date(selectedDate.setHours(0, 0, 0, 0)).toISOString();
        const endDateTime = new Date(selectedDate.setHours(23, 59, 59, 999)).toISOString();

        const response = await gapi.client.calendar.events.insert({
          calendarId: '59a6ad313c7f550c6797e8a37a562d234918fbf16ba1f3a12b0d1b8935585c0a@group.calendar.google.com\n',
          resource: {
            summary: this.newEvent,
            start: {dateTime: startDateTime},
            end: {dateTime: endDateTime}
          }
        });

        if (response.status === 200) {
          alert('Wydarzenie zostało dodane!');
          this.newEvent = '';
          this.selectedDate = '';
          this.generateCalendar();
        } else {
          alert('Wystąpił problem podczas dodawania wydarzenia.');
        }
      } catch (error) {
        console.error('Wystąpił błąd:', error);
        alert('Wystąpił problem podczas dodawania wydarzenia.');
      }
    },
    generateCalendar() {
      const firstDay = new Date(this.currentYear, this.currentMonth, 1).getDay();
      const daysInMonth = new Date(this.currentYear, this.currentMonth + 1, 0).getDate();
      const weeks = [];
      let currentWeek = [];
      let dayCounter = 1;

      // Poprawka na pierwszy dzień miesiąca
      let startDay = firstDay === 0 ? 6 : firstDay - 1; // Poniedziałek - 0, Wtorek - 1, ..., Niedziela - 6
      for (let i = 0; i < startDay; i++) {
        currentWeek.push({
          date: null, // Dzień miesiąca nie istnieje dla tych dni
          fullDate: null, // Dodajemy pole pełnej daty
          events: []
        });
      }

      for (let i = startDay; i < 7; i++) {
        const fullDate = `${this.currentYear}-${this.currentMonth + 1 < 10 ? '0' + (this.currentMonth + 1) : this.currentMonth + 1}-${dayCounter < 10 ? '0' + dayCounter : dayCounter}`;
        currentWeek.push({
          date: dayCounter,
          fullDate: fullDate, // Dodajemy pełną datę dla każdego dnia
          events: this.events[fullDate] || [] // Przypisanie eventów do danego dnia
        });
        dayCounter++;
      }

      weeks.push(currentWeek);

      while (dayCounter <= daysInMonth) {
        currentWeek = [];
        for (let i = 0; i < 7 && dayCounter <= daysInMonth; i++) {
          const fullDate = `${this.currentYear}-${this.currentMonth + 1 < 10 ? '0' + (this.currentMonth + 1) : this.currentMonth + 1}-${dayCounter < 10 ? '0' + dayCounter : dayCounter}`;
          currentWeek.push({
            date: dayCounter,
            fullDate: fullDate, // Dodajemy pełną datę dla każdego dnia
            events: this.events[fullDate] || [] // Przypisanie eventów do danego dnia
          });
          dayCounter++;
        }
        weeks.push(currentWeek);
      }
      this.weeks = weeks;
    }
  },
  computed: {
    computedDaysOfWeek() {
      const firstDayIndex = this.daysOfWeek.indexOf('Poniedziałek');
      const daysBeforeFirstDay = this.daysOfWeek.slice(firstDayIndex);
      const daysAfterFirstDay = this.daysOfWeek.slice(0, firstDayIndex);
      return [...daysBeforeFirstDay, ...daysAfterFirstDay];
    }
  }
}
</script>

<style>
#rightleft {
  margin-top: 5%;
  margin-left: 45%;
  color: white;
}
#previousMonth {
  background: #0f172a;
  text-decoration: none;
  border: none;
  border-radius: 50%;
  padding: 1%;
  color: white;
}
#nextMonth
{  background: #0f172a;
  text-decoration: none;
  border: none;
  border-radius: 50%;
  padding: 1%;
  color: white;
}

#rightarrow {
  width: 1vw;
  height: 1vw;
}
#leftarrow {
  width: 1vw;
  height: 1vw;
}
#addNewEventTitle {
  background: white;
  width: 15%;
}
#NewEventDate {
  background: white;
  width: 15%;

}
.dayOfWeek:first-child {
  border-top-left-radius: 20px;
  border-bottom-left-radius: 20px;
  margin-left: 10%;
}
.dayOfWeek:last-child {
  border-top-right-radius: 20px;
  border-bottom-right-radius: 20px;
  margin-right: 10%;
}
#urlop {
  margin-top: 10%;
  background: #212B4E;
  border-radius: 20px;
  font-family: "Open sans";
  color: white;
}
.calendar {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  margin-left: 3%;
  margin-right: 3%;
}

.daysOfWeek {
  display: contents;
  grid-template-columns: repeat(7, 1fr);
  font-weight: bold;
}

.dayOfWeek {
  padding: 10px;
  background: #101936;
}

.week {
  display: contents;
  grid-template-columns: repeat(7, 1fr);
}

.day {
  padding: 10px;
  border: 1px solid #ccc;
}

.event {
  background-color: yellow;
  margin-bottom: 2px;
  padding: 2px;
}
.event .day {
  background-color: red;
}
</style>
