<template>
  <q-page-container v-if="!isChecked">
    <div v-for="(question, questionIndex) in testData">
      <div v-if="(question.questionType == 0)">
        {{ (questionIndex + 1) }}. {{ question.testQuestion }} - единичный
        <div v-for="answer in question.testAnswers">
          <input type="radio" :id="answer.id" :value="answer.id" v-model="answers[questionIndex].choosedAnswers[0]">
          <label> {{ answer.testAnswer }}</label>
          <br>
        </div>
      </div>
      <div v-if="(question.questionType == 1)">
        {{ (questionIndex + 1) }}. {{ question.testQuestion }} - множественный
        <div v-for="answer in question.testAnswers">
          <input type="checkbox" :id="answer.id" :value="answer.id" v-model="answers[questionIndex].choosedAnswers">
          <label>{{ answer.testAnswer }}</label>
          <br>
        </div>
      </div>
    </div>
    <button @click="checkTestData()">Отправить</button>
  </q-page-container>
  <q-page-container v-else>
    <h2 v-if="checkData.isSucsess">Тест успешно сдан</h2>
    <h2 v-else>Тест провален</h2>
    <p>Отвечено правильно на {{checkData.rightAnswersCount}} из необходимых {{checkData.needAnswersCount}}</p>
    <button @click="goToArticlePage(this.$route.query.moduleId, this.$route.query.articleOrder)">Вернуться к статье</button>
  </q-page-container>
</template>

<script>
import { getPurchasedTest, checkTestAnswers } from "boot/axios";
import notify from "boot/notifyes";

export default {
  beforeMount() {
    this.updateData()
  },
  data() {
    return {
      testData: {},
      answers: [],
      isChecked: false,
      checkData: {}
    }
  },
  methods: {
    async updateData() {
      try {
        const response = await getPurchasedTest(this.$route.query.courseId, this.$route.query.moduleId, this.$route.query.articleOrder);
        console.log(response.data);
        if (response.data.isSuccess) {
          this.testData = response.data.value;
          for (let i = 0; i < this.testData.length; i++) {
            const element = this.testData[i];
            this.answers.push({
              questionId: element.testId,
              choosedAnswers: []
            });
          }
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
      this.$router.push({ path: '/course/article', query: { moduleId: moduleId, articleOrder: articleOrder, courseId: this.$route.query.courseId } })
    },
    async checkTestData() {
      const response = await checkTestAnswers({
        courseId: this.$route.query.courseId,
        moduleId: this.$route.query.moduleId,
        articleOrder: this.$route.query.articleOrder,
        answers: this.answers
      });
      console.log(response.data);
      if (response.data.isSuccess) {
        this.checkData = response.data.value;
        this.isChecked = true
        //notify.showSucsessNotify("Успешно")
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
    }
  }
}
</script>

<style lang="scss" scoped>

</style>