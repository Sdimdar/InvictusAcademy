<template>
  <q-page-container class="column" style="padding-bottom: 50px;">
    <div style="margin-bottom: 35px">
      <component :is="articleTitle" v-if="articleTitle" :moduleData="moduleData"/>
    </div>
    <div style="display: flex; flex-wrap: nowrap; justify-content: space-between; margin-bottom: 60px;  margin-right: 30px;">
      <component :is="articleDescription" v-if="articleDescription" :moduleData="moduleData"/>
      <component style="width: 267px;" :is="moduleDataAccordeons" v-if="moduleDataAccordeons" :moduleData="moduleData"/>
    </div>
    <div style=" margin-right: 30px;">
      <component :is="articleButtons" v-if="articleButtons" :moduleData="moduleData"/>
    </div>
  </q-page-container>
</template>

<script>
import { getPurchasedArticle } from "boot/axios";
import notify from "boot/notifyes";
import ArticleTitle from "src/components/Article/ArticleTitle.vue"
import ArticleDescription from "src/components/Article/ArticleDescription.vue"
import ModuleDataAccordeons from "src/components/Article/ModuleDataAccordeons.vue"
import ArticleButtons from "src/components/Article/ArticleButtons.vue"

export default {
  components: {
    ArticleTitle,
    ArticleDescription,
    ModuleDataAccordeons,
    ArticleButtons
  },
  beforeMount() {
    this.updateData();
  },
  watch: { 
    '$route': {
      handler: function() {
        this.updateData();
      },
      deep: true,
      immediate: true
    }
  },
  async updated() {
    
  },
  data() {
    return {
      moduleData : {},
      articleTitle : null,
      articleDescription : null,
      moduleDataAccordeons : null,
      articleButtons: null
    }
  },
  methods: {
    async updateData() {
      try {
        const response = await getPurchasedArticle(this.$route.query.courseId, this.$route.query.moduleId, this.$route.query.articleOrder);
        console.log(response.data);
        if (response.data.isSuccess) {
          this.moduleData = response.data.value;
          this.articleTitle = ArticleTitle
          this.articleDescription = ArticleDescription
          this.moduleDataAccordeons = ModuleDataAccordeons
          this.articleButtons = ArticleButtons
        }
        else if (response.data.errors.length != 0) {
          response.data.errors.forEach(element => {
            notify.showErrorNotify(element)
          });
        }
        else if (response.data.validationErrors.length != 0) {
          response.data.validationErrors.forEach(element => {
            notify.showErrorNotify(element)
          });
        }
      } catch (error) {
        console.log(error.message);
      }
    },
    goToArticlePage(moduleId, articleOrder) {
      if (moduleData.articles[articleOrder].isOpened) {
        this.$router.push({ path: '/course/article', query: { moduleId: moduleId, articleOrder: articleOrder, courseId:this.id } })
      }
    }
  }
}
</script>

<style scoped>

</style>