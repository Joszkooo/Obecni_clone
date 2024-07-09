import { createRouter, createWebHistory } from 'vue-router';

// Import komponentów, które będą używane w trasach
import tabela from "@/components/menu-content/choose/main-content/tabelaPrivate.vue";
import LogIn from "@/views/LoginView.vue";
import PrivateView from "@/views/PrivateView.vue";
import PublicView from "@/views/PublicView.vue";
import urlop from "@/components/menu-content/choose/calendar-content/urlopApp.vue"
import wolnezkalendarza from "@/components/menu-content/choose/calendar-content/wolnezkalendarza.vue";
const routes = [
    {
        path: '/',
        name: 'Public',
        component: PublicView,
    },
    {
        path: '/base',
        name: 'base',
        component: PrivateView,
        children: [
            {
                path: 'tabela',
                name: 'tabela',
                component: tabela
            },
            {
                path: 'urlop',
                name: 'urlop',
                component: urlop
            },
            {
                path: 'wolnezkalendarza',
                name: 'wolnezkalendarza',
                component: wolnezkalendarza
            },
        ]
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
