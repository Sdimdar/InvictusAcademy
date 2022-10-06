const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      { path: "/", name: 'homepage', component: () => import("pages/IndexPage.vue") },
      { path: "/user", component: () => import("pages/User/UserInfoPage.vue") }
    ],
  },
  {
    path: "/admin-panel",
    component: () => import("layouts/AdminPanelLayout.vue"),
    children: [
      { path: "/admin-panel/showAllUser", component: () => import("pages/Admin/UsersInfoPage.vue") },
      { path: "/admin-panel/requests", component: () => import("pages/Admin/RequestsInfoPage") }
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
