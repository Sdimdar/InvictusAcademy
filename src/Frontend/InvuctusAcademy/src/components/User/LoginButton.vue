<template>
  <div align="center" class="column" style="margin-top: 60px; margin-bottom: 50px;">
    <div class="col">
      <img src="img/logo.svg">
    </div>

    <div class="col" style="margin-top: 30px; margin-bottom: 20px;">
      <p style="font-size: 32px; font-weight: 700; color: #000000; margin-top: 50px; margin-bottom: 50px;">
        Авторизация
      </p>
      <q-form class="q-gutter-md" @submit="onSubmit">
          <q-input class="login-input" label="Электронная почта" type="email" dense v-model="loginData.email" autofocus lazy-rules :rules="[
            (val) => (val && val.length > 0) || 'Поле не должно быть пустым',
            (val) => validateEmail(val) || 'Это не E-mail',
          ]" />
          <q-input class="login-input" :type="isPwd ? 'password' : 'text'" label="Введите пароль" dense v-model="loginData.password" lazy-rules
            :rules="[
              (val) =>
                (val && val.length > 6 && val.length < 21) ||
                'Пароль должен быть от 6 до 20 символов',
              (val) =>
                validatePassword(val) ||
                'Пароль должен содержать одну цифру, одну заглавную и одну прописную букву',
            ]">
            <template v-slot:append>
              <q-icon :name="isPwd ? 'visibility_off' : 'visibility'" class="cursor-pointer" @click="isPwd = !isPwd" />
            </template>
          </q-input>

          <q-btn class="login-btn" no-caps flat type="submit">
            Войти
          </q-btn>
      </q-form>
  </div>
  <div class="col">
    <a @click="changeMode" class="change-mode-button" >
      У меня нет аккаунта
    </a>
  </div>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import { login } from "boot/axios";
import notify from "boot/notifyes";
import constants from "../../static/constants";

export default defineComponent({
  name: "login-button",
  props: {
    logined: {
      type: Boolean,
      required: true,
    },
  },
  data() {
    return {
      loginData: {
        email: "",
        password: "",
        rememberMe: false,
      },
      isPwd: true,
      loginDialog: false,
    };
  },
  methods: {
    async onSubmit() {
      try {
        const response = await login(this.loginData);
        if (response.data.isSuccess) {
          this.loginDialog = false;
          this.$emit("autorize");
          notify.showSucsessNotify("Добро пожаловать");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.loginData.email = "";
      this.loginData.password = "";
      this.loginData.rememberMe = false;
      this.loginDialog = false;
      this.errorMessage = "";
    },
    validateEmail(value) {
      return constants.EMAIL_REGEXP.test(value);
    },
    validatePassword(value) {
      return constants.PWD_REGEXP.test(value);
    },
    changeMode() {
      this.$emit("changeMode")
    }
  },
});
</script>

<style>
.login-input{
  width: 380px;
  height: 56px;
  background: #FCFCFF;
  border: 1px solid #CCCEF2;
  border-radius: 8px;
  margin-bottom: 30px;
}

.login-btn{
  width: 380px;
  height: 51px;
  background: #c23636;
  border-radius: 10px;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  color: #F9F9F9;
}

.change-mode-button{
  font-size: 13px; 
  font-weight: 400; 
  color: #0375DF;
  cursor: pointer;
}

.change-mode-button:hover{
  text-decoration: underline;
}
</style>
