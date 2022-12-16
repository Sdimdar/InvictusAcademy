<template>
  <q-btn :class="$attrs.class" label="Оставить заявку" @click="requestDialog = true"  no-caps color="accent"
  style="font-size: 20px; font-weight: 500; color: #F9F9F9"
  />

  <q-dialog v-model="requestDialog">
    <q-card style="min-width: 350px">
      <q-card-section>
        <div class="text-h6 text-center">Оставить заявку</div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section class="q-pt-none">
          <q-input
            dense
            mask="#(###) ### - ####"
            label="Телефонный номер"
            v-model="requestData.phoneNumber"
            lazy-rules
            :rules="[(val) => val !== '' || 'Номер должен содержать 11 цифр',
            ]"
          />
          <q-input
            dense
            label="Ваше имя"
            v-model="requestData.userName"
            lazy-rules
            :rules="[(val) => val !== '' || 'Это поле не может быть пустым']"
          />

        </q-card-section>

        <q-card-actions class="text-primary">
          <q-btn flat type="reset" label="Отмена" />
          <q-btn flat type="submit" label="Оставить заявку" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { createRequest } from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "request-button",
  data() {
    return {
      requestData: {
        userName: "",
        phoneNumber: ""
      },
      requestDialog: false,
    };
  },
  methods: {
    async onSubmit() {
      try {
        const response = await createRequest(this.requestData);
        if (response.data.isSuccess) {
          this.requestDialog = false;
          //this.$emit("autorize", response.data.email);
          notify.showSucsessNotify("Заявка принята! Наш менеджер свяжется с вами в ближайшее время!");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.requestData.userName = "";
      this.requestData.phoneNumber = "";
      this.requestDialog = false;
      this.errorMessage = "";
    },
  },
});
</script>
