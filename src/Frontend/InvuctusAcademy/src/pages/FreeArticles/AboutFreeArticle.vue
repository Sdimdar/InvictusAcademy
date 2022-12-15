<template>
  <div class="q-pa-md">
    <q-card class="my-card">
      <q-card-section>
        <div class="text-h4 article-title"> {{ this.article.title }} </div>
        <img class="article-image" :src="`${this.article.imageLink}`"
             alt="image cap"
        >
      </q-card-section>

      <iframe v-if="this.article.videoLink.length > 1" class="article-video" width="560" height="315" :src="`${this.article.videoLink}`" title="YouTube video player"
              frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
              allowfullscreen></iframe>

      <q-card-section class="">
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
          this.article.imageLink = response.data.value.imageLink
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
      this.article.imageLink = ""
    }
  },
});
</script>
<style scoped>
  .article-title{
    text-align: center;
  }
  .article-image{
    display: block;
    margin-left: auto;
    margin-right: auto;
    width: 50%;
  }
  .article-video{
    display: block;
    margin-left: auto;
    margin-right: auto;
    width: 50%;
  }
</style>
