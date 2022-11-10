<template>
<div>
    <q-splitter
      v-model="splitterModel"
      unit="px"
      style="height: 800px"
    >
      <template v-slot:before>
        <div class="q-pa-md">
          <div class="text-h6 q-mb-md">{{ title }}</div>
          <div class="text-h8 q-mb-md">{{ shortDescription }}</div>

        </div>
      </template>

      <template v-slot:after>
          <div class="q-pa-md">
            <div class="q-gutter-y-md" style="max-width: 1000px">
        <q-tabs
          v-model="tab"
          align="left"
          inline-label
          class="bg-primary text-white shadow-2"
        >
          <q-route-tab icon="book" label="Разделы модуля" />
        </q-tabs>
    </div>

    <q-page-container>
      <createArticule-button  class="nav-button" />
      </q-page-container>

        </div>
      </template>
    </q-splitter>
  </div>

</template>

<script>
import { defineComponent } from "vue";
import { updateMule, fetchModuleById} from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "detailsPage",
  data() {
      return {
        id: this.$route.params.id,
        title:"",
        shortDescription: ""
      };
    },
  methods: {
    async getModuleData(){
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
  beforeMount(){
    this.getModuleData()
 },
});
</script>
