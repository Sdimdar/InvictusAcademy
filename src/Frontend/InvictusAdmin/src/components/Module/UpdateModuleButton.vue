<template>
  <q-btn :class="$attrs.class" label="Редактировать" @click="openDialog()" />

  <q-dialog v-model="updateDialog">
    <q-card style="min-width: 850px">
      <q-card-section>
        <div class="text-h6 text-center">Редактировать модуль</div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>
          <q-input dense v-model="title" label="Название" />
          <q-input dense v-model="shortDescription" type="textarea" label="Краткое описание" />

        </q-card-section>
        <q-card-actions align="right" class="text-primary">
          <q-btn flat type="reset" label="Отмена" />
          <q-btn flat type="submit" label="Сохранить" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { updateModule, fetchModuleById} from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "updateModule",
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
        updateDialog: false,
        title:"",
        shortDescription: ""
      };
    },
  methods: {
    async openDialog(){
      this.updateDialog = true
      const response = await fetchModuleById(this.id);
      console.log(response)
      this.shortDescription = response.data.value.value.shortDescription
      this.title = response.data.value.value.title
    },
    async onSubmit() {
      try {
        let payload={
          Id: this.id,
          Title: this.title,
          ShortDescription: this.shortDescription
        }
        const response = await updateModule(payload);
        console.log(response)

        if(response.data.value.isSuccess){
          this.updateDialog = false
          this.$emit("updateModule");
          notify.showSucsessNotify("Изменения сохранены");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.title = "";
      this.shortDescription = "";
      this.updateDialog = false;
      this.errorMessage = "";
    }
  },
});
</script>
