<template>
  <q-btn :class="$attrs.class" label="Добавить тест" @click="openDialog()" />

  <q-dialog v-model="createTestDialog">
    
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
