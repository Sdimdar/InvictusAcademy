<template>
  <q-btn :class="$attrs.class" label="Войти" @click="loginDialog = true" />

  <q-dialog v-model="loginDialog">
    <q-card style="min-width: 350px">
      <q-card-section>
        <div class="text-h6 text-center">Авторизация</div>
      </q-card-section>
      <div class="text-center" style="color:red" v-for="item in errorMessage" >{{item}}</div>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>
          <q-input
            label="E-mail"
            type="email"
            dense
            v-model="loginData.email"
            autofocus
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Поле не должно быть пустым',
              (val) => validateEmail(val) || 'Это не E-mail',
            ]"
          />
          <q-input
            :type="isPwd ? 'password' : 'text'"
            label="Пароль"
            dense
            v-model="loginData.password"
            lazy-rules
            :rules="[
              (val) =>
                (val && val.length > 6 && val.length < 21) ||
                'Пароль должен быть от 6 до 20 символов',
              (val) =>
                validatePassword(val) ||
                'Пароль должен содержать одну цифру, одну заглавную и одну прописную букву',
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
          <q-checkbox
            size="xs"
            v-model="loginData.rememberMe"
            label="Запомнить?"
          />
        </q-card-section>

        <q-card-actions align="right" class="text-primary">
          <q-btn flat type="reset" label="Отмена" />
          <q-btn flat type="submit" label="Войти" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { login } from "boot/axios";
import { Notify } from "quasar";
import constants from "../static/constants";

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
      errorMessage:"",
      isPwd: true,
      loginDialog: false,
    };
  },
  methods: {
    async onSubmit() {
      try {
        const response = await login(this.loginData);
        localStorage.setItem('ticket', response.ticket)
        if(response.data.isSuccess){
        this.loginDialog = false;
        this.$emit("autorize", response.data.email);
        Notify.create({
          color: "green-4",
          textColor: "white",
          message: "Добро пожаловать",
        });
        }
        else{
          this.errorMessage = response.data.errors
        } 
      } catch (e) {
        Notify.create({
          color: "red-5",
          textColor: "white",
          icon: "warning",
          message: e.message,
        });
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
  },
});
</script>
