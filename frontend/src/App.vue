<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script setup>
</script>

<style>
#middle {
  width: 80%;
  height: 100%;
  float: right;
}
body::after {
  content: '';
  position: absolute;
  top: 0;
  bottom: 0;
  left: 20%;
  border-left: 0.5px solid rgba(255, 255, 255, 0.3);
}


body.login-page::after {
  border-left: none;
}

body {
  margin: 0px;
  background-color: #191F35;
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

<script>

export default {
  computed: {
    isLoginPage() {
      return this.$route.path === '/LogIn';
    },
    isHomePage() {
      return this.$route.path === '/base';
    }
  },
  watch: {
    $route(to) {
      if (to.path === '/LogIn') {
        document.body.classList.add('login-page');
      } else {
        document.body.classList.remove('login-page');
      }
    }
  },
  created() {

    if (this.$route.path === '/LogIn') {
      document.body.classList.add('login-page');
    }
  }
}
</script>
