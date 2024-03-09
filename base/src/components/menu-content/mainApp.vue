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
    <h1><img src="../../assets/ikona1.png" id="ikona1"><a href="#">{{ name }}</a></h1>
  </div>
</template>

<style scoped>
#ikona1 {
  height: 2vw;
  width: 2vw;
}
a {
  text-decoration: none;
  color: white;
  font-weight: 100;
}
@media screen and (max-width: 768px) {
  #stronaglowna {
    font-size: 4vw;
  }
  #ikona1 {
    height: 6vw;
    width: 6vw;
  }
}
</style>