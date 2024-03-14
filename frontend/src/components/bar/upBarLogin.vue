<script>
export default {}
</script>

<template>
 <div id="up-bar">
   <h1 id="welcome">Witaj Korzystasz z trybu publicznego</h1>
   <router-link to="/LogIn"><button>Zaloguj się</button></router-link>
 </div>
</template>

<style scoped>
#welcome {
  float: left;
  font-family: "Open sans";
  color: white;
  font-size: 2vw;
  margin-left: 2%;
  margin-top: 3%;
  font-weight: lighter;
}
#up-bar {

  height: 100px;
  width: 80%;
  float: right;
}
button {
  background-color: #6B72FF; /* Green */
  border: none;
  color: white;
  padding: 1% 2%;
  text-align: center;
  font-size: 1vw;
  margin: 3% 4%;
  cursor: pointer;
  border-radius: 1px;
  float: right;

}
@media screen and (max-width: 768px) {

  #up-bar {
    float: none;
    justify-content: center;
    width: 100%;
  }

}
</style>