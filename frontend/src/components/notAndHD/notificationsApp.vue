<script>
import axios from "axios";
import {gapi} from "gapi-script";

export default {
  data() {
    return{
      free_info: '',
      events: [],
      calendars: [],
      selectedCalendarId: '59a6ad313c7f550c6797e8a37a562d234918fbf16ba1f3a12b0d1b8935585c0a@group.calendar.google.com', // Default to the primary calendar
      CLIENT_ID: '261479002576-f0i7fvh46sf28l0v4h5v0emfsfqjn78n.apps.googleusercontent.com',  // Replace with your actual client ID
      API_KEY: 'AIzaSyBwH1RWW670JDB8PYLQlbVogTNJ7XK_KUA',  // Replace with your actual API key
      DISCOVERY_DOCS: ["https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest"],
      SCOPES: "https://www.googleapis.com/auth/calendar.readonly"
    }
  },
  methods: {
    showowlne() {
      axios.get("https://localhost:7285/api/ToDoApp/ShowWolne").then(
          (response)=> {
            this.free_info = response.data.map(item => item.nazwa+' '+item.kiedy).join('\n');
          }
      )
    },
    handleClientLoad() {
      gapi.load('client:auth2', this.initClient);
    },
    initClient() {
      gapi.client.init({
        apiKey: this.API_KEY,
        clientId: this.CLIENT_ID,
        discoveryDocs: this.DISCOVERY_DOCS,
        scope: this.SCOPES,
      }).then(() => {
        console.log('GAPI client initialized.');
        this.listCalendars();
      }).catch((error) => {
        console.error('Error initializing GAPI client:', error);
      });
    },
    listCalendars() {
      gapi.client.calendar.calendarList.list().then((response) => {
        this.calendars = response.result.items;
        console.log('Calendars:', this.calendars);
      }).catch((error) => {
        console.error('Error listing calendars:', error);
      });
    },
    listUpcomingEvents() {
      const now = new Date();
      gapi.client.calendar.events.list({
        'calendarId': this.selectedCalendarId,
        'timeMin': now.toISOString(),
        'showDeleted': false,
        'singleEvents': true,
        'orderBy': 'startTime'
      }).then((response) => {
        const events = response.result.items;
        const activeEvents = events.filter((event) => {
          const eventStart = new Date(event.start.dateTime || event.start.date);
          const eventEnd = new Date(event.end.dateTime || event.end.date);
          return eventStart <= now && now <= eventEnd;
        });
        this.events = activeEvents;
        console.log('Active events:', this.events);
      }).catch((error) => {
        console.error('Error listing events:', error);
      });
    }
  },
  mounted() {
    this.handleClientLoad();
    this.showowlne()
  }
}
</script>
<template>
  <div id="noti">
    <h1>Powiadomienia</h1>
    <h2><a @click="listUpcomingEvents">Urlop</a></h2>
      <h3>
        <ul style="list-style: none; display: contents">
          <li v-for="event in events" :key="event.id">
            {{ event.summary }} ({{ new Date(event.start.dateTime || event.start.date).toLocaleString()}})-({{new Date(event.end.dateTime || event.end.date).toLocaleDateString()}})
          </li>
        </ul></h3>
  </div>
</template>

<style scoped>
#noti {
  height: 13vw;
  width: 70%;
  background-color: #101936;
  border-radius: 15px;
  margin-left: 2.5%;
  margin-top: 1%;

}
h1 {
  font-family: "Open sans";
  margin-left: 2%;
  color: white;
  font-weight: lighter;
  font-size: 2vw;

}
h2 {
  font-family: "Open sans";
  margin-left: 2%;
  color: white;
  font-weight: lighter;
  font-size: 1.5vw;
  margin-bottom: 0;

}
h3 {
  font-family: "Open sans";
  margin-left: 2%;
  color: white;
  font-weight: lighter;
  font-size: 1vw;
  margin-top: 0;
}
p {
  font-family: "Open sans";
  font-size: 1vw;
  color: white;
  font-weight: 100;
  margin-left: 2%;
}
pre {
  margin: 0;
}
</style>