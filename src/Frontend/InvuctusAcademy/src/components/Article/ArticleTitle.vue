<template>
  <span class="article-title">Модуль "{{moduleData.moduleInfo.title}}" - {{moduleData.order}} урок - "{{moduleData.title}}"</span>
</template>

<script>
export default {
  props:{
    moduleData : Object
  }
}
</script>

<style scoped>
.article-title{
  font-weight: 600;
  font-size: 22px;
  line-height: 110%;
}
</style>