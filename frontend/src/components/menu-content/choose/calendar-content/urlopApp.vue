<template>
  <!--  <div id="urlop">-->
  <!-- Example of adding the calendar component -->
  <div id="cal_">
<!--    <calendar-g-g/>-->
    <CalendarAppV1/>
  </div>

  <!--  </div>-->

</template>

<script>

import {defineComponent} from "vue";
import CalendarAppV1 from "@/components/menu-content/choose/calendar-content/CalendarAppV1.vue";
import CalendarGG from "@/components/menu-content/choose/calendar-content/CalendarGG.vue";

export default defineComponent({
  components: {CalendarGG, CalendarAppV1}
})
</script>


<style scoped>
#notifications {
  height: 200px;
  width: 100%;
  float: right;
  display: flex;
  margin-bottom: 2.5%;
}
#cal_ {
  display: flex;
  border-radius: 15px;
  border: none;
  //position: fixed;
  overflow-y: auto;
  bottom: 2.5%;
  //margin-left: 2%;
  //margin-right: 2%;
  margin: 2%;
  height: 600px;
  //max-height: 175px;
  width: 96%;
  border-radius: 15px;
  //margin-top: 2.5%;
  border: 0;

}
#urlop {
  height: 500px;
  border-radius: 15px;
  display: flex;
  margin-bottom: 2.5%;
  background: white;
  margin-right: 2.5%;
  margin-left:2.5%;
}

</style>