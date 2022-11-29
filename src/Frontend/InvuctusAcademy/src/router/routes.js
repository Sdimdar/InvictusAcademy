const routes = [
  {
    path: "/",
    component: () => import("src/layouts/Layout.vue"),
    children: [
      { path: "/", name: 'homepage', component: () => import("pages/IndexPage.vue") },
      { path: "/user", component: () => import("pages/User/UserInfoPage.vue") },
      { path: "/user/courses", name: 'user-courses', component: () => import("pages/Courses/UserCoursesPage.vue") },
      { path: "/user/AllCoursesPage", name: 'user-allcoursespage', component: () => import("pages/Courses/AllCoursesPage.vue") },
      { path: "/user/ShowCourseModules/:id", name: 'course-modules', component: () => import("pages/Courses/ShowCourseModules.vue") },
      { path: "/user/courseDetails", component: () => import("src/pages/Courses/CourseDetailsPage.vue") },
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
