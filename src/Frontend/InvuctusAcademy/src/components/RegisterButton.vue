<template>
    <q-btn :class="$attrs.class" label="Зарегестрироваться" @click="registerDialog = true"/>
    
    <q-dialog v-model="registerDialog">
      <q-card style="min-width: 350px">
        <q-card-section>
          <div class="text-h6 text-center">Регистрация</div>
        </q-card-section>
        <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
            <q-card-section class="q-pt-none">
                <q-input 
                    dense 
                    v-model="registerData.email" 
                    autofocus 
                    label="E-mail" 
                    lazy-rules
                    :rules="[ 
                        val => val && val.length > 0 || 'Поле не должно быть пустым',
                        val => validateEmail(val) || 'Это не E-mail'
                    ]"
                />
                <q-input
                    :type="isPwd ? 'password' : 'text'"  
                    dense 
                    v-model="registerData.password" 
                    label="Пароль"
                    lazy-rules
                    :rules="[ 
                        val => val && val.length > 6 && val.length < 21 || 'Пароль должен быть от 6 до 20 символов',
                        val => validatePassword(val) || 'Пароль должен содержать одну цифру, одну заглавную и одну прописную букву'
                    ]" 
                >
                    <template v-slot:append>
                        <q-icon
                            :name="isPwd ? 'visibility_off' : 'visibility'"
                            class="cursor-pointer"
                            @click="isPwd = !isPwd"
                        />
                    </template>
                </q-input>
                <q-input 
                    :type="isPwdConfirm ? 'password' : 'text'"
                    dense 
                    v-model="registerData.passwordConfirm" 
                    label="Повтор пароля"
                    lazy-rules
                    :rules="[
                        val => val === registerData.password || 'Пароли должны совпадать', 
                        val => val && val.length > 6 && val.length < 21 || 'Пароль должен быть от 6 до 20 символов',
                        val => validatePassword(val) || 'Пароль должен содержать одну цифру, одну заглавную и одну прописную букву'
                    ]" 
                >
                    <template v-slot:append>
                        <q-icon
                            :name="isPwdConfirm ? 'visibility_off' : 'visibility'"
                            class="cursor-pointer"
                            @click="isPwdConfirm = !isPwdConfirm"
                        />
                    </template>
                </q-input>
                <q-input 
                    dense 
                    mask="#(###) ### - ####" 
                    v-model="registerData.phoneNumber" 
                    label="Телефонный номер" 
                    lazy-rules
                    :rules="[
                        val => val && val.length === 17 || 'Номер должен содержать 11 цифр'
                    ]"
                />
                <q-input 
                    dense 
                    v-model="registerData.firstName" 
                    label="Имя" 
                    lazy-rules
                    :rules="[
                        val => val !== '' || 'Это поле не может быть пустым'
                    ]"
                />
                <q-input 
                    dense 
                    v-model="registerData.middleName" 
                    label="Отчество" 
                />
                <q-input 
                    dense 
                    v-model="registerData.lastName" 
                    label="Фамилия" 
                    lazy-rules
                    :rules="[
                        val => val !== '' || 'Это поле не может быть пустым'
                    ]"
                />
                <q-input 
                    dense 
                    v-model="registerData.instagramLink" 
                    label="Ссылка на Instagram" 
                />
                <q-select 
                    v-model="registerData.citizenship" 
                    :options="citizenships" 
                    label="Гражданство" 
                />
            </q-card-section>

            <q-card-actions align="right" class="text-primary">
                <q-btn flat label="Отмена" v-close-popup type="reset"/>
                <q-btn flat label="Зарегестрироваться" type="submit"/>
            </q-card-actions>
        </q-form>
        
      </q-card>
    </q-dialog>
</template>

<script>
import { defineComponent, ref } from 'vue'
import axios from 'axios'
import { Notify } from 'quasar'
import constants from '../static/constants'

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
                citizenship: "Казахстан"
            },
            citizenships:
            [
                "Россия",
                "Казахстан"
            ],
            registerDialog: ref(false),
            isPwd: ref(true),
            isPwdConfirm: ref(true)
        }
    },
    props: {
        logined: {
            type: Boolean,
            required: true
        }
    },
    methods:{
        onSubmit(){
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
            axios.post("https://localhost:7243/Account/Register", data, constants.loginConfig)
            .then(ret =>{
                this.registerDialog = false;
                this.$emit('autorize');
                Notify.create({
                    color: 'green-4',
                    textColor: 'white',
                    message: 'Добро пожаловать'
                })
            })
            .catch(ret =>{
                Notify.create({
                    color: 'red-5',
                    textColor: 'white',
                    icon: 'warning',
                    message: ret.message
                })
            })
        },
        onReset(){
            this.registerData.email = ""
            this.registerData.password = ""
            this.registerData.phoneNumber = ""
            this.registerData.firstName = ""
            this.registerData.middleName = ""
            this.registerData.lastName = ""
            this.registerData.instagramLink = ""
            this.registerData.citizenship = "Казахстан"
            this.registerDialog = false;
        },
        validateEmail (value){
            return constants.EMAIL_REGEXP.test(value)
        },
        validatePassword (value){
            return constants.PWD_REGEXP.test(value)
        }

    }
})
</script>