<template>
    <q-btn no-caps outline class="edit-btn" @click="getUserData">
      Редактировать
    </q-btn>

    <q-dialog v-model="editDialog">
      <q-card style="min-width: 350px">
        <q-card-section>
          <div class="text-h6 text-center">Редактировать профиль</div>
        </q-card-section>
        <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
          <q-card-section class="q-pt-none">

            <q-input dense v-model="editData.firstName" label="Имя" />
            <q-input dense v-model="editData.lastName" label="Фамилия" />
            <q-input dense v-model="editData.middleName" label="Отчество" />

            <q-input
            dense
            mask="#(###) ### - ####"
            v-model="editData.phoneNumber"
            label="Телефонный номер"
            lazy-rules
            :rules="[
              (val) =>
                (val && val.length === 17) || 'Номер должен содержать 11 цифр',
            ]"
          />

            <q-input
            dense
            v-model="editData.instagramLink"
            label="Ссылка на Instagram"
          />

           <q-select
            v-model="editData.citizenship"
            :options="citizenships"
            label="Гражданство"
          />

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
  import { defineComponent } from "vue";
  import { editProfile } from "boot/axios";
  import { fetchLoginedUserData } from 'boot/axios'
  import { fetchUserData } from 'boot/axios'
  import notify from "boot/notifyes";

  export default defineComponent({
    name: "edit-button",
    data() {
      return {
        data: [],
        editData: {
            email: "",
            firstName:"",
            lastName: "",
            middleName: "",
            phoneNumber: "",
            instagramLink: "",
            citizenship: "Казахстан",
        },
        citizenships: ["Казахстан", "Россия", "другое"],
        editDialog: false,
      };
    },
    methods: {
      async getUserData (){
        this.editDialog = true
        const autorize = await fetchLoginedUserData();
        this.autorizeEmail = autorize.data.value.email
        const response = await fetchUserData(this.autorizeEmail);
        this.data = response.data.value;
        this.editData.firstName = response.data.value.firstName
        this.editData.lastName = response.data.value.lastName
        this.editData.middleName = response.data.value.middleName
        this.editData.phoneNumber = response.data.value.phoneNumber
        this.editData.instagramLink = response.data.value.instagramLink
        this.editData.citizenship = response.data.value.citizenship
      },
      async onSubmit() {
        try {
          const autorize = await fetchLoginedUserData();
          this.editData.email = autorize.data.value.email
          const response = await editProfile(this.editData);
          if (response.data.isSuccess) {
            this.editDialog = false;
            this.$emit("autorize");
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
        this.editData.firstName = "";
        this.editData.lastName = "";
        this.editData.middleName = "";
        this.editData.phoneNumber = "";
        this.editData.instagramLink = "";
        this.editData.citizenship = "";
        this.editDialog = false;
        this.errorMessage = "";
      }
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
