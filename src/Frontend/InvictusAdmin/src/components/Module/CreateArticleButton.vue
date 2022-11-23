<template>
  <q-btn :class="$attrs.class" label="+ Добавить " @click="openDialog()" />

  <q-dialog v-model="newArticleDialog">
    <q-card style="min-width: 850px">
      <q-card-section>
        <div class="text-h6 text-center">Добавить новую статью в "{{ title }}"</div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>

          <q-input dense v-model="newArticle.title" label="Название статьи"
          lazy-rules
          :rules="[
            (val) => (val && val.length > 0) || 'Поле не должно быть пустым'
          ]" />

          <q-input dense v-model="newArticle.order"
          type="number"
          label="Порядковый номер в модуле"
          lazy-rules
          :rules="[
                (val) => (val && val.length > 0) || 'Поле не должно быть пустым',
                (val) => validateOrder(val)|| 'Урок с таким номер уже существует'
              ]"/>

          <q-input dense v-model="newArticle.videoLink" label="Ссылка на видео"
          lazy-rules
          :rules="[
                (val) => (val && val.length > 0) || 'Поле не должно быть пустым'
              ]"/>

      <div class="q-pa-md q-gutter-sm">
        <q-editor
          v-model="newArticle.text"
          :dense="$q.screen.lt.md"
          :toolbar="[
            [
              {
                label: $q.lang.editor.align,
                icon: $q.iconSet.editor.align,
                fixedLabel: true,
                list: 'only-icons',
                options: ['left', 'center', 'right', 'justify']
              }
            ],
            ['bold', 'italic', 'strike', 'underline'],
            ['token', 'hr', 'link', 'custom_btn'],
            [
              {
                label: $q.lang.editor.fontSize,
                icon: $q.iconSet.editor.fontSize,
                fixedLabel: true,
                fixedIcon: true,
                list: 'no-icons',
                options: [
                  'size-1',
                  'size-2',
                  'size-3',
                  'size-4',
                  'size-5',
                  'size-6',
                  'size-7'
                ]
              }
            ],
            ['unordered', 'ordered'],

            ['undo', 'redo'],
            ['viewsource']
          ]"
          lazy-rules
          :rules="[
                (val) => (val && val.length > 0) || 'Поле не должно быть пустым'
              ]"
        />
        </div>

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
          text:"добавить текст"
        }
      };
    },
  methods: {
    async openDialog(){
      this.newArticleDialog = true
      const response = await fetchModuleById(this.id);
      console.log(response)
      this.shortDescription = response.data.value.shortDescription
      this.title = response.data.value.title
    },
    async onSubmit() {
      this.articles.push(this.newArticle)
      try {
        let payload = {
          moduleId: this.id,
          articles: this.articles
        }
        const response = await addNewArticle(payload);
        console.log(payload)

        if(response.data.isSuccess){
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
    },
    validateOrder(value) {
      if(this.articles.find(a => a.order === Number(value))){
        return false
      }
      return true
      },
  },
});
</script>
