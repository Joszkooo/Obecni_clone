<template>
  <div id="menuApp">
    <TittleApp :titlee="title"/>
    <chooseApp></chooseApp>
  </div>
</template>

<script setup>
import TittleApp from '@/components/menu-content/TiitleApp.vue';
import chooseApp from "./chooseApp.vue"

</script>

<script>
const title = "ePracownik";
</script>

<style>
#menuApp {
  text-align: center;
  width: 20%;
  height: 700px;
  display: inline-table;
}

@media screen and (max-width: 768px) {

  #menuApp {
    width: 100%;
    border-right: none;
    text-align: center;
  }

}
</style>
