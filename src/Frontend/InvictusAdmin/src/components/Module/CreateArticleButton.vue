<template>
  <q-btn :class="$attrs.class" label="+ Добавить " @click="openDialog()" />

  <q-dialog v-model="newArticleDialog">
    <q-card style="min-width: 850px">
      <q-card-section>
        <div class="text-h6 text-center">Добавить новый раздел в "{{ title }}"</div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>

          <q-input dense v-model="newArticle.title" label="Название раздела" />
          <q-input dense v-model="newArticle.order"  type="number" label="Порядковый номер в модуле" />
          <q-input dense v-model="newArticle.videoLink" label="Ссылка на видео" />
          <q-input dense v-model="newArticle.text" type="textarea" label="Нужный текст" />

        </q-card-section>
        <q-card-actions align="right" class="text-primary">
          <q-btn flat type="reset" label="Отмена" />
          <q-btn flat type="submit" label="Добавить" />
        </q-card-actions>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { addNewArticle, updateModule, fetchModuleById} from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "addArticle",
  props: {
      title: {
        type: String,
        required: true,
      },
      id: {
        type: Number,
        required: true,
      },
      articles: {
        type: Array,
        required: true,
      },
    },
  data() {
      return {
        newArticleDialog: false,
        title:"",
        shortDescription: "",
        newArticle:{
          title:"",
          order:"",
          videoLink:"",
          text:""
        }
      };
    },
  methods: {
    async openDialog(){
      this.newArticleDialog = true
      const response = await fetchModuleById(this.id);
      console.log(response)
      this.shortDescription = response.data.value.value.shortDescription
      this.title = response.data.value.value.title
    },
    async onSubmit() {
      this.articles.push(this.newArticle)
      try {
        let payload = {
          moduleId: this.id,
          articles: this.articles
        }
        const response = await addNewArticle(payload);
        console.log(response)

        if(response.data.value.isSuccess){
          this.newArticleDialog = false
          this.$emit("addArticle");
          notify.showSucsessNotify("Раздел успешно добавлен");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.newArticleDialog = false;
      this.shortDescription = "";
      this.articles.title = "";
      this.articles.order = "";
      this.articles.videoLink = "";
      this.articles.text = "";
      this.errorMessage = "";
    }
  },
});
</script>
