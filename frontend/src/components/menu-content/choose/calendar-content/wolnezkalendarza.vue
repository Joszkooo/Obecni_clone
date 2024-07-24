<template>
  <div>
    <button @click="handleAuthClick">Authorize Google Calendar</button>
    <div v-if="events.length">
      <h2>Today's Events</h2>
      <ul>
        <li v-for="event in events" :key="event.id">
          {{ event.summary }} - {{ event.start.dateTime }} to {{ event.end.dateTime }}
        </li>
      </ul>
    </div>
    <div>
      <h2>Add Event</h2>
      <form @submit.prevent="addEvent">
        <label>
          Summary:
          <input v-model="newEvent.summary" required />
        </label>
        <label>
          Start DateTime:
          <input type="datetime-local" v-model="newEvent.startDateTime" required />
        </label>
        <label>
          End DateTime:
          <input type="datetime-local" v-model="newEvent.endDateTime" required />
        </label>
        <button type="submit">Add Event</button>
      </form>
    </div>
    <button @click="listUpcomingEvents">Refresh Events</button>
  </div>
</template>

<script>
import { gapi } from 'gapi-script';

export default {
  data() {
    return {
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
    };
  },
  mounted() {
    this.loadGapi();
  },
  methods: {
    loadGapi() {
      console.log('Loading GAPI client...');
      gapi.load('client:auth2', () => {
        console.log('GAPI client loaded.');
        this.initClient();
      });
    },
    initClient() {
      console.log('Initializing GAPI client...');
      gapi.client.init({
        apiKey: this.apiKey,
        clientId: this.clientId,
        discoveryDocs: this.discoveryDocs,
        scope: this.scopes,
      }).then(() => {
        this.gapiLoaded = true;
        console.log('GAPI client initialized.');
      }).catch(error => {
        console.error('Error during GAPI client init: ', error);
      });
    },
    handleAuthClick() {
      if (this.gapiLoaded) {
        console.log('Attempting to sign in...');
        gapi.auth2.getAuthInstance().signIn().then(() => {
          console.log('User signed in.');
          this.listUpcomingEvents();
        }).catch(error => {
          console.error('Error during sign in: ', error);
        });
      } else {
        console.error('GAPI client not loaded.');
      }
    },
    listUpcomingEvents() {
      const today = new Date().toISOString().split('T')[0];
      const startOfDay = new Date(today).toISOString();
      const endOfDay = new Date(today + 'T23:59:59').toISOString();

      gapi.client.calendar.events.list({
        calendarId: this.calendarId,
        timeMin: startOfDay,
        timeMax: endOfDay,
        showDeleted: false,
        singleEvents: true,
        orderBy: 'startTime',
      }).then(response => {
        this.events = response.result.items;
      }).catch(error => {
        console.error('Error listing events: ', error);
      });
    },
    addEvent() {
      const authInstance = gapi.auth2.getAuthInstance();
      if (!authInstance.isSignedIn.get()) {
        console.log('User not signed in. Signing in...');
        authInstance.signIn().then(() => {
          this.createEvent();
        }).catch(error => {
          console.error('Error during sign in: ', error);
        });
      } else {
        this.createEvent();
      }
    },
    createEvent() {
      const event = {
        summary: this.newEvent.summary,
        start: {
          dateTime: new Date(this.newEvent.startDateTime).toISOString(),
          timeZone: 'UTC',
        },
        end: {
          dateTime: new Date(this.newEvent.endDateTime).toISOString(),
          timeZone: 'UTC',
        },
      };

      gapi.client.calendar.events.insert({
        calendarId: this.calendarId,
        resource: event,
      }).then(response => {
        console.log('Event created: ', response);
        this.listUpcomingEvents(); // Refresh the event list
        this.newEvent.summary = '';
        this.newEvent.startDateTime = '';
        this.newEvent.endDateTime = '';
      }).catch(error => {
        console.error('Error creating event: ', error);
      });
    }
  }
};
</script>
