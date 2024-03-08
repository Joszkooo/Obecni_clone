<script>
export default {
  data() {
    return {
      name: "Strona Główna"
    }
  }
}

</script>

<template>
  <div id="stronaglowna">
    <h1><img src="@/assets/ikona1.png" id="ikona1">{{ name }}</h1>
  </div>
</template>

<style scoped>
#ikona1 {
  height: 23px;
  width: 23px;
}
@media screen and (max-width: 768px) {
  #stronaglowna {
    font-size: 4vw;
  }
}
</style>