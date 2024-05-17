<template>
  <Qalendar

      :selected-date="selectedDate"
      :events="events"
      :config="config"
      @date-was-clicked="handleDateClick"
      @event-was-clicked="handleEventClick"
  />
  <!-- Formularz dodawania nowego wydarzenia -->
  <div class="centered-box" v-if="isAddingEvent">
    <input type="text" v-model="newEventTitle" placeholder="Tytuł wydarzenia">
    <button class="add-event-button" @click="addEvent">Dodaj wydarzenie</button>
  </div>

</template>

<script>
import { Qalendar } from "qalendar";

export default {
  components: {
    Qalendar,
  },

  data() {
    return {
      selectedDate: new Date(), // Domyślnie wybrana data
      events: [], // Lista wydarzeń
      config: {
        // Konfiguracja Qalendar
      },
      isAddingEvent: false, // Flaga określająca, czy użytkownik dodaje nowe wydarzenie
      newEventTitle: "", // Tytuł nowego wydarzenia
      editingEventId: null, // Add this line

    };
  },

  methods: {
    handleDateClick(date) {
      // Funkcja wywoływana po kliknięciu na kalendarz
      this.isAddingEvent = true;
      this.selectedDate = date; // Zapisz wybraną datę
      this.editingEventId = null; // Reset the editing event ID

    },
    handleEventClick(event) {
      this.isAddingEvent = true;
      this.newEventTitle = event.title;
      this.editingEventId = event.id; // Save the ID of the event being edited
    },

    addEvent() {
      if (this.newEventTitle.trim() === "") {
        alert("Proszę podać tytuł wydarzenia.");
        return;
      }

      const eventId = `event-${Date.now()}-${Math.floor(Math.random() * 1000)}`;

      this.events.push({
        title: this.newEventTitle,
        time: { start: this.selectedDate, end: this.selectedDate }, // Wydarzenie trwa tylko jeden dzień
        color: 'yellow',
        isEditable: true,
        id: eventId,
      });

      this.isAddingEvent = false;
      this.newEventTitle = "";
      this.editingEventId = null; // Reset the editing event ID


    },
  }
};
</script>

<style>
@import "style.css";

.form-container {
  //display: flex;
  //justify-content: center;
  //align-items: center;
  //position: fixed;
  //top: 0;
  //left: 0;
  //width: 100%;
  //height: 100%;
  //background: rgba(0, 0, 0, 0.5); /* Optional: semi-transparent background */
}
.centered-box {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  position: fixed;
  bottom: 300px;
  left: 0%;
  width: 20%;
  height: 180px;
  border-radius: 15px;
  background: rgba(0, 0, 0, 0.5); /* Optional: semi-transparent background */
}
.centered-box input[type="text"] {
  display: block;
  padding: 15px 6px;
  width: calc(100% - 20px); /* Subtract 10px from both sides */
  margin: 0 10px; /* Add 10px margin to both sides */
  box-sizing: border-box;
  border-radius: 10px;
  border: 1px solid #ddd;
  background-color: RGB(15, 23, 42);
  border-color: #64748b;
  color: #555;
  border-radius: 10px;
  //border-style: solid;
  //border-color: #64748b;
  margin: 15px;
}

.add-event-button {
  background-color: #a78bfa;
  text-align: center;
  border-style: solid;
  border-radius: 10px;
  padding: 15px 6px;
  width: calc(100% - 20px); /* Make the button the same width as the input field */
  margin: 0 10px; /* Add 10px margin to both sides */
  font-weight: 600;
  transition-duration: 200ms;
  margin: 15px;
  font-family: "Open sans";
  color: white;
  font-size: 1.5vw;
  font-weight: lighter;
  cursor: pointer;
  text-decoration: none;
}
</style>