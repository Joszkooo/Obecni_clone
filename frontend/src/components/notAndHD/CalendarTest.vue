<script setup>

</script>

<template>
<!--  <div id="noti">-->
<!--  </div>-->
  <div id="calendar">
    <iframe id="cal_" src="https://calendar.google.com/calendar/embed"></iframe>
  </div>


</template>

<style scoped>
#calendar {
  position: fixed;
  display: flex;
  bottom: 2.5%;
  margin-left: 2%;
  margin-right: 2%;
  height: 60%;
  width: 76%;
  background-color: #101936;
  border-radius: 15px;
  margin-top: 2.5%;
  border: 0;
}
#cal_ {
  width: 100%;
  background: #191F35;
  border-radius: 15px;
  border: none;
}
h1 {
  font-family: "Open sans";
  margin-left: 2%;
  color: white;
  font-weight: lighter;
  font-size: 2vw;
}
p {
  font-family: "Open sans";
  font-size: 1vw;
  color: white;
  font-weight: 100;
  margin-left: 2%;
  margin-top: 5%;
}
</style>