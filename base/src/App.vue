<template>
  <div id="app">
    <MenuApp/>
  </div>
</template>

<script setup>
import MenuApp from './components/MenuApp.vue';
</script>

<style>
body {
  margin: 0px;
  background-color: RGB(15, 23, 42);
  border-right: 0.5px solid rgba(255, 255, 255, 0.3);
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