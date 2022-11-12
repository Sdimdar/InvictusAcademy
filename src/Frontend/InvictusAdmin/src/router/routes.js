const routes = [
  {
    path: "/admin-panel",
    component: () => import("layouts/AdminLayout.vue"),
    children: [
      { path: "showAllUser", component: () => import("pages/Admin/UsersInfoPage.vue") },
      { path: "requests", component: () => import("pages/Admin/RequestsInfoPage.vue") },
      { path: "createAdmin", component: () => import("pages/Admin/CreateAdminPage.vue") },
      { path: "content", component: () => import("src/pages/Courses/ContentPage.vue") },
      { path: "moduleDetails/:id", component: () => import("src/pages/Courses/ModuleDetailsPage.vue") },
      { path: "showAllModules", component: () => import("pages/Content/ModulesInfoPage.vue") },
      { path: "showAllArticules", component: () => import("pages/Content/ArticulesInfoPage.vue") },
      { path: "editCourse/:id", component: () => import("pages/Admin/EditCourse.vue") },
      { path: "createCourse", component: () => import("pages/Admin/CreateCourse.vue") },
      { path: "allCourses", component: () => import("pages/Admin/AllCourses.vue") },
    ],
  },
  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/Error/ErrorNotFound.vue"),
  },
];

export default routes;

