<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-img class="toolbar-logo" src="../static/invictus_academy_logo.png" />
        <q-space />
        <q-separator dark vertical />
        <q-btn stretch flat label="Зарегестрироваться" />
      </q-toolbar>
    </q-header>


    <q-page-container>
      <keep-alive>
        <router-view />
      </keep-alive>
    </q-page-container>
  </q-layout>
</template>

<script>
import { defineComponent, ref } from 'vue'
export default defineComponent({
  name: 'MainLayout',

})
</script>

<style>
  .toolbar-logo{
    width: 180px;
    cursor: pointer;
  }
</style>