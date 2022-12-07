<template>
  <div :class="$attrs.class">

      <div class="course-logo" style="color:#F9F9F9;">
        <span class="course-logo-first-subtitle">Онлайн курс:</span>
        <span style="font-size: 22px; display: block;  width: 204px; font-weight: bold;">{{ courseName }}</span>
      </div>

      <div class="description-elem">
        <span class="gray-sub-title" style="margin-bottom:8px">Автор курса</span>
        <span class="description-elem-text">Имя автора курса</span>
      </div>

      <div class="description-elem">
        <span class="gray-sub-title" style="margin-bottom:8px">Тип курса</span>
        <span class="description-elem-text">Самостоятельный</span>
      </div>

      <div class="description-elem">
        <span class="gray-sub-title" style="margin-bottom:8px">Сертификат</span>
        <span class="description-elem-text">Нет</span>
      </div>

      <div class="description-elem">
        <span class="gray-sub-title" style="margin-bottom:8px">Время прохождения</span>
        <span class="description-elem-text">Вечность</span>
      </div>

      <div class="description-elem">
        <div class="gray-sub-title" style="margin-bottom:8px; display: flex; justify-content: space-between;">
          <span>Прогресс:</span> 
          <span class="description-elem-text">
            {{(Math.ceil(progress*100))}} %
          </span>
        </div>
        <q-linear-progress rounded size="24px" color="pink" :value="progress">
          <div class="absolute-full flex flex-center">
            <span style="color:white; font-size: 12px;">{{progressLabel}} </span>
          </div>
        </q-linear-progress>
      </div>

      <div>
        <p class="card-text">Вы остановились на:</p>
        <p class="card-text blue-text">
          Модуль "{{nextLearningModule.title}}" - 
          {{nextLearningArticle.order}} урок - 
          "{{nextLearningArticle.title}}"
        </p>
        <div @click="goToArticlePage(nextLearningModule.id, nextLearningArticle.order)" class="button">Продолжить обучение</div>
      </div>
    </div>
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
    },
    progressLabel() {
      return `${this.moduleCompletedCount}\\${this.moduleCount} модулей`
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
.course-logo{
  padding: 49px 16px;
  background: linear-gradient(102.29deg, #E5B1B1 0%, #D76969 100%);
  box-shadow: 0px 4px 33px rgba(0, 0, 0, 0.12);
  border-radius: 12px;
  margin-bottom: 20px;
}

.course-logo-first-subtitle{
  font-family: 'Montserrat';
  font-style: normal;
  margin-bottom: 10px; 
  display: block; 
  font-size: 22px; 
  width: 204px; 
  font-weight: 300;
}

.course-title{
  font-size: 32px;
  font-weight: 700;
  margin-bottom: 32px;
}

.description-elem{
  margin-bottom: 20px;
}

.description-elem-text{
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  color: #242424;
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
  font-weight: 400;
}

.button{
  padding: 16px 32px;
  width: 100%;
  /* gap: 10px;
  width: 332px;
  height: 51px; */
  background: #CD3838;
  box-shadow: 0px 4px 22px rgba(177, 20, 20, 0.12);
  border-radius: 10px;
  border-color: #CD3838;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  text-align: center;
  color: #F9F9F9;
}
</style>