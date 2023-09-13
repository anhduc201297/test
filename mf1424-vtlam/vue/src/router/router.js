import { createRouter, createWebHistory } from "vue-router";
import EmployeeList from '../views/employee/EmployeeList.vue';
import CustomerList from '../views/customer/CustomerList.vue';
import ReportList from '../views/report/ReportList.vue';

const routes = [
    {
        path: "/employee", name:"EmployeeRouter",component: EmployeeList
    },
    {
        path: "/customer", name:"CustomerRouter",component: CustomerList
    },
    {
        path: "/report", name:"ReportRouter",component: ReportList
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router