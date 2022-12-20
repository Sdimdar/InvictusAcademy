<template>
  <q-page-container>

    <div v-if="(currentCourses.length != 0)" class="col" style="font-size: 32px; font-weight: 700; color: #000000;">
        Мои курсы
    <div class="row">
          <course-card class="list-card" v-for="course in currentCourses" :data="course" />
    </div>
  </div>

  <div v-if="(wishedCourses.length != 0)" class="col" style="font-size: 32px; font-weight: 700; color: #000000;">
    Избранное
    <div class="row">
          <course-card class="list-card" v-for="course in wishedCourses" :data="course" />
    </div>
  </div>

  <div v-if="(newCourses.length != 0)"  class="col" style="font-size: 32px; font-weight: 700; color: #000000;">
    Новые курсы
    <div class="row">
          <course-card class="list-card" v-for="course in newCourses" :data="course" />
    </div>
  </div>

  </q-page-container>

</template>

<script>
import { ref } from "vue";
import {
  getCurrentCourses,
  getCompletedCourses,
  getWishedCourses,
  getNewCourses,
} from "boot/axios";
import CourseCard from "components/Courses/CourseCard.vue";

export default {
  components: { CourseCard },
  data() {
    return {
      currentCourses: [],
      completedCourses: [],
      wishedCourses:[],
      newCourses: [],
    };
  },
  mounted() {
    this.getCoursesData();
  },
  methods: {
    async getCoursesData() {
      try {
        const response = await getCurrentCourses();
        if (response.data.isSuccess) {
          this.currentCourses = response.data.value.courses;
          console.log(response.data);
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
};
</script>

<style scoped>
.my-courses {
  margin-left: 20%;
}
</style>
