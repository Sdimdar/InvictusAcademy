<template>
   <q-btn no-caps color="accent" class="start-btn" label="Начать обучение"  @click="openDialog()" />

<q-dialog v-model="paymentDialog">
  <q-card style="min-width: 350px">
    <q-card-section>
      <div class="text-h6 text-center">Купить курс "{{ title }}"</div>
    </q-card-section>
    <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
      <q-card-section>
        <div>
          <p>
            Покупка курса осуществляется с помощью отправки заявки менеджеру
          </p>
          <p>
            Стоимость курса {{ cost }} тенге
          </p>
        </div>
      </q-card-section>
      <q-card-actions align="right" class="text-primary">
        <q-btn flat type="reset" label="Отмена" />
        <q-btn flat type="submit" label="Отправить заявку" />
      </q-card-actions>
    </q-form>
  </q-card>
</q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { addToPayments } from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  props: {
      title: {
        type: String,
        required: true,
      },
      id: {
        required: true,
      },
      cost: {
        type: Number,
        required: true,
      },
    },
    data() {
      return {
        paymentDialog: false
      };
    },
    methods: {
      openDialog(){
        this.paymentDialog = true
      },
      async addPayment() {
      const response = await addToPayments(this.id);
      if(response.data.isSuccess){
        this.paymentDialog = false
        console.log(response.data.value)
        notify.showSucsessNotify("Запрос на покупку отправлен менеджер свяжется  вами!");
      }
      else{
        notify.showErrorNotify("Что-то пошло не так. Попробуйте еще раз позже");
      }
    },
    onSubmit(){
      this.addPayment();
    },
    onReset(){
      this.paymentDialog = false
    }
    }
})

</script>

