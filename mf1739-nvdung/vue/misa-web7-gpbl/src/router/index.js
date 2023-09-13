import { createRouter, createWebHistory } from 'vue-router'

import EmployeeList from '../views/employee/employeeList/EmployeeList.vue'
import CustomerList from '../views/customer/customerList/CustomerList.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/EmployeeList',
      name: 'EmployeeRouter',
      component: EmployeeList,
    },
    {
      path: '/CustomerList',
      name: 'CustomerRouter',
      component: CustomerList,
    },

    
    // {
    //   path: '/about',
    //   name: 'about',
    //   // component: () => import('../views/AboutView.vue')
    // }
  ]
})

export default router
