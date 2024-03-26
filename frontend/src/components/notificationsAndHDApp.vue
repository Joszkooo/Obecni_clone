<script setup>
import Notifications from "@/components/notAndHD/notificationsApp.vue";
import HelpDesk from "@/components/notAndHD/HelpDesk.vue";
import CalendarTest from "@/components/notAndHD/CalendarTest.vue";
</script>
<script>
export default {
}

</script>

<template>
  <div id="notifications">
    <Notifications></Notifications>
    <help-desk></help-desk>
    <calendar-test></calendar-test>
  </div>
</template>

<style scoped>
#notifications {
  height: 10%;
  width: 100%;
  float: right;
  display: flex;
}
</style>