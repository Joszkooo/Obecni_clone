import { createRouter, createWebHistory } from 'vue-router';

// Import komponentów, które będą używane w trasach
import base from '@/components/AppBase.vue';
import mainApp from "@/components/menu-content/mainApp.vue";
import tabela from "@/components/menu-content/main-content/tabelaMain.vue";
import LogIn from "@/components/LogIn.vue";
const routes = [
    {
        path: '/base',
        name: 'base',
        component: base
    },
    {
        path: '/tabela',
        name: 'tabela',
        component: tabela
    },
    {
        path: '/mainApp',
        name: 'MainApp',
        component: mainApp
    },
    {
        path: '/LogIn',
        name: 'Login',
        component: LogIn
    },

];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;
