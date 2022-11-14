<template>
  <q-btn color="primary" label="Текст" @click="getArticleData" />

  <q-dialog v-model="viewArticleText">
    <q-card style="min-width: 650px">
      <q-card-section>
        <div v-html="text"></div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">

        <q-card-actions class="text-primary">
          <q-btn flat type="reset" label="Отмена" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>


<script>
import { defineComponent, ref } from "vue";
import { fetchModuleById } from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "viewArticleDetails-button",
  props: {
      id: {
        type: Number,
        required: true,
      },
      order: {
        type: Number,
        required: true,
      },
    },
  data() {
    return {
      text: "",
      articles:[],
      viewArticleText: false
    };
  },
  methods: {
    async getArticleData(){
      this.viewArticleText = true;
      const response = await fetchModuleById(this.id);
      console.log(response)
      this.articles = response.data.value.value.articles
      console.log(this.articles)
      console.log(this.order)
      this.findArticle(this.order)
      console.log(this.text)
    },
    onReset() {
      this.viewArticleText = false;
      this.errorMessage = "";
    },
    findArticle(order){
      this.articles.find(article => { if (article.order === order) {this.text = article.text}})
    }
  },
});
</script>

