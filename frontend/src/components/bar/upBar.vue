<template>
  <div id="up-bar">
    <upBarLogin v-show="user"/>
    <upBarLogged/>
  </div>
</template>

<script setup>

import { getUser } from '@/userService';
import UpBarLogged from "@/components/bar/upBarLogged.vue";
import upBarLogin from "@/components/bar/upBarLogin.vue"
const user = getUser();

</script>

<style scoped>
#up-bar {
  height: 100px;
  width: 80%;
  float: right;
  justify-content: flex-end;
}

@media screen and (max-width: 768px) {
  #up-bar {
    float: none;
    justify-content: center;
    width: 100%;
  }
}
</style>
