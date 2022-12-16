<template>
  <q-page-container v-if="!isChecked">
    <div class="question" v-for="(question, questionIndex) in testData">
      <div v-if="(question.questionType == 0)">
        <span class="question-title">{{ (questionIndex + 1) }}. {{ question.testQuestion }}</span>
        <span class="question-subtitle"></span>
        <div v-for="answer in question.testAnswers" class="answer">
          <input type="radio" :id="answer.id" :value="answer.id" v-model="answers[questionIndex].choosedAnswers[0]" class="answer-input-single">
          <label class="answer-label"> {{ answer.testAnswer }}</label>
          <br>
        </div>
      </div>
      <div v-if="(question.questionType == 1)">
        <span class="question-title">{{ (questionIndex + 1) }}. {{ question.testQuestion }}</span>
        <span class="question-subtitle">Возможно несколько вариантов ответа</span>
        <div v-for="answer in question.testAnswers" class="answer">
          <input type="checkbox" :id="answer.id" :value="answer.id" v-model="answers[questionIndex].choosedAnswers" class="answer-input-multiple">
          <label class="answer-label">{{ answer.testAnswer }}</label>
          <br>
        </div>
      </div>
    </div>
    <button class="submit-button" @click="checkTestData()">Отправить</button>
  </q-page-container>
  <q-page-container v-else style="text-align:center;">
    <h4 v-if="checkData.isSucsess">Тест успешно сдан</h4>
    <h4 v-else>Тест провален</h4>
    <p style="margin-bottom: 40px;">Отвечено правильно на {{checkData.rightAnswersCount}} из необходимых {{checkData.needAnswersCount}}</p>
    <button class="back-button" @click="goToArticlePage(this.$route.query.moduleId, this.$route.query.articleOrder)">Вернуться к статье</button>
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

<style scoped>
.question{
  margin-bottom: 40px;
}

.question-title{
  font-weight: 500;
  font-size: 16px;
  line-height: 120%;
  color: #3B3E4B;
  display: block;
  margin-bottom: 12px;
}

.question-subtitle{
  font-weight: 400;
  font-size: 12px;
  line-height: 120%;
  color: #878CA3;
  display: block;
  margin-bottom: 32px;
}

.answer{
  margin-bottom: 5px;
}

.answer-input-single{
  margin-right: 12px;
}

.answer-input-multiple{
  margin-right: 12px;
}

.answer-label{
  font-weight: 400;
  font-size: 16px;
  line-height: 19px;
  color: #4B4C7A;
}

.submit-button{
  background: #CD3838;
  box-shadow: 0px 4px 22px rgba(177, 20, 20, 0.12);
  border: 0px;
  border-radius: 10px;
  padding: 16px 32px;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  color: #F9F9F9;
}

.submit-button:hover{
  background: #ca2323;
}

.back-button{
  background-color: white;
  border: 1px solid #CD3838;
  border-radius: 10px;
  padding: 16px 32px;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  color: black;
}

.back-button:hover{
  box-shadow: 0px 4px 22px rgba(177, 20, 20, 0.12);
}
</style>