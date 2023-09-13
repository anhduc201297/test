import { createRouter,createWebHistory } from "vue-router";
import HomePage from '@/view/HomePage.vue'
import EmployeeList from '@/view/employee/EmployeeList.vue'
/**
 * Định nghĩa các router
 * AUTHOR: DTTHANH (28/08/2023)
 */
const routers = [
    {
        path:"/", name:"HomeRouter",component:HomePage
    },
    {
        path:"/employee", name: "EmployeeRouter", component: EmployeeList
    }
]

const router = createRouter({
    history:createWebHistory(), 
    routes:routers
})

export default router;