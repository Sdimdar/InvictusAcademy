<template>
  <q-page-container v-if="!logined">
    <request-button />
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
