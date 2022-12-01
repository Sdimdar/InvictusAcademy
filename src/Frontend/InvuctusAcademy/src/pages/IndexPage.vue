<template>
  <q-page-container v-if="!logined">
    <request-button />
  </q-page-container>

  <q-page-container v-else class="column" style="height: 1200px; padding-bottom: 0px;">
      <div class="col"> БЛОК </div>
      <div class="col"> Мой прогресс </div>
      <div class="col"> Идеально подойдут вам </div>
      <div class="col"> Недавно просмотренные </div>
      <div class="col-2"> Читайте также </div>
  </q-page-container>

</template>

<script>
import { defineComponent } from 'vue'
import RequestButton from 'components/RequestButton.vue'

export default defineComponent({
  name: 'IndexPage',
  components: {
    RequestButton
  },
  props: {
    logined: {
      type: Boolean,
      required: true,
    },
  }
})
</script>
