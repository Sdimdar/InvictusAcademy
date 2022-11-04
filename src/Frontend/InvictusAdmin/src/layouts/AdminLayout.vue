<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-item to="/" >
          <img class="logo-img" src="img/invictus_academy_logo.png" />
        </q-item>
        <q-space />
        <template v-if="!logined" >
          <login-button class="nav-button" :logined="logined" @autorize="autorize"/>
        </template>
        <template v-else>
          <q-btn to="/admin-panel/showAllUser" class="nav-button" label="Список пользователей"/>
          <q-btn to="/admin-panel/requests" class="nav-button" label="Список заявок"/>
          <q-btn to="/admin-panel/createAdmin" class="nav-button" label="Создать админа"/>
          <logout-button class="nav-button" :logined="logined" @unautorize="unautorize" />
        </template>
      </q-toolbar>
    </q-header>
    <q-page-container>
        <router-view v-if="initialized" :logined="logined" :loginedUserEmail="loginedUserEmail"/>
    </q-page-container>
  </q-layout>
  
</template>


<script>
import LogoutButton from 'components/LogoutButton.vue'
import LoginButton from 'components/LoginButton.vue'
import { fetchLoginedUserData } from 'boot/axios'

export default {
  name: 'AdminLayout',
  components:{
    LoginButton,
    LogoutButton
  },
  data(){
    return{
      logined: false,
      loginedUserEmail: "",
      initialized: false
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
    async checkLogin(){
      try {
        const response = await fetchLoginedUserData();
        console.log(response)
        if (response.data.isSuccess) {
          this.autorize(response.data.value.email);
        }
        else{
          response.data.errors.forEach(error => {
            console.log(error)
          });
        }
      } catch (e) {
        console.log(e.message);
        this.unautorize()
      }
    }
  },
  async created() {
    await this.checkLogin()
    this.initialized = true
    console.log(process.env.GATEWAY);
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