<template>
  <q-card :class="$attrs.class">
      <q-card-section>
        <div class="course-title"> {{ courseName }} </div>
      </q-card-section>

      <q-card-section>
        <div class="progres-title">Прогресс:</div>
        <p class="card-text">
          {{moduleCompletedCount}}/{{moduleCount}} модулей
        </p>
        <q-linear-progress rounded size="24px" color="accent" :value="progress" />
      </q-card-section>

      <q-card-section>
        <p class="card-text">Вы остановились на:</p>
        <p class="card-text blue-text">
          Модуль "{{nextLearningModule.title}}" - 
          {{nextLearningArticle.order}} урок - 
          "{{nextLearningArticle.title}}"
        </p>
        <q-btn @click="goToArticlePage(nextLearningModule.id, nextLearningArticle.order)" outline style="color: #CD3838;" label="Продолжить обучение" />
      </q-card-section>
    </q-card>
</template>

<script>
import { defineComponent } from "vue";

export default defineComponent({
  name: "PurchasedCourseCard",
  props: {
    courseName: String,
    nextLearningModule: Object,
    nextLearningArticle: Object,
    moduleCount: Number,
    moduleCompletedCount: Number
  },
  computed:{
    progress() {
      return this.moduleCompletedCount / this.moduleCount
    }
  },
  methods:{
    goToArticlePage(moduleId, articleOrder)
    {
      this.$emit('goToArticlePageEvent', moduleId, articleOrder)
    }
  }
})
</script>

<style scoped>
.course-title{
  font-size: 32px;
  font-weight: 700;
  margin-bottom: 32px;
}

.progres-title{
  font-size: 22px;
  font-weight: 600;
  margin-bottom: 16px;
}

.card-text{
  font-size: 16px;
  font-weight: 300;
}

.blue-text{
  color: #0375DF;
}
</style>