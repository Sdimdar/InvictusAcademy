<template>
  <q-card :class="$attrs.class">
    <q-card-section v-for="(item, index) in modules" :key="item.id" style="padding-bottom:0">
      <q-expansion-item expand-separator icon="perm_identity" style="border-bottom: 1px solid #E9E9E9;">
        <template v-slot:header>
          <q-item-section>
            {{ `Модуль №${index + 1} - ''${item.title}''` }}
          </q-item-section>

          <q-item-section side>
            <div class="row items-center" style="width:30px">
              <q-icon v-if="item.isCompleted" name="done" color="green" size="24px" />
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
    </q-card-section>
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
</style>