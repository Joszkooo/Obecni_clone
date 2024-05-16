<script setup>
import Notifications from "@/components/notAndHD/notificationsApp.vue";
import HelpDesk from "@/components/notAndHD/HelpDesk.vue";
import { ref, onMounted } from 'vue';
import { getUser } from '@/userService';

const user = ref(null);

const savedUser = localStorage.getItem('user');
if (savedUser) {
  user.value = JSON.parse(savedUser);
}

onMounted(async () => {
  if (!user.value) {
    user.value = await getUser();
    localStorage.setItem('user', JSON.stringify(user.value));
  }
});
</script>
<script>
export default {
}

</script>

<template>
  <div v-if="user" id="notifications">
    <Notifications></Notifications>
    <help-desk></help-desk>
  </div>
</template>

<style scoped>
#notifications {
  height: 200px;
  width: 100%;
  float: right;
  display: flex;
  margin-bottom: 2.5%;
}
</style>