<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-item to="/" >
          <img class="logo-img" src="../static/invictus_academy_logo.png" />
        </q-item>
        <q-space />
        <login-button class="nav-button" />
        <register-button class="nav-button" />
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
  }
}
</script>

<style>
  .logo-img{
    width: 200px;
    object-fit: contain;
  }
  .nav-button{
    font-size: 11px;
    border-radius: 0;
    align-self: stretch;
  }
</style>