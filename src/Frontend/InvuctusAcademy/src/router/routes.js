const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      { path: "/", name: 'homepage', component: () => import("pages/IndexPage.vue") },
      { path: "/user", component: () => import("pages/User/UserInfoPage.vue") },
      { path: "/user/courses", name: 'user-courses', component: () => import("pages/User/UserCoursesPage.vue") },
      { path: "/user/AllCoursesPage", name: 'user-allcoursespage', component: () => import("pages/User/AllCoursesPage.vue") },
      { path: "/user/ShowCourseModules/:id", name: 'course-modules', component: () => import("pages/User/ShowCourseModules.vue") }
      ],
  },
  {
    path: "/admin-panel",
    component: () => import("layouts/AdminPanelLayout.vue"),
    children: [
      { path: "showAllUser", component: () => import("pages/Admin/UsersInfoPage.vue") },
      { path: "requests", component: () => import("pages/Admin/RequestsInfoPage") }
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
