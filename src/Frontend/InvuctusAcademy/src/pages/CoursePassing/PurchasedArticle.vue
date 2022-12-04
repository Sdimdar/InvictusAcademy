<template>
  <q-page-container class="column" style="height: 2000px; padding-bottom: 0px;">
    <div>
      <p>Купленная статья</p>
    </div>
  </q-page-container>
</template>

<script>
import { getPurchasedArticle } from "boot/axios";

export default {
  components: {

  },
  mounted() {
    this.updateData();
  },
  data() {
    return {

    }
  },
  methods: {
    async updateData() {

    }
  }
}
</script>

<style scoped>

</style>