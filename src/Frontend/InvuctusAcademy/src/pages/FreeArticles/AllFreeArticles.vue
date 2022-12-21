<template>
  <q-page-container v-if="initialized" style="text-align:center">
    <div class="row justify-content-start" v-if="allFreeAticles.length != 0">
      <free-article-card :freeArticle="freeArticle" v-for="freeArticle in allFreeAticles"/>
    </div>

    <div class="q-pa-lg flex flex-center" v-if="allFreeAticles.length != 0">
      <q-pagination v-model="this.pageNumber" @click="getFreeArticlesMounted" color="accent" :max="countPages"
        :max-pages="10" :boundary-numbers="false" />
    </div>

    <h2 v-if="allFreeAticles.length == 0">На текущий момент статей нет</h2>
  </q-page-container>
</template>

<script>
import { fetchAllFreeArticles, getFreeArticlesCount } from "boot/axios";
import FreeArticleCard from "src/components/FreeArticle/FreeArticleCard.vue";

export default {
  name: "AllFreeArticles",
  components: {
    FreeArticleCard
  },
  data() {
    return {
      allFreeAticles: [],
      pageNumber: 1,
      countPages: 0,
      pageSize: 20,
      initialized: false
    }
  },
  async beforeMount() {
    await this.getAllFreeArticlesCount()
    await this.getFreeArticles()
    this.initialized = true
  },
  watch:{
    pageNumber: function() {
      this.getFreeArticles()
    }
  },
  methods: {
    async getFreeArticles() {
      try {
        const response = await fetchAllFreeArticles(this.pageNumber, this.pageSize);
        if (response.data.isSuccess) {
          this.allFreeAticles = response.data.value.freeArticles;
          await this.getAllFreeArticlesCount();
        } else {
          response.data.errors.forEach(element => {
            notify.showErrorNotify(element);
          });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    async getAllFreeArticlesCount() {
      try {
        const response = await getFreeArticlesCount();
        if (response.data.isSuccess) {
          this.countPages = Math.ceil(response.data.value / this.pageSize)
          console.log(response.data)
        } else {
          response.data.errors.forEach(element => {
            notify.showErrorNotify(element);
          });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    }
  }
}
</script>

<style scoped>
</style>
