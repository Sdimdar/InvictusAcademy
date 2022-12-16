<template>
  <div :style="$attrs.style" class="card">
    <div class="title">
      {{moduleData.moduleInfo.title}}
    </div>
    <div class="articles" v-for="(item, index) in moduleData.articles" @click="goToArticlePage(moduleData.moduleInfo.id, item.order)">
      <q-item>
        <q-item-section>
          <div :class="{'articles-not-opened':true, 'articles-opened':item.isOpened, 'articles-complete':item.isCompleted}">
            Урок №{{index}} - {{item.title}}
          </div>
        </q-item-section>

        <q-item-section side>
          <div class="row items-center" style="width:30px">
            <q-icon v-if="item.isCompleted" name="done" color="green" size="18px" />
          </div>
        </q-item-section>
      </q-item>
    </div>
  </div>  
</template>

<script>
export default {
  props:{
    moduleData : Object
  },
  methods: {
    goToArticlePage(moduleId, articleOrder) {
      if (this.moduleData.articles[articleOrder-1].isOpened) {
        this.$router.push({ path: '/course/article', query: { moduleId: moduleId, articleOrder: articleOrder, courseId:this.$route.query.courseId } })
      }
    }
  }
}
</script>

<style scoped>
.card{
  background: #F9F9F9;
  border-bottom: 1px solid #E9E9E9;
  box-shadow: 0px 4px 33px rgba(0, 0, 0, 0.12);
  padding: 16px;
}

.title{
  color: #0375DF;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  border-bottom: 1px solid #E9E9E9;
  padding-bottom: 5px;
}

.articles{
  font-weight: 300;
  font-size: 16px;
  line-height: 140%;
  border-bottom: 1px solid #E9E9E9;
  padding-bottom: 5px;
}

.articles-not-opened{
  color: #242424;
  font-weight: 300;
  font-size: 16px;
  line-height: 140%;
}

.articles-opened{
  color: #0375DF;
}

.articles-opened:hover{
  text-decoration: underline;
  cursor: pointer;
}

.articles-complete{
  color: #7D7D7D;
}

.articles-complete:hover{
  text-decoration: underline;
  cursor: pointer;
}
</style>