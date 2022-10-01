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
          <logout-button class="nav-button" :logined="logined" :loginedUserEmail="loginedUserEmail" @unautorize="unautorize" />
        </template>
      </q-toolbar>
    </q-header>
    <q-page-container>
        <router-view :logined="logined" :loginedUserEmail="loginedUserEmail"/>
    </q-page-container>
  </q-layout>
</template>


<script>
import LogoutButton from 'components/LogoutButton.vue'
import LoginButton from 'components/LoginButton.vue'
import RegisterButton from 'components/RegisterButton.vue'
import axios from 'axios'
import constants from '../static/constants'

export default {
  name: 'MainLayout',
  components:{
    LoginButton,
    LogoutButton,
    RegisterButton
  },
  data(){
    return{
      logined: false,
      loginedUserEmail: ""
    }
  },
  methods:{
    autorize: function(email){
      this.logined = true;
      this.loginedUserEmail = email;
    },
    unautorize: function(){
      this.logined = false;
      this.loginedUserEmail = ""
    },
    checkLogin(){
      axios.get("https://localhost:7210/User/GetLoginedUserData", constants.loginConfig)
      .then(ret => {
        this.autorize(ret.data.email);
      }).catch(ret => {
        this.unautorize()
      });
    }
  },
  mounted() {
      this.checkLogin()
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