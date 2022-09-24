<template>
    <q-btn  stretch flat label="Зарегестрироваться" @click="register = true"/>
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
</template>

<script>
import { defineComponent, ref } from 'vue'
import axios from 'axios'
import { reactive } from 'vue' 
import { useVuelidate } from '@vuelidate/core'
import { required, minLength, email } from '@vuelidate/validators'

export default defineComponent({
    name: 'register-button',
    data(){
        return{
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
        ]
        }
    },
    setup (){
        return {
            register: ref(false)
        }
    },
    methods:{
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
            })
            .catch(ret =>{
                console.log("ret");
            })
        },
        checkRegisterData: function(){

        }
    }
})
</script>