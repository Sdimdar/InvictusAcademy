<template>
  <div class="container">

    <div class="row justify-content-start">

      <div class="col-lg-4 col-md-12 col-sm-12 article-img-box"
           v-for="freeArticle in this.allFreeAticles"
      >
        <div class="card" @click="$router.push(`/FreeArticle/AboutFreeArticle/${freeArticle.id}`)">
          <div class="card-body">
            <h5 class="card-title">{{freeArticle.title}}</h5>
          </div>
          <img class="card-img-top" :src="`${freeArticle.imageLink}`"
               alt="Card image cap"
          >
        </div>
      </div>


    </div>

    <div class="q-pa-lg flex flex-center">
      <q-pagination
        v-model="this.pageNumber"
        @click="getFreeArticlesMounted"
        color="black"
        :max="countPages"
        :max-pages="10"
        :boundary-numbers="false"
      />
    </div>
  </div>
</template>

<script>
import {ref} from "vue";
import {fetchAllFreeArticles, getFreeArticlesCount} from "boot/axios";

export default {
  name: "allFreeArticles",
  setup () {
    return {
      current: ref(5)
    }
  },
  data(){
    return{
      allFreeAticles: [{}],
      pageNumber: 1,
      countPages: 0,
      pageSize: 5,
    }
  },
  mounted() {
    this.getFreeArticlesMounted();
  },
  methods:{
    async getFreeArticlesMounted() {
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

.article-img-box {
  margin-bottom: 100px;
  height: 160px;
  width: 270px;
}

.card-title{
  font-size: medium;
  text-align: center;
}

.card-body{
  padding-bottom: 1px;
  padding-top: 1px;
}
</style>
