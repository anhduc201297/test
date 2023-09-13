import { createRouter, createWebHistory } from "vue-router";
// import App from "@/App.vue";
import CustomerList from "../views/customer/CustomerList.vue";
import CustomerDetail from "../views/customer/CustomerDetail.vue";
import ErrorPage from "../views/error/Error.vue";
// import IndexEmployee from "../views/employee/IndexEmployee.vue";
import EmployeeList from "../views/employee/EmployeeList.vue";
import EmployeeDetail from "../views/employee/EmployeeDetail.vue";
import Home from "../views/home/Home.vue";
import MForm from "../components/base/mform/MForm.vue";
import MNotificationDialog from "../components/base/mdialog/MNotificationDialog.vue";
const routes = [
  {
    name: "HomeView",
    path: "/",
    component: Home,
  },
  {
    name: "EmployeeList",
    path: "/employees",
    component: EmployeeList,
    children: [
      {
        // name: "EmployeeDetail",
        path: "form/:id",
        component: EmployeeDetail,
        name: "Update One Employee",
      },
      {
        path: "form",
        component: EmployeeDetail,
        name: "Add New Employee",
      },
    ],
  },
  {
    path: "/customers",
    component: CustomerList,
    name: "CustomerList",
    children: [
      {
        path: "form",
        component: CustomerDetail,
        name: "Customer Detail Form",
      },
      {
        path: "form/:id",
        component: CustomerDetail,
        name: "Customer Detail Change Form",
      },
    ],
  },
  {
    path: "/mform",
    component: MForm,
    name: "M Form",
  },
  {
    path: "/notifidialog",
    component: MNotificationDialog,
    name: "MNotificationDialog",
  },
  {
    path: "/:pathMatch(.*)*",
    component: ErrorPage,
    name: "Page not found",
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
