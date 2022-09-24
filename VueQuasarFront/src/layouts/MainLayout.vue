<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-item style="width: 220px;"  to="/" >
          <img class="logo-img" src="../static/invictus_academy_logo.png" />
        </q-item>
        <q-space />
        <login-button />
        <register-button />
      </q-toolbar>
    </q-header>
    <q-page-container>
        <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import LoginButton from 'components/LoginButton.vue'
import RegisterButton from 'components/RegisterButton.vue'

export default {
  name: 'MainLayout',
  components:{
    LoginButton,
    RegisterButton
  },
}
</script>

<style>
  .logo-img{
    width: 100%;
    object-fit: contain;
  }
</style>