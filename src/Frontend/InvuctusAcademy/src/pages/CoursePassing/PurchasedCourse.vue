<template>
  <q-page-container>
    <div class="flex-style">
      <component :is="courseCard" v-if="courseCard" class="card-style" @goToArticlePageEvent="goToArticlePage"
        :courseName="courseData.name"
        :nextLearningModule="courseData.nextLearingModule"
        :nextLearningArticle="courseData.nextLearningArticle"
        :moduleCount="courseData.modules.length"
        :moduleCompletedCount="courseData.completedModulesCount"
      />
      <component  class="card-style" :is="modulesList" v-if="modulesList" :modules="courseData.modules" @goToArticlePageEvent="goToArticlePage"/>
    </div>
  </q-page-container>

</template>

<script>
import { defineComponent } from "vue";
import { getPurchasedCourseData } from "boot/axios";
import notify from "boot/notifyes";
import PurchasedCourseCard from "src/components/Courses/PurchasedCourseCard.vue"
import ModuleListCard from "src/components/Courses/ModuleListCard.vue"

export default defineComponent({
  name: "PurchasedCourse",
  components: {
    PurchasedCourseCard,
    ModuleListCard
  },
  beforeMount() {
    this.updateData();
  },
  data() {
    return {
      id: this.$route.params.id,
      courseData: {},
      courseCard: null,
      modulesList: null
    }
  },
  methods: {
    async updateData() {
      try {
        const response = await getPurchasedCourseData(this.id);
        console.log(response.data);
        if (response.data.isSuccess) {
          this.courseData = response.data.value;
          this.courseCard = PurchasedCourseCard
          this.modulesList = ModuleListCard
        }
        else if (response.data.errors.length != 0) {
          response.data.errors.forEach(element => {
            notify.showErrorNotify(element)
          });
        }
        else if (response.data.validationErrors.length != 0) {
          response.data.validationErrors.forEach(element => {
            notify.showErrorNotify(element)
          });
        }
      } catch (error) {
        console.log(error.message);
      }
    },
    goToArticlePage(moduleId, articleOrder) {
      this.$router.push({ path: '/course/article', query: { moduleId: moduleId, articleOrder: articleOrder, courseId:this.id } })
    }
  }
})
</script>

<style scoped>
.flex-style{
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
}

.card-style{
  box-shadow: 0px 4px 33px rgba(0, 0, 0, 0.08);
  border-radius: 10px;
  width: 540px;
  margin-bottom: 150px;
}
</style>
