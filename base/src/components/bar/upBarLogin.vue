<script>
export default {}
</script>

<template>
 <div id="up-bar">
   <h1 id="welcome">Witaj Korzystasz z trybu publicznego</h1>
   <button>Zaloguj się</button>
 </div>
</template>

<style scoped>
#welcome {
  float: left;
  font-family: "Arial Narrow";
  color: gray;
  margin-left: 20px;
  margin-top: 40px;
}
#up-bar {

  height: 100px;
  width: 80%;
  float: right;
  justify-content: flex-end;
}
button {
  background-color: #6B72FF; /* Green */
  border: none;
  color: white;
  padding: 15px 32px;
  text-align: center;
  font-size: 16px;
  margin: 40px 40px;
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