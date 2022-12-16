<template>
  <div style="display: flex; justify-content: space-between;">
    <div v-if="(moduleData.order != 1)" class="previous-button" 
         @click="goToArticlePage(moduleData.moduleInfo.id, moduleData.order-1)"> 
      &lt Предыдущий урок
    </div>
    <div v-if="(moduleData.order == 1)" class="previous-button" 
         @click="goToCoursePage()"> 
      &lt Назад к курсу
    </div>
    <div v-if="!moduleData.isCompleted" class="next-button"
         @click="goToTest(moduleData.moduleInfo.id, moduleData.order)">
      Перейти к тесту >
    </div>
    <div v-if="(moduleData.isCompleted && !moduleData.isLastArticleInModule)" class="next-button"
         @click="goToArticlePage(moduleData.moduleInfo.id, moduleData.order+1)">
      Перейти к следующей статье >
    </div>
    <div v-if="(moduleData.isCompleted && moduleData.isLastArticleInModule)" class="next-button"
         @click="goToArticlePage(moduleData.moduleInfo.id+1, 1)">
      Перейти к следующему модулю >
    </div>  
  </div>

</template>

<script>
export default {
  props: {
    moduleData: Object
  },
  methods: {
    goToCoursePage()
    {
      this.$router.push({ path: `/course/${this.$route.query.courseId}`})
    },
    goToArticlePage(moduleId, articleOrder) {
      this.$router.push({ path: '/course/article', query: { moduleId: moduleId, articleOrder: articleOrder, courseId:this.$route.query.courseId } })
    },
    goToTest(moduleId, articleOrder) {
      this.$router.push({ path: '/course/test', query: { moduleId: moduleId, articleOrder: articleOrder, courseId:this.$route.query.courseId } })
    }
  }
}
</script>

<style scoped>
.previous-button{
  padding: 16px 32px;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  text-align: center;
  color: #CD3838;
  border: 1px solid #CD3838;
  border-radius: 10px;
}

.previous-button:hover{
  text-decoration: underline;
  cursor: pointer;
}

.next-button{
  background: #CD3838;
  border-radius: 10px;
  padding: 16px 32px;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  text-align: center;
  color: #F9F9F9;
}

.next-button:hover{
  text-decoration: underline;
  cursor: pointer;
}
</style>