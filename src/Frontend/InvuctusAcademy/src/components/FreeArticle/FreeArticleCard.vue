<template>
  <q-card class="my-card">
    <q-img :src="`${freeArticle.imageLink}`">
      <div class="absolute-bottom">
        <div class="text-h6">{{ freeArticle.title }}</div>
      </div>
    </q-img>

    <q-card-actions>
      <q-btn @click="redirectToArticle(freeArticle.id)">
        Читать
      </q-btn>
    </q-card-actions>
  </q-card>
</template>

<script>
export default {
  props: {
    freeArticle: Object
  },
  methods: {
    redirectToArticle(id) {
      this.$router.push(`/FreeArticle/AboutFreeArticle/${id}`)
    }
  }
}
</script>

<style scoped>
.course-img {
  height: 100px;
  width: 500px;
}

.my-card {
  margin: 10px;
  width: 220px;
  border-radius: 12px;
}
</style>