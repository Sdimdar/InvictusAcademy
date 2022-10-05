import { createRouter, createWebHistory } from "vue-router";
import { VueCookieNext } from "vue-cookie-next";
import routes from "./routes";

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requireAuth)) {
    next({ name: "account-signin", query: { next: to.fullPath } });
  } else {
    next();
  }
});

export default router;
