<template>
  <q-btn :class="$attrs.class" label="Удалить" @click="openDialog()" />

  <q-dialog v-model="deleteDialog">
    <q-card style="min-width: 550px">
      <q-card-section>
        <div class="text-h6 text-center">Удалить модуль</div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>
          <div>Вы уверены что хотите удалить модуль {{ title }}?</div>
        </q-card-section>
        <q-card-actions align="right" class="text-primary">
          <q-btn flat type="reset" label="Отмена" />
          <q-btn flat type="submit" label="Удалить" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { deleteModule, fetchModuleById, fetchModuleByFilterString} from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "deleteModule",
  props: {
      title: {
        type: String,
        required: true,
      },
      id: {
        type: Number,
        required: true,
      },
    },
  data() {
      return {
        moduleData: {
          id: this.id,
          title: this.title

        },
        deleteDialog: false
      };
    },
  methods: {
    async openDialog(){
      this.deleteDialog = true
      console.log(this.title)
      let payload = this.moduleData
      console.log(payload)
      const response = await fetchModuleById(payload);
      console.log(response)
    },
    async onSubmit() {
      try {

        console.log(payload)

        if(response.data.isSuccess){
          this.moduleData.title = response.data.title;
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.moduleData.title = "";
      this.deleteDialog = false;
      this.errorMessage = "";
    }
  },
});
</script>
