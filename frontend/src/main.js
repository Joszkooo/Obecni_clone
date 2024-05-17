import { createApp } from 'vue'
import App from './App.vue'
import router from '@/router/index'; // Importuje Vue Router
import vue3GoogleLogin from 'vue3-google-login'
const CLIENT_ID = "644279063794-opj59scldem2oe2dn2l03dnrql8jaq9b.apps.googleusercontent.com"
createApp(App)
    .use(router)
    .use(vue3GoogleLogin,{
        clientId: CLIENT_ID
    })
    .mount('#app');

