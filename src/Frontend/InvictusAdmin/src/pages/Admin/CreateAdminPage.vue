<template>
  <q-card style="width: 500px">
    <q-card-section>
      <div class="text-h6 text-center">Создать нового админа</div>
    </q-card-section>
    <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
      <q-card-section class="q-pt-none">

        <q-input dense v-model="newAdminData.userName" label="Имя пользователя" lazy-rules :rules="[
          (val) => (val && val.length > 0) || 'Поле не должно быть пустым',
          (val) => validateEmail(val) || 'Это не E-mail',
        ]" />

        <q-input dense v-model="newAdminData.password" :type="isNewPwd ? 'password' : 'text'" label="Пароль" lazy-rules
          :rules="[
            (val) =>
              (val && val.length > 6 && val.length < 21) ||
              'Пароль должен быть от 6 до 20 символов',
            (val) =>
              validatePassword(val) ||
              'Пароль должен содержать одну цифру, одну заглавную и одну прописную букву',
          ]">
          <template v-slot:append>
            <q-icon :name="isNewPwd ? 'visibility_off' : 'visibility'" class="cursor-pointer"
              @click="isNewPwd = !isNewPwd" />
          </template>
        </q-input>

        <q-input dense v-model="newAdminData.confirmPassword" :type="isPwdConfirm ? 'password' : 'text'"
          label="Подтвердить пароль" lazy-rules :rules="[
            (val) =>
              val === newAdminData.password || 'Пароли должны совпадать',
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

        <q-select v-model="newAdminData.roles" filled multiple :options="options" label="Выбрать роль" />
      </q-card-section>

      <q-card-actions class="text-primary">
        <q-btn flat type="reset" label="Отмена" />
        <q-btn flat type="submit" label="Сохранить" />
      </q-card-actions>
    </q-form>
  </q-card>
</template>
    
<script>
import { defineComponent, ref } from "vue";
import { fetchLoginedUserData, createAdmin } from "boot/axios";
import notify from "boot/notifyes";
import constants from "../../static/constants";

export default defineComponent({
  name: 'CreateAdminPage',
  data() {
    return {
      data: [],
      newAdminData: {
        userName: "",
        password: "",
        confirmPassword: "",
        roles: ref(null),
      },
      options: ["manager", "copywriter", "instructor", "moderator"],
      createAdminDialog: false,
      isNewPwd: ref(true),
      isPwdConfirm: ref(true),
    }
  },
  methods: {
    async onSubmit() {
      try {
        const autorize = await fetchLoginedUserData();
        if (autorize.data.isSuccess) {
          const response = await createAdmin(this.newAdminData);
          if (response.data.isSuccess) {
            this.createAdminDialog = false;
            notify.showSucsessNotify("Администратор успешно создан");
            this.onReset();
          }
          else {
            notify.showSucsessNotify("Пользователь не залогинен");
            response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          }
        }
        else {
          response.data.errors.forEach(error => {
            console.log(error)
          });
        }
      }
      catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.newAdminData.userName = "";
      this.newAdminData.password = "";
      this.newAdminData.confirmPassword = "";
      this.newAdminData.roles = ref(null);
      this.createAdminDialog = false;
      this.errorMessage = "";
    },
    validateEmail(value) {
      return constants.EMAIL_REGEXP.test(value);
    },
    validatePassword(value) {
      return constants.PWD_REGEXP.test(value);
    }
  }
})
</script>
  