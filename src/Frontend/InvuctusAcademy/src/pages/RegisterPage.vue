<template>
  <div align="center" class="column" style="margin-top: 10px; margin-bottom: 20px;">
    <div class="col">
      <img src="img/logo.svg">
    </div>

    <div class="col" style="margin-top: 10px; margin-bottom: 20px;">
      <p style="font-size: 32px; font-weight: 700; color: #000000; margin-bottom: 20px;">
        Регистрация
      </p>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-input  class="register-input" dense v-model="registerData.firstName" label="Имя" lazy-rules
            :rules="[(val) => val !== '' || 'Это поле не может быть пустым']" />

          <q-input  class="register-input" dense v-model="registerData.lastName" label="Фамилия" lazy-rules
            :rules="[(val) => val !== '' || 'Это поле не может быть пустым']" />

          <q-input class="register-input" dense v-model="registerData.email" autofocus label="Электронная почта"
          lazy-rules :rules="[
            (val) => (val && val.length > 0) || 'Поле не должно быть пустым',
            (val) => validateEmail(val) || 'Это не E-mail',
          ]" />

          <q-input class="register-input" :type="isPwd ? 'password' : 'text'" dense v-model="registerData.password"
          label="Придумайте пароль"
          lazy-rules
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

          <q-input class="register-input" :type="isPwdConfirm ? 'password' : 'text'" dense v-model="registerData.passwordConfirm"
            label="Подтвердите пароль" lazy-rules :rules="[
              (val) =>
                val === registerData.password || 'Пароли должны совпадать',
              (val) =>
                (val && val.length > 6 && val.length < 21) ||
                'Пароль должен быть от 6 до 20 символов',
              (val) =>
                validatePassword(val) ||
                'Пароль должен содержать одну цифру, одну заглавную и одну прописную букву',
            ]">
            <template v-slot:append>
              <q-icon :name="isPwdConfirm ? 'visibility_off' : 'visibility'" class="cursor-pointer"
                @click="isPwdConfirm = !isPwdConfirm" />
            </template>
          </q-input>

          <q-input class="register-input" dense mask="#(###) ### - ####" v-model="registerData.phoneNumber"
          label="Номер телефона" lazy-rules
            :rules="[
              (val) =>
                (val && val.length === 17) || 'Номер должен содержать 11 цифр',
            ]" />

          <q-input  class="register-input" dense v-model="registerData.city" label="Город" lazy-rules
            :rules="[(val) => val !== '' || 'Это поле не может быть пустым']" />

        <div class="text-center" style="color:red" v-for="item in errorMessages" :key="item">{{ item.identifier }} :
          {{ item.errorMessage }}</div>

          <q-btn  class="register-btn" no-caps flat @click="onSubmit()">
            Зарегестрироваться
          </q-btn>
      </q-form>
      <div class="col" style="margin-top: 10px;">
        <q-checkbox  v-model="val" style="text-align: left;"
        :rules="[
          (val) =>
          (val === true) || 'Это обязательное поле',
            ]">
          Я согласен с условиями
        </q-checkbox>
        <a :href="'https://edunavigator.kz/docs/%D0%9F%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D0%B5%D0%BB%D1%8C%D1%81%D0%BA%D0%BE%D0%B5%20%D1%81%D0%BE%D0%B3%D0%BB%D0%B0%D1%88%D0%B5%D0%BD%D0%B8%D0%B5.pdf'"
          style="font-size: 13px; font-weight: 400; color: #0375DF;">
            пользовательского <br/> соглашения
          </a>
      </div>
      <div class="col" style="margin-top: 20px;">
          <a :href="'/'" style="font-size: 13px; font-weight: 400; color: #0375DF;">
            У меня есть аккаунт
          </a>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent, ref } from "vue";
import notify from "boot/notifyes";
import { register } from "boot/axios";
import constants from "src/static/constants";

export default defineComponent({
  name: "register-button",
  setup () {
    return {
      val: ref(true)
    }
  },
  data() {
    return {
      registerData: {
        email: "",
        password: "",
        passwordConfirm: "",
        phoneNumber: "",
        firstName: "",
        lastName: "",
        city: "",
      },
      registerDialog: ref(false),
      isPwd: ref(true),
      isPwdConfirm: ref(true),
      errorMessages: "",
      logined: false,
    };
  },
  methods: {
    async onSubmit() {
      console.log(this.registerData)
      try {
        const response = await register(this.registerData);
        if (response.data.isSuccess) {
          this.logined = true;
          notify.showSucsessNotify("Добро пожаловать");
          this.$router.push({ path: '/'})
        }
        else {
          this.errorMessages = response.data.validationErrors
        }

      } catch (e) {
        notify.showErrorNotify(e.message);
        console.log(e);
      }
    },
    onReset() {
      this.registerData.email = "";
      this.registerData.password = "";
      this.registerData.phoneNumber = "";
      this.registerData.firstName = "";
      this.registerData.lastName = "";
      this.registerDialog = false;
      errorMessages = "";
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

<style>
.register-input{
  width: 380px;
  height: 56px;
  background: #FCFCFF;
  border: 1px solid #CCCEF2;
  border-radius: 8px;
}

.register-btn{
  width: 380px;
  height: 51px;
  background: #c23636;
  border-radius: 10px;
  font-weight: 500;
  font-size: 16px;
  line-height: 19px;
  color: #F9F9F9;
}

</style>
