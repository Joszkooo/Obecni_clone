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

import { reactive, onMounted } from 'vue';
import { getUser } from '@/userService';

const user = reactive({
    name: '',
    email: '',
    picture: ''
});

// Funkcja do ustawiania użytkownika w sesji
const setUserSession = (userData) => {
    sessionStorage.setItem('user', JSON.stringify(userData));
};

export function saveAuthToken(token) {
    localStorage.setItem('authToken', token);
}

// Pobiera token z localStorage
export function getAuthToken() {
    return localStorage.getItem('authToken');
}

// Usuwa token z localStorage
export function clearAuthToken() {
    localStorage.removeItem('authToken');
}

// Funkcja do pobierania użytkownika z sesji
const getUserSession = () => {
    const userData = sessionStorage.getItem('user');
    return userData ? JSON.parse(userData) : null;
};

// Pobierz użytkownika przy montowaniu komponentu
onMounted(() => {
    const storedUser = getUserSession();
    if (storedUser) {
        Object.assign(user, storedUser);
    } else {
        const fetchedUser = getUser();
        Object.assign(user, fetchedUser);
        setUserSession(fetchedUser);
    }
});