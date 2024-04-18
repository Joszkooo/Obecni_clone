import { createApp } from 'vue'
import App from './App.vue'
import router from '@/router/index'; // Importuje Vue Router
import vue3GoogleLogin from 'vue3-google-login'
const CLIENT_ID = "985809653496-p55gnpsln3q0rsp01elpjotnq9sm70st.apps.googleusercontent.com"
createApp(App)
    .use(router)
    .use(vue3GoogleLogin,{
        clientId: CLIENT_ID
    })
    .mount('#app');

