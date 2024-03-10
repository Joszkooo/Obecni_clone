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
    <h1><img src="../../assets/ikony/home_color.png" id="ikona1"><a href="#">&nbsp;{{ name }}</a></h1>
  </div>
</template>

<style scoped>
#ikona1 {
  height: 1.3vw;
  width: 1.3vw;
}
a {
  text-decoration: none;
  color: white;
  font-weight: 100;
  font-family: "Open sans";
  font-size: 1.5vw;
}
@media screen and (max-width: 768px) {
  a {
    font-size: 7vw;
  }
  #ikona1 {
    height: 6vw;
    width: 6vw;
  }
}
</style>