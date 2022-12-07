<template>
  <div style="display: flex; flex-direction: column;">
    <iframe class="video" width="810" height="404" src="https://www.youtube.com/embed/KnkxyFnzdHY"
      title="YouTube video player" frameborder="0"
      allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
      allowfullscreen></iframe>

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