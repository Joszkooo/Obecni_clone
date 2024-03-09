
<template>
  <div id="tittleApp">
    <h1>{{ titlee }}</h1>
  </div>
</template>

<script>
export default {
  props: {
    titlee: String
  }
}
</script>

<style scoped>
#tittleApp {
  font-size: 1.5vw;
  text-align: center;
  font-family: Arial;
  color: white;
}
#tittleApp::first-letter {
  color: cornflowerblue;
}

@media screen and (max-width: 768px) {
  #tittleApp {
    font-size: 4vw;
    border-bottom: 0.5px solid rgba(255, 255, 255, 0.3);
  }
}
</style>
