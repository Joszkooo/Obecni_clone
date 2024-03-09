<template>
  <div id="chooseApp">
    <mainApp>
    </mainApp>
    <calendarApp>
    </calendarApp>
  </div>
</template>

<style scoped>
#chooseApp {
  margin-top: 50%;
  color: white;
  font-size: 1vw;
}
@media screen and (max-width: 768px) {
  #chooseApp {
    margin-top: 10%;
    font-size: 4vw;
  }
}

</style>
<script setup>
import mainApp from "./menu-content/mainApp.vue";
import calendarApp from "./menu-content/calendarApp.vue";
</script>
<script>
export default {

}

</script>