<template>
  <div class="q-pa-md">
     <q-card class="my-card">
       <q-card-section>
         <div class="text-h6"> Тестовые вопросы к уроку "{{ article.title }}"" </div>
         <div class="text-subtitle2"> {{ title }} </div>

       </q-card-section>

       <q-card v-if="test != null">

        <q-card-section class="q-pt-none">
        <div> Количество вопросов для пользователя: {{test.testShowCount}} </div>
        <div> Количество вопросов для успешного прохождения: {{test.testCompleteCount}} </div>
        </q-card-section>

      </q-card>

      <q-card v-else>

      <q-card-section class="q-pt-none">
        <createTest :id="id" :order="order" />
      </q-card-section>

      </q-card>

    </q-card>

   </div>

 </template>

 <script>
 import { fetchModuleById } from "boot/axios";
 import CreateTest from 'src/components/Module/CreateTestButton.vue'

 export default{
   components: {
    CreateTest,
   },
   data() {
     return {
       id: this.$route.query.id,
       order: Number(this.$route.query.order),
       test:"",
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
       this.test = this.article.test
       console.log(this.test)
     }
   },
   mounted(){
     this.getModuleData()
   }
 }
 </script>
