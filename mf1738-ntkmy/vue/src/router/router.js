import { createRouter, createWebHistory } from 'vue-router';
import EmployeeList from "../views/employee/EmployeeList.vue";

const routes = [
    {
        path: "/employee", name: "EmployeeListRouter", component: EmployeeList
    },
];

const vueRouter = createRouter({
    history: createWebHistory(),
    routes: routes
});

export default vueRouter;