<template>
  <div id="app">
    <div id="middle">
      <up-bar-logged></up-bar-logged>
      <NotificationsAndHDApp/>
    </div>
    <MenuApp/>
  </div>
</template>

<script setup>
import MenuApp from '@/components/MenuApp.vue';
import UpBarLogged from "@/components/bar/upBarLogged.vue";
import NotificationsAndHDApp from "@/components/notificationsAndHDApp.vue";

</script>

<style>
#middle {
  width: 80%;
  height: 100%;
  float: right;
}
body {
  margin: 0px;
  background-color: #191F35;

}
body::after {
  content: '';
  position: absolute;
  top: 0;
  bottom: 0;
  left: 20%;
  border-left: 0.5px solid rgba(255, 255, 255, 0.3);
}
@media screen and (max-width: 768px) {
  body::after {
    border-left: none;
  }
}
#app {
  margin: 0px;
}
</style>