<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-item class="toolbar-logo"  to="/" >
          <img class="logo-img" src="../static/invictus_academy_logo.png" />
        </q-item>
        
        <q-space />
        
        <q-btn stretch flat label="Войти" @click="login = true"/>
        <q-btn stretch flat label="Зарегестрироваться" @click="register = true"/>
      </q-toolbar>
    </q-header>

    <q-dialog v-model="login">
      <q-card style="min-width: 350px">
        <q-card-section>
          <div class="text-h6 text-center">Авторизация</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input dense v-model="loginData.emailOrPhoneNumber" autofocus />
          <q-input dense v-model="loginData.password" @keyup.enter="loginF" />
        </q-card-section>

        <q-card-actions align="right" class="text-primary">
          <q-btn flat label="Отмена" v-close-popup />
          <q-btn flat label="Войти" @click="loginF" />
        </q-card-actions>
      </q-card>
    </q-dialog>

    <q-dialog v-model="register">
      <q-card style="min-width: 350px">
        <q-card-section>
          <div class="text-h6 text-center">Регистрация</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input dense v-model="registerData.email" autofocus label="E-mail" />
          <q-input dense v-model="registerData.password" label="Пароль" />
          <q-input dense v-model="registerData.passwordConfirm" label="Повтор пароля" />
          <q-input dense v-model="registerData.phoneNumber" label="Телефонный номер" />
          <q-input dense v-model="registerData.firstName" label="Имя" />
          <q-input dense v-model="registerData.middleName" label="Отчество" />
          <q-input dense v-model="registerData.lastName" label="Фамилия" />
          <q-input dense v-model="registerData.instagramLink" label="Ссылка на Instagram" />
          <q-select v-model="registerData.citizenship" :options="citizenships" label="Гражданство" />
        </q-card-section>

        <q-card-actions align="right" class="text-primary">
          <q-btn flat label="Отмена" v-close-popup />
          <q-btn flat label="Зарегестрироваться" @click="registerF" />
        </q-card-actions>
      </q-card>
    </q-dialog>

    <q-page-container>
      <q-btn flat label="Выйти" @click="logout" />
      <keep-alive>
        <router-view />
      </keep-alive>
    </q-page-container>
  </q-layout>
</template>

<script>
import { defineComponent, ref } from 'vue'
import axios from 'axios'
import { Cookies } from 'quasar'


export default defineComponent({
  name: 'MainLayout',
  data(){
    return{
      loginData: {
        emailOrPhoneNumber: "",
        password: "",
        rememberMe: false
      },
      registerData:
      {
        email: "",
        password: "",
        passwordConfirm: "",
        phoneNumber: "",
        firstName: "",
        middleName: "",
        lastName: "",
        instagramLink: "",
        citizenship: ""
      },
      citizenships:
      [
        "Россия",
        "Казахстан"
      ],
      
    }
  },
  setup (){
    console.log(Cookies.get(".AspNetCore.Identity.Application"));
    return {
      login: ref(false),
      register: ref(false)
    }
  },
  methods:{
    loginF: async function()
    {
      let config = {
        headers: {
          "Access-Control-Allow-Methods": "GET, POST",
          "Access-Control-Allow-Origin": "*"
        }
      }
      let data = {
        emailOrPhoneNumber: this.loginData.emailOrPhoneNumber,
        password: this.loginData.password,
        rememberMe: false
      }
      axios.post("https://localhost:7243/Account/Login",data, config)
      .then(ret =>{
        console.log("ok");
        this.login = false;
      }).catch(ret =>{
        console.log("bad");
      })
    },
    registerF: async function()
    {
      let config = {
        headers: {
          "Access-Control-Allow-Methods": "GET, POST",
          "Access-Control-Allow-Origin": "*"
        }
      }
      let data = {
        email: this.registerData.email,
        password: this.registerData.password,
        phoneNumber: this.registerData.phoneNumber,
        firstName: this.registerData.firstName,
        middleName: this.registerData.middleName,
        lastName: this.registerData.lastName,
        instagramLink: this.registerData.instagramLink,
        citizenship: this.registerData.citizenship
      }
      axios.post("https://localhost:7243/Account/Register", data, config)
      .then(ret =>{
        console.log("ok");
        this.register = false;
        Cookies.set(".AspNetCore.Identity.Application",ret.headers('set-cookie'));
      }).catch(ret =>{
        console.log("ret");
      })
    },
    logout: async function()
    {
      let config = {
        headers: {
          "Access-Control-Allow-Methods": "GET, POST",
          "Access-Control-Allow-Origin": "*",
          "cookie": '.AspNetCore.Antiforgery.9euSgxLAROE' + Cookies.get('.AspNetCore.Antiforgery.9euSgxLAROE')
        }
      }
      axios.post("https://localhost:7243/Account/LogOff", {}, config)
      .then(ret =>{
        console.log("ok");
      }).catch(ret =>{
        console.log("ret");
      })
    }
  }
})
</script>

<style>
  .toolbar-logo{
    width: 220px;
  }
  .logo-img{
    width: 100%;
    object-fit: contain;
  }
</style>