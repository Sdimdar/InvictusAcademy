<template>
  <div class="q-pa-md">
    <q-card class="my-card">
      <q-card-section>
        <div class="text-h6"> {{ article.title }} </div>
        <div class="text-subtitle2"> {{ title }} </div>

        <editArticle :id="id" :order="order" />

      </q-card-section>

      <iframe width="560" height="315" src="https://www.youtube.com/embed/_Kqtj14rxes" title="YouTube video player"
        frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
        allowfullscreen></iframe>

      <q-card-section class="bg-grey-3">
        <div v-html="article.text"></div>
      </q-card-section>

      <q-card-section class="q-pt-none">
        <q-btn label="Тест к уроку" @click="openDetails(id, article.order)" />
      </q-card-section>

    </q-card>
  </div>

</template>

<script>
import { fetchModuleById } from "boot/axios";
import EditArticle from 'src/components/Module/EditArticleButton.vue'

export default {
  components: {
    EditArticle,
  },
  data() {
    return {
      id: this.$route.query.id,
      order: Number(this.$route.query.order),
      title: "",
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
      console.log(this.article)
    },
    openDetails(id, order) {
      this.$router.push({ path: '/admin-panel/test', query: { id: id, order: order } })
    }
  },
  mounted() {
    this.getModuleData()
  }
}
</script>
