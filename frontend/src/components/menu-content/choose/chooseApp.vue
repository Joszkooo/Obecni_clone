<template>
  <div id="chooseApp">
      <mainApp></mainApp>
      <calendarApp></calendarApp>
      <SettingsApp></SettingsApp>
  </div>
</template>

<style scoped>
#chooseApp {
  margin-top: 30%;
  color: white;
  align-items: center;
}
@media screen and (max-width: 768px) {
  #chooseApp {
    margin-top: 10%;
    font-size: 4vw;
  }
}

</style>
<script setup>
import mainApp from "@/components/menu-content/choose/main-content/mainApp.vue";
import calendarApp from "@/components/menu-content/choose/calendar-content/calendarApp.vue";
import SettingsApp from "@/components/menu-content/choose/settings/settingsApp.vue";

</script>
<script>
export default {

}
</script>