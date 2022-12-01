<template>
  <q-page-container>
    <h4 style="text-align: center">Все курсы</h4>

    <q-splitter>
      <template v-slot:after>
        <q-tab-panels v-model="tab" animated swipeable vertical transition-prev="jump-up" transition-next="jump-up">
          <q-tab-panel name="new">
            <course-card class="list-card" v-for="course in newCourses" :data="course" />
          </q-tab-panel>
        </q-tab-panels>
      </template>
    </q-splitter>

  </q-page-container>

</template>

<script>
import { ref } from "vue";
import CourseCard from "components/Courses/CourseCardForNotLogined.vue";
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
      tab: ref("new"),
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
