<script setup>
</script>

<template>
  <div id="nav">
    <h1 id="title">ePracownik</h1>
  </div>
</template>

<style scoped>
#nav {
  height: 4em;
  width: 100%;
  margin: 0;
  border-bottom: 0.5px solid rgba(255, 255, 255, 0.3);
}
#title {
  float: left;
  font-size: 2em; /* Set a default font size */
  font-family: Jura;
  color: white;
  text-decoration: none; /* Changed 'text-underline' to 'text-decoration' */
  margin-left: 1%;
  margin-top: 1%;
  height: 100%; /* Ensure title takes full width */
}
#title::first-letter {
  color: #6B72FF;
}
</style>
