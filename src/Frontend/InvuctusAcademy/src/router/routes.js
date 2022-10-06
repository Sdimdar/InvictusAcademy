const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      { path: "/", name: 'homepage', component: () => import("pages/IndexPage.vue") },
      { path: "/user", component: () => import("pages/UserInfoPage.vue") }
    ],
  },
  {
    path: "/admin-panel",
    component: () => import("layouts/AdminPanelLayout.vue"),
    children: [
      { path: "", component: () => import("pages/UsersInfoPage.vue") }
    ],
  },
  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/ErrorNotFound.vue"),
  },
];

export default routes;
