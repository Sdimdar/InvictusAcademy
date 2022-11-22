<template>
  <div class="q-pa-md">
     <q-card class="my-card">
       <q-card-section>
         <div class="text-h6"> Тестовые вопросы к уроку "{{ article.title }}"" </div>
         <div class="text-subtitle2"> {{ title }} </div>
       </q-card-section>

       <q-card v-if="article.test != null">

        <q-card-section>
        <div class="text-h8 text-left">Количество вопросов для пользователя "{{ article.test.testShowCount }}" </div>
        <div class="text-h8 text-left">Количество вопросов для успешного прохождения "{{ article.test.testCompleteCount }}" </div>

        <div>
              <table class="styled-table">
                 <thead class="text-subtitle2">
                  <td> Вопрос </td>
                  <td> Тип вопроса </td>
                  <td> Ответы </td>
                 </thead>
                  <tbody>
                      <tr v-for="question in article.test.testQuestions" :key="article.test.testQuestions.question">
                          <td>{{question.question}}</td>
                          <td>{{question.questionType}}</td>
                          <tr v-for="answer in question.answers">
                            {{ answer.text }}
                            <td v-show="answer.isCorrect" class="text-subtitle2"> - правильный ответ </td>
                          </tr>
                      </tr>
                  </tbody>
              </table>
    </div>
    <editTest :id="id" :order="order" @updateTest="getModuleData"/>
  </q-card-section>
       </q-card>

      <q-card v-else>
      <q-card-section class="q-pt-none">
        <q-card style="min-width: 1050px">
      <q-card-section>
        <div class="text-h6 text-center">Добавить тест к уроку "{{ article.title }}" </div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>

          <q-input dense v-model="test.testShowCount"  type="number" label="Количество вопросов для пользователя" />
          <q-input dense v-model="test.testCompleteCount"  type="number" label="Количество вопросов для успешного прохождения" />

          <div  v-for="(input, index) in test.testQuestions" :key="`questionInput-${index}`">
            <q-select  v-model="input.questionType" :options="types" label="Выберите тип вопроса" />
            <q-input dense v-model="input.question" label="Введите вопрос" />

            <div  v-for="(answerInput, index) in input.answers" :key="`answerInput-${index}`">
            <q-input dense v-model="answerInput.text" label="Вариант ответа" />
            <q-checkbox  v-model="answerInput.isCorrect" size="xs" label="Отметить как правильный" />
          </div>

          <q-card-actions align="left" class="text-primary">
            <q-btn @click="addAnswerField(input.answers)"  label="Добавить ответ" />
            <q-btn @click="removeField(index, input.answers)" label="Удалить ответ" />
          </q-card-actions>

          </div>

          <q-card-actions align="right" class="text-primary">
            <q-btn @click="addField(test.testQuestions)"  label="Добавить вопрос" />
            <q-btn @click="removeField(index, test.testQuestions)" label="Удалить вопрос" />
          </q-card-actions>

        </q-card-section>
        <q-card-actions align="center" class="text-primary">
          <q-btn flat type="submit" label="Сохранить" />
          <q-btn flat type="reset" label="Отмена" />
        </q-card-actions>
      </q-form>
    </q-card>
      </q-card-section>

      </q-card>

    </q-card>

   </div>

 </template>

 <script>
 import { fetchModuleById, addTest } from "boot/axios";
 import EditTest from 'src/components/Module/EditTestButton.vue';
 import notify from "boot/notifyes";
 import { toRaw } from 'vue';

 export default{
   components: {
    EditTest
   },
   data() {
     return {
       id: Number(this.$route.query.id),
       order: Number(this.$route.query.order),
       test:{
          testShowCount:"",
          testCompleteCount:"",
          testQuestions: [{question: "", questionType: "",  answers:[{text: "", isCorrect: false}]}]
        },
        types: [0, 1],
       article: "",
       articles: []
     };
   },
   methods: {
     async getModuleData() {
       const response = await fetchModuleById(this.id);
       this.title = response.data.value.title
       this.articles = response.data.value.articles
       console.log(this.order)
       this.article = this.articles.find(a => a.order === this.order)
     },
     addField(fieldType) {
      fieldType.push({question: "", questionType: "",  answers:[{text: "", isCorrect: false}]});
    },
    removeField(index, fieldType) {
      fieldType.splice(index, 1);
    },
    addAnswerField(fieldType) {
      fieldType.push({text: "", isCorrect: false});
    },
    async onSubmit() {
      console.log(this.test)
      this.article.test = this.test
      const object = toRaw(this.test)

      try {
        let payload = {
        moduleId: this.id,
        order: this.order,
        test: object
      }
        console.log(payload)
        const response = await addTest(payload);
        console.log(response)

        if(response.data.isSuccess){
          notify.showSucsessNotify("Тест успешно добавлен");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }

    },
   },

   mounted(){
     this.getModuleData()
   }
 }
 </script>

<style>
.styled-table{
    border-collapse: collapse;
    margin: 50px 0;
    min-width: 800px;
}
.styled-table tbody tr {
    border-bottom: 1px solid #dddddd;
    padding: 10px;
}

</style>
