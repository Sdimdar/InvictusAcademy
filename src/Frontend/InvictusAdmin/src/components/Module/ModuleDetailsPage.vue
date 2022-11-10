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
          <q-btn to="/admin-panel/content" class="nav-button" label="Назад"/>

        </div>
      </template>

      <template v-slot:after>
          <div class="q-pa-md">
            <div class="q-gutter-y-md" style="max-width: 1000px">
        <q-tabs
          align="left"
          inline-label
          class="bg-primary text-white shadow-2"
        >
          <q-route-tab icon="book" label="Разделы модуля" />

        </q-tabs>
    </div>

    <q-page-container>

      <createArticle  class="nav-button"
      :title="title"
      :id="id"
      :articles="articles"
      @addArticle="getModuleData"
      />

      <div class="q-pa-md">
        <q-table
         style="max-width: 800px"
          title="Разделы модуля"
          :rows="articles"
          row-key="title"
        />
      </div>

      </q-page-container>

        </div>
      </template>
    </q-splitter>
  </div>

</template>

<script>
import { defineComponent } from "vue";
import { updateModule, fetchModuleById} from "boot/axios";
import notify from "boot/notifyes";
import CreateArticle from 'src/components/Module/CreateArticleButton.vue'

export default defineComponent({
  name: "detailsPage",
  components:{
    CreateArticle
  },
  data() {
      return {
        id: this.$route.params.id,
        title:"",
        shortDescription: "",
        articles: []
      };
    },
  methods: {
    async getModuleData(){
      const response = await fetchModuleById(this.id);
      console.log(response)
      this.shortDescription = response.data.value.value.shortDescription
      this.title = response.data.value.value.title
      this.articles = response.data.value.value.articles
      console.log(this.articles)
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
