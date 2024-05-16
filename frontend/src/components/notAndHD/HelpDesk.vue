<script>
import axios from "axios";
import moment from "moment";

export default {
  data() {
    return {
      hd_date: moment().format('YYYY-MM-DD'),
      hd_info: '',
    }
  },
  methods: {

    gethd(date) {
      axios.get("https://localhost:7285/api/ToDoApp/GetHD?data="+date)
          .then(response => {
            this.hd_info = response.data.map(item => item.imie+' '+item.nazwisko).join('\n');

          })
    }
  },
  mounted() {
    this.gethd(this.hd_date)
  }
}
</script>

<template>
  <div id="hd">
    <h1>HD</h1>
    <h2>{{this.hd_date}}</h2>
    <h3><pre>{{this.hd_info}}</pre></h3>
  </div>
</template>

<style scoped>
  #hd {
    height: 13vw;
    width: 30%;
    background-color: #101936;
    border-radius: 15px;
    margin-top: 1%;
    margin-right: 2.5%;
    margin-left:3%;
  }
  h1 {
    font-family: "Open sans";
    margin-left: 5%;
    color: white;
    font-weight: lighter;
    font-size: 2vw;
  }
  h2 {
    font-family: "Open sans";
    margin-left: 5%;
    color: white;
    font-weight: lighter;
    font-size: 1.5vw;
  }
  h3 {
    font-family: "Open sans";
    margin-left: 5%;
    color: white;
    font-weight: lighter;
    font-size: 1vw;
  }
</style>