<template>
  <div style="display: flex; flex-direction: column; max-width: 810px;">
    <video class="video" controls="controls" :src="moduleData.videoLink"></video>

    <span class="text" v-html="moduleData.text"></span>
  </div>

</template>

<script>
export default {
  props: {
    moduleData: Object
  }
}
</script>

<style scoped>
.video {
  display: block;
  margin-bottom: 40px;
}

.text {
  font-weight: 300;
  font-size: 16px;
  line-height: 140%;
}
</style>