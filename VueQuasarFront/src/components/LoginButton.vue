<template>
    <q-btn :class="$attrs.class" label="Войти" @click="login = true"/>

    <q-dialog v-model="login">
      <q-card style="min-width: 350px">
        <q-card-section>
          <div class="text-h6 text-center">Авторизация</div>
        </q-card-section>

        <q-card-section class="q-pt-none">
          <q-input 
            label="E-mail" 
            dense 
            v-model="loginData.emailOrPhoneNumber" 
            autofocus 
            @keyup="checkLoginData"/>
          <q-input 
            :type="isPwd ? 'password' : 'text'" 
            label="Пароль" 
            dense 
            v-model="loginData.password" 
            @keyup="checkLoginData" 
            @keyup.enter="loginF" >
            <template v-slot:append>
                <q-icon
                    :name="isPwd ? 'visibility_off' : 'visibility'"
                    class="cursor-pointer"
                    @click="isPwd = !isPwd"
                />
            </template>
          </q-input>
          <q-checkbox size="xs" v-model="loginData.rememberMe" label="Запомнить?" />
        </q-card-section>

        <q-card-actions align="right" class="text-primary">
          <q-btn flat label="Отмена" v-close-popup />
          <q-btn flat label="Войти" @click="loginF" />
        </q-card-actions>
      </q-card>
    </q-dialog>
</template>

<script>
import { defineComponent, ref } from 'vue'
import axios from 'axios'
import { reactive } from 'vue' 
import { useVuelidate } from '@vuelidate/core'
import { required, minLength, email } from '@vuelidate/validators'

export default defineComponent({
    name: 'login-button',
    data() {
        return{
            loginData: {
                emailOrPhoneNumber: "",
                password: "",
                rememberMe: false
            },
            isPwd: ref(true)
        }
    },
    props: {
        logined: {
            type: Boolean,
            required: true
        }
    },
    validations:{
        loginDataRules: {
            emailOrPhoneNumber: {required, email},
            password: {required, minLength}
        }
    },
    setup(){
        return {
            login: ref(false)
        }
    },
    methods:{
        loginF: async function()
        {
            let config = {
                headers: {
                    "Content-Type": "application/json",
                },
                withCredentials: true
            }
            let data = {
                emailOrPhoneNumber: this.loginData.emailOrPhoneNumber,
                password: this.loginData.password,
                rememberMe: false
            }
            axios.post("https://localhost:7243/Account/Login",data, config)
            .then(ret =>{
                console.log("ok");
                console.log(ret);
                this.login = false;
                this.$emit('autorize');
            }).catch(ret =>{
                console.log("bad");
            })
        },
        checkLoginData: function(){
            
        }
    }
})
</script>