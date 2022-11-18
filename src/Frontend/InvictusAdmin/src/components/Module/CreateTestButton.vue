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

          <div  v-for="(input, index) in test.testQuestions" :key="`questionInput-${index}`">
            <q-input dense v-model="input.question" label="Введите вопрос" />
            <q-select  v-model="input.questionType" :options="types" label="Выберите тип вопроса" />

            <div v-for="(answerInput, answerIndex) in input.answers" :key="`answerInput-${answerIndex}`">
            <q-input  v-model="answerInput.text" dense label="Вариант ответа" />
            <q-checkbox  v-model="answerInput.isCorrect" size="xs" label="Отметить как правильный" />
            </div>

            <q-btn @click="addField(answerInput, input.answers)" label="Добавить ответ" />
            <q-btn @click="removeField(answerIndex, input.answers)" label="Удалить ответ" />

          </div>

          <q-card-actions align="right" class="text-primary">
            <q-btn @click="addField(input, test.testQuestions)"  label="Добавить вопрос" />
            <q-btn @click="removeField(index, test.testQuestions)" label="Удалить вопрос" />
          </q-card-actions>

        </q-card-section>
        <q-card-actions align="center" class="text-primary">
          <q-btn flat type="submit" label="Сохранить" />
          <q-btn flat type="reset" label="Отмена" />
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
          testQuestions: [{question: "", questionType: "Single",  answers:[{text: "", isCorrect: false}]}]
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
      console.log(this.test)
    },
    onReset() {
      this.createTestDialog = false;
      this.errorMessage = "";
    },
    addField(value, fieldType) {
      fieldType.push({value: {} });
    },
    removeField(index, fieldType) {
      fieldType.splice(index, 1);
    }
  },
});
</script>
