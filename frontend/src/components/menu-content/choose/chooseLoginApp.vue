<template>
  <div id="chooseApp">
    <mainApp></mainApp>
  </div>
</template>

<style scoped>
#chooseApp {
  margin-top: 30%;
  color: white;
  align-items: center;
}
@media screen and (max-width: 768px) {
  #chooseApp {
    margin-top: 10%;
    font-size: 4vw;
  }
}

</style>
<script setup>
import mainApp from "@/components/menu-content/choose/main-content/mainApp.vue";


</script>
<script>
export default {

}
</script>