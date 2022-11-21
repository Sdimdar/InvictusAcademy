<template>
  <div class="q-pa-md">
     <q-card class="my-card">
       <q-card-section>
         <div class="text-h6"> Тестовые вопросы к уроку "{{ article.title }}"" </div>
         <div class="text-subtitle2"> {{ title }} </div>

       </q-card-section>

      <q-card>

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
 import CreateTest from 'src/components/Module/CreateTestButton.vue';
 import notify from "boot/notifyes";

 export default{
   components: {
    CreateTest
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
        types: ["Single", "Multiple"],
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
      try {
        let payload = {
        moduleId: this.id,
        order: this.order,
        test: this.test
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
