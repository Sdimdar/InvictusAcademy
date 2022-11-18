<template>
  <q-btn :class="$attrs.class" label="Добавить тест" @click="openDialog()" />

  <q-dialog v-model="createTestDialog">
    <q-card style="min-width: 1050px">
      <q-card-section>
        <div class="text-h6 text-center">Добавить тест к уроку "{{ article.title }}" </div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>

          <q-input dense v-model="test.testShowCount"  type="number" label="Количество вопросов для пользователя" />
          <q-input dense v-model="test.testCompleteCount"  type="number" label="Количество вопросов для успешного прохождения" />

          <div v-for="question in test.testQuestions">
          <q-input dense v-model="testQuestion.question" label="Ввeдите вопрос" />
          <q-select
            v-model="testQuestion.questionType"
            :options="types"
            label="Выберите тип вопроса"
          />
        </div>


          <q-input dense v-model="answer.text" label="Вариант ответа" />


        </q-card-section>
        <q-card-actions align="right" class="text-primary">
          <q-btn flat type="reset" label="Отмена" />
          <q-btn flat type="submit" label="Сохранить" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { fetchModuleById } from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  props: {
      order: {
        type: Number,
        required: true,
      },
      id: {
        type: Number,
        required: true,
      },
    },
  data() {
      return {
        createTestDialog: false,
        articles: [],
        article: "",
        test:{
          testShowCount:"",
          testCompleteCount:"",
          testQuestions: [{
            testQuestion:{
              question: "",
              questionType:"",
              answers: [{
                text:"",
                isCorrect:""
              }]
          },
          }]
        },
        types: ["Single", "Multiple"],
      };
    },
  methods: {
    async openDialog(){
      this.createTestDialog = true
      const response = await fetchModuleById(this.id);
      this.articles = response.data.value.articles
      this.article = this.articles.find(a => a.order === this.order)
      console.log(this.article)
    },
    async onSubmit() {

    },
    onReset() {
      this.createTestDialog = false;
      this.errorMessage = "";
    }
  },
});
</script>
