const employeeRouter = {
  path: "/employee",
  name: "employee",
  component: () => import("@/views/employee/Index"),
  children: [
    {
      // employee/list
      path: "list",
      name: "employee.list",
      component: () => import("@/views/employee/EmployeeList"),
      children: [
        {
          // employee/list/form
          path: "form",
          name: "employee.list.form",
          component: () => import("@/views/employee/EmployeeForm"),
        },
      ],
    },
  ],
};

export default employeeRouter;
