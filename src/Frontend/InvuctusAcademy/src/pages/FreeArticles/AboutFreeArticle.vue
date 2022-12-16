<template>
  <q-page-container>
    <div class="article-title"> {{ article.title }} </div>
    <video class="article-video" controls="controls" :src="article.videoLink"></video>
    <div class="article-text" v-html="article.text"></div>
  </q-page-container>
</template>

<script>
import notify from "boot/notifyes";
import { fetchFreeArticle } from "boot/axios";
import { defineComponent } from "vue";

export default defineComponent({
  name: "AboutFreeArticle",
  data() {
    return {
      article: {}
    };
  },
  mounted() {
    this.getFreeArticle();
  },
  methods: {
    async getFreeArticle() {
      try {
        const response = await fetchFreeArticle(this.$route.params.id);
        console.log("FREEARTICLE")
        console.log(response)
        if (response.data.isSuccess) {
          this.article.title = response.data.value.title
          this.article.videoLink = response.data.value.videoLink
          this.article.text = response.data.value.text
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    }
  },
});
</script>
<style scoped>
.article-title {
  text-align: center;
  font-size: 40px;
  margin-bottom: 40px;
}

.article-video {
  display: block;
  margin-left: auto;
  margin-right: auto;
  width: 50%;
  margin-bottom: 30px;
}

.article-text{
  font-family: Inter;
}
</style>
