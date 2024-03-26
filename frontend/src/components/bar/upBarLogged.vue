<script>
import { getUser } from '@/userService';
export default {
  data () {
    return {
      user: null,
      name: "Miłosz"
    };
  },
  created() {
    this.user = getUser();
  }
}
</script>

<template>
  <div id="up-bar">
    <h1 id="welcome">Witaj, {{user.name}}!</h1>
    <h1 id="email">{{user.email}}</h1>
  </div>
</template>

<style scoped>
#up-bar {
  height: 100px;
  width: 100%;
  float: right;
  justify-content: flex-end;
}
#email {
  float: right;
  margin-right: 3%;
  margin-top: 3%;
  font-size: 1.2vw;
  color: white;
  font-family: "Open sans";
  font-weight: 100;
}
#welcome {
  float: left;
  font-family: "Open sans";
  color: white;
  margin-left: 4%;
  margin-top: 3%;
  font-weight: 100;
  font-size: 2.1vw;
}

@media screen and (max-width: 768px) {

  #up-bar {
    float: none;
    justify-content: center;
    width: 100%;
  }

}
</style>