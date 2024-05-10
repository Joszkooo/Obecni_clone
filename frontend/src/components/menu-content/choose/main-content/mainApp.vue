<script setup>
</script>
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
    <router-link class="router-link" :to="{ name: 'tabela' }"><h1><img src="../../../../assets/ikony/home_color.png" id="ikona1">&nbsp;{{ name }}</h1>
    </router-link>
  </div>
</template>

<style scoped>
#stronaglowna {
  align-items: center;
}
#ikona1 {
  height: 1.3vw;
  width: 1.3vw;
}
h1 {
  text-decoration: none;
  color: white;
  font-weight: 100;
  font-family: "Open sans";
  font-size: 1.5vw;
  text-underline: none;
}
.router-link {
  text-decoration: none; /* Usunięcie podkreślenia */
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