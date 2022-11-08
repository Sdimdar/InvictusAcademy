<template>
  <q-layout view="lHh Lpr lFf">
    <left-bar />
    <div class="my-courses">
      <h4 style="text-align: center">Ваши курсы</h4>

      <q-splitter
        v-model="splitterModel"
        style="height: 750px, margin-left: 100px"
      >
        <template v-slot:before>
          <q-tabs v-model="tab" vertical class="text-teal">
            <q-tab name="current" icon="mail" label="Текущие" />
            <q-tab name="completed" icon="alarm" label="Завершённые" />
            <q-tab name="wish" icon="movie" label="Желаемые" />
            <q-tab name="new" icon="movie" label="Новые" />
          </q-tabs>
        </template>

        <template v-slot:after>
          <q-tab-panels
            v-model="tab"
            animated
            swipeable
            vertical
            transition-prev="jump-up"
            transition-next="jump-up"
          >
            <q-tab-panel name="current">
              <div class="text-h4 q-mb-md">Текущие</div>
              <course-card
                class="list-card"
                v-for="course in currentCourses"
                :data="course"
              />
            </q-tab-panel>

            <q-tab-panel name="completed">
              <div class="text-h4 q-mb-md">Завершённые</div>
              <course-card
                class="list-card"
                v-for="course in completedCourses"
                :data="course"
              />
            </q-tab-panel>

            <q-tab-panel name="wish">
              <div class="text-h4 q-mb-md">Желаемые</div>
              <course-card
                class="list-card"
                v-for="course in wishedCourses"
                :data="course"
              />
            </q-tab-panel>

            <q-tab-panel name="new">
              <div class="text-h4 q-mb-md">Новые</div>
              <course-card
                class="list-card"
                v-for="course in newCourses"
                :data="course"
              />
            </q-tab-panel>
          </q-tab-panels>
        </template>
      </q-splitter>
    </div>
  </q-layout>
</template>

<script>
import { ref } from "vue";
import CourseCard from "components/CourseCard.vue";
import LeftBar from "components/LeftBar.vue";
import {
  getCurrentCourses,
  getCompletedCourses,
  getWishedCourses,
  getNewCourses,
} from "boot/axios";

export default {
  props: {
    logined: {
      Type: Boolean,
    },
  },
  setup() {
    return {
      tab: ref("current"),
      splitterModel: ref(20),
    };
  },
  mounted() {
    if (!this.logined) {
      this.$router.push({ name: "homepage" });
    }
    this.getCoursesData();
  },
  data() {
    return {
      currentCourses: [],
      completedCourses: [],
      wishedCourses: [],
      newCourses: [],
    };
  },
  methods: {
    async getCoursesData() {
      try {
        const response = await getCurrentCourses();
        if (response.data.isSuccess) {
          this.currentCourses = response.data.value.courses;
          console.log(response.data.value.courses);
        }
      } catch (error) {
        console.log(error.message);
      }

      try {
        const response = await getCompletedCourses();
        if (response.data.isSuccess) {
          this.completedCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }

      try {
        const response = await getWishedCourses();
        if (response.data.isSuccess) {
          this.wishedCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }

      try {
        const response = await getNewCourses();
        if (response.data.isSuccess) {
          this.newCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }
    },
  },
  components: { CourseCard, LeftBar },
};
</script>

<style scoped>
.my-courses {
  margin-left: 20%;
}
</style>