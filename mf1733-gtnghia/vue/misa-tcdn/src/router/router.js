import { createRouter, createWebHistory } from "vue-router";
import CustomerIndex from "@/views/customer/Index.vue";
import CustomerList from "@/views/customer/CustomerList.vue";
import SystemIndex from "@/views/system/Index.vue";
import HomePage from "@/views/Home.vue";
import ReportPage from "@/views/report/ReportPage";
import employeeRouter from "./employeeRouter";
// Define routers
const routes = [
  {
    path: "/customer",
    name: "customer",
    component: CustomerIndex,
    children: [
      {
        // customer/list
        path: "list",
        name: "customer.list",
        component: CustomerList,
        children: [{
          // customer/list/form
          path: "form",
          name: "customer.list.form",
          component: () => import("@/views/customer/CustomerForm"),
        }],
      },
    ],
  },
  {
    path: "/cs",
    redirect: "customer",
  },
  {
    path: "/report",
    name: "report",
    component: ReportPage,
    children: [
      {
        path: "1",
        name: "report.child",
        components: {
          top: () => import("@/views/report/ReportDetail.vue"),
          bottom: () => import("@/views/report/ReportDetailId.vue"),
        },
      },
    ],
  },
  {
    path: "/system",
    name: "system",
    component: SystemIndex,
  },
  employeeRouter,
  {
    path: "/",
    name: "HomeRouter",
    component: HomePage,
  },
];
// Initialization router
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
