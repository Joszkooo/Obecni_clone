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
  color: gray;
  font-size: 1vw;
}
@media screen and (max-width: 768px) {
  #chooseApp {
    margin-top: 0;
    font-size: 4vw;
  }
}

</style>
<script setup>
import mainApp from "./mainApp.vue";
import calendarApp from "./calendarApp.vue";
</script>
<script>
export default {

}

</script>