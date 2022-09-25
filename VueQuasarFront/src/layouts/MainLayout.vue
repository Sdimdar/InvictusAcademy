<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-item to="/" >
          <img class="logo-img" src="../static/invictus_academy_logo.png" />
        </q-item>
        <q-space />
        <template v-if="!logined" >
          <login-button class="nav-button" :logined="logined" @autorize="autorize"/>
          <register-button class="nav-button" :logined="logined" @autorize="autorize"/>
        </template>
        <template v-else>
          <q-btn to="/user" class="nav-button" label="Личный кабинет"/>
          <logout-button class="nav-button" :logined="logined" @unautorize="unautorize" />
        </template>
      </q-toolbar>
    </q-header>
    <q-page-container>
        <router-view :logined="logined"/>
    </q-page-container>
  </q-layout>
</template>


<script>
import LogoutButton from 'components/LogoutButton.vue'
import LoginButton from 'components/LoginButton.vue'
import RegisterButton from 'components/RegisterButton.vue'

export default {
  name: 'MainLayout',
  components:{
    LoginButton,
    LogoutButton,
    RegisterButton
  },
  data(){
    return{
      logined: false
    }
  },
  methods:{
    autorize: function(){
      this.logined = true;
    },
    unautorize: function(){
      this.logined = false;
    }
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