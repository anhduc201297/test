import { createRouter, createWebHistory } from 'vue-router';

import EmployeeList from './pages/employee/employeeList/EmployeeList.vue';
import NotFound from './pages/notFound/NotFound.vue';

const routes = [
    { path: '/', name: 'EmployeeList', component: EmployeeList },

    { path: '/:pathMatch(.*)*', name: 'NotFound', component: NotFound }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

export default router;
