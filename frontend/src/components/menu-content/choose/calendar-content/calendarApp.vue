<template>
  <div id="menuuu">
    <a href="#" id="toggle-menu" @click="toggleMenu" class="button"><img id="ikona2" src="../../../../assets/ikony/calendar_color.png"> {{isMenuActive ? 'Kalendarz' : 'Kalendarz v' }}</a>
    <ul class="menu" :class="{ 'active': isMenuActive }">
      <li><router-link class="urlop" :to="{ name: 'urlop' }">Urlop</router-link></li>
      <li>Wyjazd do klienta</li>
      <li>HD</li>
      <li>Fajrant</li>
    </ul>
  </div>
</template>

<script>

export default {
  data() {
    return {
      isMenuActive: false,
      cos: 'Kalendarz v'
    };
  },
  methods: {
    toggleMenu() {
      this.isMenuActive = !this.isMenuActive;

    }
  }
}
</script>

<style scoped>
.urlop {
  text-decoration: none;
  text-underline: none;
  color: white;
}
.menu {
  display: none;
  list-style-type: none;
  padding: 0;
}
#menuuu {
  height: 60vh;
  align-items: center;
}
#ikona2 {
  height: 1.3vw;
  width: 1.3vw;
}

.menu.active {
  display: block;
}

.menu li {
  cursor: pointer;
  font-size: 1.5vw;
  font-family: "Open sans";
  font-weight: lighter;
}


.button {
  border: none;
  cursor: pointer;
  text-decoration: none;
  font-family: "Open sans";
  color: white;
  font-size: 1.5vw;
  font-weight: lighter;
}

@media screen and (max-width: 768px) {
  #toggle-menu {
    margin-top: 0;
    font-size: 8vw;
  }
  .menu li {
    font-size: 8vw;
  }
  #ikona2 {
    height: 6vw;
    width: 6vw;
  }

}
</style>
