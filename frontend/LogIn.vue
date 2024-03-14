<template>
  <div>

  </div>
  <h1>Zaloguj się</h1>

  <form>
    <div>
<!--      &lt;!&ndash;    <label>E-mail<span>*</span>:</label>&ndash;&gt;-->
      <input class="email-input" type="email" required v-model="email" placeholder="E-mail">
    </div>
    <div>
        <!--    <label>Haslo:</label>-->
      <input class="password-input" type="password" required v-model="email" placeholder="Haslo">
    </div>
    <div>
      <input type="checkbox">
      <label>Zapamiętaj haslo</label>
    </div>

<!--    <input type="checkbox" value="lsRememberMe" id="rememberMe"> <label class="" for="rememberMe">asdds</label>-->
    <button>Zaloguj się</button>
  </form>
</template>
<script>
export default {
  data() {
    return {
      email: '',
      password: '',
    }
  }

}
</script>

<!--mb-12 font-light-->


<style>
  input[type="checkbox"] {
    display: inline-block;
    width: 16px;
    margin: 0 16px 0 0;
    position: relative;
    top: 2px;
    background-color: #eee;
  }
  h1 {
    margin: 30px auto;
    max-width: 420px;
    text-align: center;
    font-size: 1.875rem;
    line-height: 2.25rem;
    font-weight: 300;
    font-family: "Arial Narrow";
    color: white;
  }

  form {
    max-width: 420px;
    margin: 30px auto;
    background-color: RGB(15, 23, 42);
    text-align: left;
    padding: 40px;
    border-radius: 10px;
    //border: 1px solid #ddd;
    border: 0.5px solid rgba(255,255,255,0.3);
    border-color: #64748b;
  }
  label {
    color: #aaa;
    display: inline-block;
    margin: 25px 0 15px;
    font-size: 0.9em;
    //letter-spacing: 1px;
    //font-weight: bold;
    font-family: "Arial Narrow";
  }
  input {
    display: block;
    padding: 15px 6px;
    width: 100%;
    box-sizing: border-box;
    border-radius: 10px;
    border: 1px solid #ddd;
    background-color: RGB(15, 23, 42);
    border-color: #64748b;
    color: #555;
    border-radius: 10px;
    border-style: solid;
    border-color: #64748b;
    margin: 25px 0 15px;
  }

  .email-input {
    background-position-x: 10px;
    background-image: url('arroba.png');
    background-size: 20px; /* Dostosuj rozmiar obrazka */
    background-position: left center;
    background-repeat: no-repeat;
    padding-left: 30px; /* Dodaj odstęp od lewej strony dla tekstu */
    position: ;

  }
  .password-input {
    background-image: url('padlock.png');
    background-size: 20px; /* Dostosuj rozmiar obrazka */
    background-position: left center;
    background-repeat: no-repeat;
    padding-left: 30px; /* Dodaj odstęp od lewej strony dla tekstu */

  }
  button {
    margin: 30px auto;
    background-color: #a78bfa;
    text-align: center;
    border-style: solid;
    margin: 25px 0 15px;
    border-radius: 10px;
    padding: 15px 6px;
    width: 100%;
    font-weight: 600;
    transition-duration: 200ms;
    flex: 1 1 0%;
  }
  button.b {
    margin: 30px auto;
    background-color: #a78bfa;
    text-align: center;
    border-style: solid;
    margin: 25px 0 15px;

  }
</style>