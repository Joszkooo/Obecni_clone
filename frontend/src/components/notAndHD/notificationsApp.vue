<script>
import axios from "axios";

export default {
  data() {
    return{
      free_info: '',
    }
  },
  methods: {
    showowlne() {
      axios.get("https://localhost:7285/api/ToDoApp/ShowWolne").then(
          (response)=> {
            this.free_info = response.data.map(item => item.nazwa+' '+item.kiedy).join('\n');
          }
      )
    }
  },
  mounted() {
    this.showowlne()
  }
}

</script>

<template>
  <div id="noti">
    <h1>Powiadomienia</h1>
    <h2>Najbliższe wolne ustawowo</h2>
    <pre>
      <h3>{{this.free_info}}</h3>
    </pre>
  </div>
</template>

<style scoped>
#noti {
  height: 13vw;
  width: 70%;
  background-color: #101936;
  border-radius: 15px;
  margin-left: 2.5%;
  margin-top: 1%;

}
h1 {
  font-family: "Open sans";
  margin-left: 2%;
  color: white;
  font-weight: lighter;
  font-size: 2vw;

}
h2 {
  font-family: "Open sans";
  margin-left: 2%;
  color: white;
  font-weight: lighter;
  font-size: 1.5vw;
  margin-bottom: 0;

}
h3 {
  font-family: "Open sans";
  margin-left: 2%;
  color: white;
  font-weight: lighter;
  font-size: 1vw;
  margin-top: 0;
}
p {
  font-family: "Open sans";
  font-size: 1vw;
  color: white;
  font-weight: 100;
  margin-left: 2%;
}
pre {
  margin: 0;
}
</style>