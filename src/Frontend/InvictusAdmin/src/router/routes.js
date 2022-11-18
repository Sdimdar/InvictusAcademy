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
      { path: "article", component: () => import("src/pages/Courses/ArticleDetailsPage.vue") },
      { path: "test", component: () => import("src/pages/Courses/ArticleTestPage.vue") },
      { path: "editCourse/:id", component: () => import("pages/Courses/EditCourse.vue") },
      { path: "createCourse", component: () => import("pages/Courses/CreateCourse.vue") },
      { path: "allCourses", component: () => import("pages/Courses/AllCourses.vue") },
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

