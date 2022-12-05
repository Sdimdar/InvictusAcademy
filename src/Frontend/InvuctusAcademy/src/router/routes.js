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
      { path: "/user/courseDetails", component: () => import("pages/Courses/CourseDetailsPage.vue") },
      { path: "/user/ShowFullCourseModules/:id", name: 'course-fullModules', component: () => import("pages/Courses/ShowFullCourseModules.vue") },
      { path: "/freeArticle/AllFreeArticles", name: 'allFreeArticles', component: () => import("pages/FreeArticles/AllFreeArticles.vue") },
      { path: "/freeArticle/AboutFreeArticle/:id", name: 'aboutFreeArticles', component: () => import("pages/FreeArticles/AboutFreeArticle.vue") },
      { path: "/course/:id", name: 'purchasedCourseData', component: () => import("pages/CoursePassing/PurchasedCourse.vue") },
      { path: "/course/article", name: 'purchasedArticleData', component: () => import("pages/CoursePassing/PurchasedArticle.vue") },
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
