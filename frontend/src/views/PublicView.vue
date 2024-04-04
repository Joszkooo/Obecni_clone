<script setup>

import UpBarLogin from "@/components/bar/upBarLogin.vue";
import MenuLoginApp from "@/components/menu-content/MenuLoginApp.vue"
import TabelaPublic from "@/components/menu-content/choose/main-content/tabelaPublic.vue";

</script>

<template>
  <div id="private">
    <div id="middle">
    <upBarLogin></upBarLogin>
    <tabela-public></tabela-public>
    </div>
    <MenuLoginApp></MenuLoginApp>
  </div>
</template>

<style scoped>

</style>