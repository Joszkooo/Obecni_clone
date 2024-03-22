<template>
  <div>
    <h1  id="welcome">Witaj, {{ user.name }}!</h1>
    <div id="emailphoto">
      <h1  id="email">{{ user.email }}</h1>
      <img id="photo" :src="user.picture"/>
    </div>
  </div>
</template>

<style scoped>
#photo {
  float: right;
  margin-right: 2%;
  border-radius: 50%;
  width: 2vw;
  height: 2vw;
  margin-top: 3%;
}
#welcome {
  float: left;
  font-family: "Open sans";
  color: white;
  margin-left: 4%;
  margin-top: 3%;
  font-weight: 100;
  font-size: 2.1vw;
}

#email {
  float: right;
  margin-top: 3%;
  margin-right: 3%;
  font-size: 1.2vw;
  color: white;
  font-family: "Open sans";
  font-weight: 100;
}
</style>

<script setup>
import { getUser } from '@/userService';
const user = getUser();
</script>