<template>
  <q-btn color="primary" label="Редактировать урок" @click="getArticleData" />

  <q-dialog v-model="editDialog">
    <q-card style="min-width: 850px">
      <q-card-section>
        <div class="text-h6 text-center">Редактировать</div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section class="q-pt-none">

          <q-input dense v-model="editData.title" label="Название статьи" />
          <q-input dense v-model="editData.order"  type="number" label="Порядковый номер в модуле" />
          <q-input dense v-model="editData.videoLink" label="Ссылка на видео" />

        <div class="q-pa-md q-gutter-sm">
        <q-editor
          v-model="editData.text"
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
        />
        </div>

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
import { fetchModuleById } from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "edit-button",
  props: {
      order: {
        type: Number,
        required: true,
      },
      id: {
        type: Number,
        required: true,
      },
    },
  data() {
    return {
      articles: [],
      article: "",
      editData: {
        title:"",
        order:"",
        videoLink:"",
        text:""
      },
      editDialog: false,
    };
  },
  methods: {
    async getArticleData (){
      this.editDialog = true
      const response = await fetchModuleById(this.id);
      this.title = response.data.value.title
      this.articles = response.data.value.articles
      this.article = this.articles.find(a => a.order === this.order)
      console.log(this.article)
      this.editData.title = this.article.title
      this.editData.order = this.article.order
      this.editData.videoLink = this.article.videoLink
      this.editData.text = this.article.text
    },
    async onSubmit() {

    },
    onReset() {
      this.editDialog = false;
      this.errorMessage = "";
      this.editData.title = "";
      this.editData.order = "";
      this.editData.videoLink = "";
      this.editData.text = "";
    }
  },
});
</script>
