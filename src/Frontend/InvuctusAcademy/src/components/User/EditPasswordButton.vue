<template>
    <q-btn no-caps outline :class="$attrs.class" @click="getUserData">
        Изменить пароль
    </q-btn>

    <q-dialog v-model="editDialog">

      <q-card style="min-width: 350px">
        <q-card-section>
          <div class="text-h6 text-center">Изменить пароль</div>
        </q-card-section>
        <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
          <q-card-section class="q-pt-none">

            <q-input
            :type="isOldPwd ? 'password' : 'text'"
            label="Старый пароль"
            v-model="editPassword.oldPassword"
            >
            <template v-slot:append>
              <q-icon
                :name="isOldPwd ? 'visibility_off' : 'visibility'"
                class="cursor-pointer"
                @click="isOldPwd = !isOldPwd"
              />
            </template>
          </q-input>

            <q-input
            :type="isNewPwd ? 'password' : 'text'"
            dense
            v-model="editPassword.newPassword"
            label="Новый пароль"
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
              <q-icon
                :name="isNewPwd ? 'visibility_off' : 'visibility'"
                class="cursor-pointer"
                @click="isNewPwd = !isNewPwd"
              />
            </template>
          </q-input>

            <q-input
            :type="isConfirm ? 'password' : 'text'"
            dense
            v-model="editPassword.confirmPassword"
            label="Повторите пароль"
            lazy-rules
            :rules="[
              (val) =>
                val === editPassword.newPassword || 'Пароли должны совпадать',
              (val) =>
                (val && val.length > 6 && val.length < 21) ||
                'Пароль должен быть от 6 до 20 символов',
              (val) =>
                validatePassword(val) ||
                'Пароль должен содержать одну цифру, одну заглавную и одну прописную букву',
            ]">
            <template v-slot:append>
              <q-icon
                :name="isConfirm ? 'visibility_off' : 'visibility'"
                class="cursor-pointer"
                @click="isConfirm = !isConfirm"
              />
            </template>
          </q-input>

          </q-card-section>

          <q-card-actions class="text-primary">
            <q-btn flat type="reset" label="Отмена" />
            <q-btn flat type="submit" label="Сохранить" />
          </q-card-actions>
        </q-form>
      </q-card>
    </q-dialog>
  </template>

  <script>
  import { defineComponent, ref } from "vue";
  import { editPassword } from "boot/axios";
  import { fetchLoginedUserData } from 'boot/axios'
  import constants from "../../static/constants";
  import notify from "boot/notifyes";

  export default defineComponent({
    name: "editPassword-button",
    data() {
      return {
        data: [],
        editPassword: {
            email:"",
            oldPassword: "",
            newPassword:"",
            confirmPassword: ""
        },
        editDialog: false,
        isOldPwd: ref(true),
        isNewPwd: ref(true),
        isConfirm: ref(true),
      };
    },
    methods: {
      async getUserData (){
        this.editDialog = true
      },
      async onSubmit() {
        try {
          const autorize = await fetchLoginedUserData();
          this.editPassword.email = autorize.data.value.email
          const response = await editPassword(this.editPassword);
          if (response.data.isSuccess) {
            this.editDialog = false;
            notify.showSucsessNotify("Изменения успешно сохранены");
          }
          else {
            response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          }
        } catch (e) {
          notify.showErrorNotify(e.message);
        }
      },
      onReset() {
        this.editPassword.oldPassword = "";
        this.editPassword.newPassword = "";
        this.editPassword.confirmPassword = "";
        this.editDialog = false;
        this.errorMessage = "";
      },
      validatePassword(value) {
      return constants.PWD_REGEXP.test(value);
    },
    },
  });
  </script>

<style>
.edit-btn{
  color:#0375DF;
  font-size: 16px;
  font-weight: 500;
  border-radius: 10px;
}

</style>
