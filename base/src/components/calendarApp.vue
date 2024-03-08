<template>
  <div>
    <a href="#" id="toggle-menu" @click="toggleMenu" class="button">Pokaż menu</a>
    <ul class="menu" :class="{ 'active': isMenuActive }">
      <li>Element 1</li>
      <li>Element 2</li>
      <li>Element 3</li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isMenuActive: false
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
.menu {
  display: none;
  list-style-type: none;
  padding: 0;
  margin: 0;

}

.menu.active {
  display: block;
}

.menu li {
  padding: 10px;
  cursor: pointer;
  font-size: 2vw;
}


.button {
  border: none;
  cursor: pointer;
  text-decoration: none;
  font-family: "Arial Narrow";
  color: gray;
  font-size: 2vw;
}


</style>
