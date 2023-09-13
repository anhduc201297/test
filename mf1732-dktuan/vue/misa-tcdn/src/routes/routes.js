import EmpoyeeList from '@/views/employee/EmployeeList.vue'
import CashIndex from '@/views/cash/CashIndex.vue'
// import EmployeeForm from '@/views/employee/EmployeeForm.vue'
import {createRouter, createWebHistory} from 'vue-router'
const routes = [
    {
        path: "/", name: "HomeRoute", component: EmpoyeeList
    },
    {
        path: "/employee", name: "EmployeeRoute", component: EmpoyeeList
    },
    {
        path: "/cash", name: "CashRoute", component: CashIndex
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
})


export default router