<template>
  <div v-if="user">
    <h1 id="welcome">Witaj, {{ user.name }}!</h1>
    <div id="emailphoto">
      <h1 id="email">{{ user.email }}</h1>
      <img id="photo" :src="user.picture"/>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { getUser } from '@/userService';

const user = ref(null);

// Sprawdź, czy dane użytkownika są już w localStorage i ustaw je, jeśli istnieją
const savedUser = localStorage.getItem('user');
if (savedUser) {
  user.value = JSON.parse(savedUser);
}

onMounted(async () => {
  // Pobierz użytkownika tylko wtedy, gdy nie jest już zapisany w localStorage
  if (!user.value) {
    user.value = await getUser();
    // Zapisz użytkownika do localStorage
    localStorage.setItem('user', JSON.stringify(user.value));
  }
});
</script>

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
