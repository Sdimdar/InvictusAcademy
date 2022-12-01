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
      { path: "editCourse/:id", component: () => import("pages/Courses/EditCourse.vue") },
      { path: "createCourse", component: () => import("pages/Courses/CreateCourse.vue") },
      { path: "allCourses", component: () => import("pages/Courses/AllCourses.vue") },
      { path: "paymentRequests", component: () => import("src/pages/Payment/PaymentRequestPage.vue")},
      { path: "confirmPayments", component: () => import("src/pages/Payment/ConfirmPaymentPage.vue")},
      { path: "rejectRequests", component: () => import("src/pages/Payment/RejectRequests.vue")},
      {path: "historyPage", component:() => import("src/pages/Payment/HistoryPage.vue")}
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

