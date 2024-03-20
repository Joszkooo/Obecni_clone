<script setup>
import Notifications from "@/components/notAndHD/notificationsApp.vue";
import HelpDesk from "@/components/notAndHD/HelpDesk.vue";
</script>
<script>
export default {
}

</script>

<template>
  <div id="notifications">
    <Notifications></Notifications>
    <help-desk></help-desk>
  </div>
</template>

<style scoped>
#notifications {
  height: 10%;
  width: 100%;
  float: right;
  display: flex;
}
</style>