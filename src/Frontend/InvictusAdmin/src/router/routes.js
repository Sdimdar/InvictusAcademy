const routes = [
  {
    path: "/admin-panel",
    component: () => import("layouts/AdminLayout.vue"),
    children: [
      { path: "showAllUser", component: () => import("pages/Admin/UsersInfoPage.vue") },
      { path: "requests", component: () => import("pages/Admin/RequestsInfoPage.vue") },
      { path: "createAdmin", component: () => import("pages/Admin/CreateAdminPage.vue") },
      { path: "content", component: () => import("src/layouts/ContentPageLayout.vue") },
      { path: "createCourse", component: () => import("pages/Admin/CreateCoursePage.vue") },
      { path: "showAllModules", component: () => import("pages/Content/ModulesInfoPage.vue") },
      { path: "showAllArticules", component: () => import("pages/Content/ArticulesInfoPage.vue") }
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

