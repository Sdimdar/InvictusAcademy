<template>
  <q-page-container>
    <h4 style="text-align: center">Все курсы</h4>
    <div class="row">
          <course-card class="list-card" v-for="course in currentCourses" :data="course" />
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
