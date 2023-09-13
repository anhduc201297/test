import { createRouter, createWebHistory } from "vue-router";
import StaffComponent from "@/views/staff/StaffList.vue";
import DepositComponent from "@/views/deposit/Index.vue";
import SaleComponent from "@/views/sale/Index.vue";
import HomeComponent from "@/views/Home.vue";


const routers = [
    {  
        path:'/staff', name: 'StaffRouter', component:StaffComponent
    }, 
    { 
        path:'/deposit', name: 'DepositRouter', component:DepositComponent
    }, 
    { 
        path:'/sale', name: 'SaleRouter', component:SaleComponent
    }, 
    { 
        path:'/', name: 'HomeRouter', component:HomeComponent
    }, 
    
]
const router = createRouter({
    history: createWebHistory(),
    routes: routers,
})

export default router;