<template>
  <q-card class="my-card">
    <q-img :src="`${stream.imageLink}`">
      <div class="absolute-bottom">
        <div class="text-h6">{{ stream.name }}</div>
      </div>
    </q-img>

    <q-card-actions>
      <q-btn v-if="stream.isOpened" @click="redirectToArticle()">
        Смотреть
      </q-btn>
      <div v-else>
        Скоро начало!
      </div>
    </q-card-actions>
  </q-card>
</template>

<script>
  export default {
    props: {
      stream: Object
  },
  methods: {
    redirectToArticle() {
      this.$router.push(`StreamingRooms/Room/${this.stream.address}`)
    }
  }
  }
</script>

<style lang="scss" scoped>
.stream-img {
  height: 100px;
  width: 500px;
}

.my-card {
  margin: 10px;
  width: 220px;
  border-radius: 12px;
}
</style>