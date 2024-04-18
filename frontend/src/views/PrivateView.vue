<script setup>
import MenuApp from "@/components/menu-content/MenuApp.vue"
import UpBarLogged from "@/components/bar/upBarLogged.vue";
import NotificationsAndHDApp from "@/components/notAndHD/notificationsAndHDApp.vue";
</script>
<script>
export default {
computed: {
  isLoginPage() {
    return this.$route.path === '/base/urlop';
  },
}
}
</script>

<template>
  <div id="private">
    <div id="middle">
      <UpBarLogged></UpBarLogged>
      <notifications-and-h-d-app v-if="!isLoginPage"></notifications-and-h-d-app>
      <router-view></router-view>
    </div>
    <MenuApp></MenuApp>

  </div>
</template>

<style scoped>
#middle {
  display: block;
}
</style>