import { createApp } from 'vue'
import App from './App.vue'
import router from '@/router/index'; // Importuje Vue Router
import vue3GoogleLogin from 'vue3-google-login'
const CLIENT_ID = "261479002576-f0i7fvh46sf28l0v4h5v0emfsfqjn78n.apps.googleusercontent.com"
createApp(App)
    .use(router)
    .use(vue3GoogleLogin,{
        clientId: CLIENT_ID
    })
    .mount('#app');

