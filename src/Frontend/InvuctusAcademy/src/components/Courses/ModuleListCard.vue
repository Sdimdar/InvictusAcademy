<template>
  <q-card :class="$attrs.class">
    <div v-for="(item, index) in modules" :key="item.id" style="padding-bottom:0">
      <q-expansion-item expand-separator icon="perm_identity" style="border-bottom: 1px solid #E9E9E9;">
        <template v-slot:header>
          <q-item-section>
            <div class="module-title">{{ `Модуль №${index + 1} - ''${item.title}''` }}</div>
          </q-item-section>

          <q-item-section side>
            <div class="row items-center" style="width:70px">
              <div style="margin-right: 10px;">{{item.completedArticlesCount}}/{{item.articlesCount}}</div>
              <q-icon class="done-icon" v-if="item.isCompleted" name="done" color="white" size="18px" />
            </div>
          </q-item-section>
        </template>
        <q-card>
          <q-card-section>
            {{ item.shortDescription }}
          </q-card-section>
          <q-card-section>
            <q-btn @click="goToArticlePage(item.id, 1)">Перейти к модулю</q-btn>
          </q-card-section>
        </q-card>
      </q-expansion-item>
    </div>
  </q-card>
</template>

<script>
export default {
  props: {
    modules: Object
  },
  methods:{
    goToArticlePage(moduleId, articleOrder)
    {
      this.$emit('goToArticlePageEvent', moduleId, articleOrder)
    }
  }
}
</script>

<style lang="scss" scoped>
.done-icon{
  width: 24px;
  height: 24px;
  background: #63BD6C;
  border-radius: 44px;  
}

.module-title{
  font-weight: 600;
  font-size: 16px;
  line-height: 19px;
}
</style>