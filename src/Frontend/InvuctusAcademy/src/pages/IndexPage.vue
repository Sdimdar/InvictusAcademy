<template>
  <q-page-container v-if="!logined">
    <request-button />
  </q-page-container>

  <q-page-container v-else class="column" style="height: 1200px; padding-bottom: 0px;">
      <div class="col"> БЛОК </div>
      <div class="col"> Мой прогресс </div>
      <div class="col">
        Идеально подойдут вам
          <div class="row">
            <course-card class="list-card" v-for="course in currentCourses" :data="course" />
          </div>
      </div>
      <div class="col"> Недавно просмотренные </div>
      <div class="col-2"> Читайте также </div>
  </q-page-container>

</template>

<script>
import { defineComponent } from 'vue'
import {
  getCurrentCourses,
  getCompletedCourses,
  getWishedCourses,
  getNewCourses,
} from "boot/axios";
import RequestButton from 'components/RequestButton.vue'
import CourseCard from "components/Courses/CourseCard.vue";

export default defineComponent({
  name: 'IndexPage',
  components: {
    RequestButton,
    CourseCard
  },
  props: {
    logined: {
      type: Boolean,
      required: true,
    },
  },
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
})
</script>
