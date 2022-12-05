<template>
  <div class="q-pa-md">
    <q-card class="my-card">
      <q-card-section>
        <div class="text-h6"> {{ this.article.title }} </div>
        <div class="text-subtitle2"> {{ this.article.title }} </div>

      </q-card-section>

      <iframe width="560" height="315" src="" title="YouTube video player"
              frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
              allowfullscreen></iframe>

      <q-card-section class="bg-grey-3">
        <div v-html="article.text"></div>
      </q-card-section>

    </q-card>
  </div>

</template>

<script>

import {fetchFreeArticle} from "boot/axios";
import {defineComponent} from "vue";

export default defineComponent({
  name: "aboutFreeArticle",

  props: {
    id: {
      type: Number,
      required: true,
    }
  },
  setup () {
    return {
      options: [
        'true', 'false'
      ]
    }
  },
  data() {
    return {
      shortDescription: "",
      articleId: this.$route.params.id,
      article:{
        title:"",
        videoLink:"",
        id: 0,
        text:"добавить текст"
      }
    };
  },
  mounted() {
    this.getFreeArticle();
  },
  methods: {
    async getFreeArticle(){
      try {
        const response = await fetchFreeArticle(this.articleId);
        console.log("FREEARTICLE")
        console.log(response)
        if(response.data.isSuccess){
          this.article.title = response.data.value.title
          this.article.text = response.data.value.text
          this.article.videoLink = response.data.value.videoLink
          this.article.id = response.data.value.id
          notify.showSucsessNotify("Бесплатная статья найдена");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    async onSubmit() {
      try {
        const response = await editFreeArticle(this.article);
        console.log(response)

        if(response.data.isSuccess){
          notify.showSucsessNotify("Бесплатная статья успешно отредактирована");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.article.title = "";
      this.article.text = "добавить текст";
      this.article.videoLink = "";
    }
  },
});
</script>
