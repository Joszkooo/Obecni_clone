<template>
  <nav-bar></nav-bar>
  <div id="loginpage">
    <div v-if="loggedIn" class="user-info">

    </div>
    <div v-else>
    <h1 id="login">Zaloguj się</h1>
    <form class="loginform">
      <div>
  <!--      &lt;!&ndash;    <label>E-mail<span>*</span>:</label>&ndash;&gt;-->
        <input class="email-input" type="email" required v-model="email" placeholder="E-mail" style="background-position-x:20px ">
      </div>
      <div>
          <!--    <label>Haslo:</label>-->
        <input class="password-input" type="password" required v-model="email" placeholder="Haslo" style="background-position-x:20px ">
      </div>
      <div style="display: flex; justify-content: center">
        <input type="checkbox" >
        <label id="rememberpassword">Zapamiętaj haslo</label>
      </div>

  <!--    <input type="checkbox" value="lsRememberMe" id="rememberMe"> <label class="" for="rememberMe">asdds</label>-->
      <router-link to="/base"><button id="buttonCos">Zaloguj się</button></router-link>
      <GoogleLogin :callback="callback" @click="handleAuthClick"  class="google-login"/>
    </form>
    </div>
  </div>
</template>
<script>

import NavBar from "@/components/bar/navBar.vue";
import {decodeCredential, GoogleLogin, googleLogout} from "vue3-google-login";
import {setUser} from "@/userService.js";

export default {
  components: {GoogleLogin, NavBar},
  data() {
    return {
      email: '',
      password: '',
      loggedIn: false,
      user: null,
      gapiLoaded: false,
      clientId: '261479002576-vvtpb4ctt25gtd6rtlhgfsi72nuj4ipv.apps.googleusercontent.com', // Podaj swój Client ID
      apiKey: 'AIzaSyC7S4P4TH1BdF2blBb9Jz3IaQl8cvTd-p8', // Podaj swój API Key
      discoveryDocs: ["https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest"],
      scopes: 'https://www.googleapis.com/auth/calendar',
    };
  },
  methods: {
    callback(response) {
      console.log("Zalogowano");
      this.loggedIn = true;
      console.log(response);
      this.user = decodeCredential(response.credential);
      setUser(this.user);
      this.$router.push('/base');
    },
    logout() {
      googleLogout();
      this.loggedIn = false;
    },
    loadGapi() {
      gapi.load('client:auth2', this.initClient);
    },
    initClient() {
      gapi.client.init({
        apiKey: this.apiKey,
        clientId: this.clientId,
        discoveryDocs: this.discoveryDocs,
        scope: this.scopes,
      }).then(() => {
        this.gapiLoaded = true;
        console.log('GAPI client initialized.');
      }).catch(error => {
        console.error('Error during gapi client init: ', error);
      });
    },
    handleAuthClick() {
      if (this.gapiLoaded) {
        gapi.auth2.getAuthInstance().signIn().then(() => {
          console.log('User signed in.');
          // Przykładowe wywołanie funkcji po zalogowaniu, np. listowanie nadchodzących wydarzeń
          // this.listUpcomingEvents();
        }).catch(error => {
          console.error('Error during sign in: ', error);
        });
      } else {
        console.error('GAPI client not loaded.');
      }
    },
  },
  mounted() {
    // Ładuje GAPI po zamontowaniu komponentu
    this.loadGapi();
  },

};
</script>

<style>
#loginpage {
  margin-top: 3%;
}
  input[type="checkbox"] {
    display: inline-block;
    width: 16px;
    margin: 0 16px 0 0;
    position: relative;
    top: 2px;
    background-color: #eee;
  }
  #login {
    margin: 30px auto;
    max-width: 420px;
    text-align: center;
    font-size: 1.875rem;
    line-height: 2.25rem;
    font-weight: 300;
    font-family: "Open sans";
    color: white;
  }




  .loginform {
    max-width: 420px;
    margin: 30px auto;
    background-color: RGB(15, 23, 42);
    text-align: left;
    padding: 40px;
    border-radius: 10px;
    border: 0.5px solid rgba(255,255,255,0.3);
  }
  #rememberpassword {
    color: #aaa;
    display: inline-block;
    margin: 25px 0 15px;
    font-size: 0.9em;
    font-family: "Open sans";
  }


  .email-input {
    background-image: url('@/assets/ikony/arroba.png');
    background-size: 20px; /* Dostosuj rozmiar obrazka */
    background-position: left center;
    background-repeat: no-repeat;
    display: block;
    padding: 15px 6px;
    width: 100%;
    box-sizing: border-box;
    border-radius: 10px;
    border: 1px solid #ddd;
    background-color: RGB(15, 23, 42);
    color: #555;
    margin: 25px 0 15px;
    padding-left: 44px;
  }
  .password-input {
    background-image: url('@/assets/ikony/padlock.png');
    background-size: 20px; /* Dostosuj rozmiar obrazka */
    background-position: left center;
    background-repeat: no-repeat;
    display: block;
    padding: 15px 6px;
    padding-left: 40px;
    width: 100%;
    box-sizing: border-box;
    border-radius: 10px;
    border: 1px solid #ddd;
    background-color: RGB(15, 23, 42);
    color: #555;
    margin: 25px 0 15px;

  }
  #buttonCos {
    background-color: #a78bfa;
    text-align: center;
    border-style: solid;
    border-radius: 10px;
    padding: 15px 6px;
    width: 45%;
    font-weight: 600;
    transition-duration: 200ms;
  }


  .google-login {
    float: right;
    background-color: #ffffff;
    border-style: solid;
    border-radius: 10px;
    padding: 3px;
    font-weight: 600;
    box-sizing: border-box;
    display: flex;
    justify-content: center;
  }

</style>