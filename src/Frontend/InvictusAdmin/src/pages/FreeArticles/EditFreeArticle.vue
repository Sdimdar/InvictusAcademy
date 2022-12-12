<template>
  <q-card style="min-width: 850px">
    <q-card-section>
      <div class="text-h6 text-center">Редактировать бесплатную статью {{article.title}}</div>
    </q-card-section>
    <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
      <q-card-section>

        <q-input dense v-model="article.title" label="Название статьи" />
        <q-input dense v-model="article.videoLink" label="Ссылка на видео" />
        <q-input dense v-model="article.imageLink" label="Ссылка на картинку статьи" />
        <q-select
          label="Видимость"
          transition-show="scale"
          transition-hide="scale"
          filled
          v-model="article.isVisible"
          :options="options"
          style=""
        />

        <div class="q-pa-md q-gutter-sm">
          <q-editor height="330px"
                    v-model="article.text"
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
              },
              {
                label: $q.lang.editor.defaultFont,
                icon: $q.iconSet.editor.font,
                fixedIcon: true,
                list: 'no-icons',
                options: [
                  'default_font',
                  'arial',
                  'arial_black',
                  'comic_sans',
                  'courier_new',
                  'impact',
                  'lucida_grande',
                  'times_new_roman',
                  'verdana'
                ]
              }
            ],
            ['unordered', 'ordered'],

            ['undo', 'redo'],
            ['viewsource']
          ]"
                    :fonts="{
            arial: 'Arial',
            arial_black: 'Arial Black',
            comic_sans: 'Comic Sans MS',
            courier_new: 'Courier New',
            impact: 'Impact',
            lucida_grande: 'Lucida Grande',
            times_new_roman: 'Times New Roman',
            verdana: 'Verdana'
          }"
          />
        </div>

      </q-card-section>
      <q-card-actions align="right" class="text-primary">
        <q-btn flat type="reset" @click="onReset()" label="Очистить всё" />
        <q-btn flat type="submit" label="Отредактировать" />
      </q-card-actions>
    </q-form>
  </q-card>
</template>

<script>
import {defineComponent, ref} from "vue";
import {createFreeArticle, editFreeArticle, fetchFreeArticle} from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "editFreeArticle",

  props: {
    id: {
      type: Number,
      required: true,
    }
  },
  setup () {
    return {
      options: [
        'true', 'false'
      ]
    }
  },
  data() {
    return {
      shortDescription: "",
      articleId: this.$route.params.id,
      article:{
        title:"",
        videoLink:"",
        imageLink: "",
        id: 0,
        isVisible: 'false',
        text:"добавить текст"
      }
    };
  },
  mounted() {
    this.getFreeArticle();
  },
  methods: {
    async getFreeArticle(){
      try {
        const response = await fetchFreeArticle(this.articleId);
        console.log("FREEARTICLE")
        console.log(response)
        if(response.data.isSuccess){
          this.article.title = response.data.value.title
          this.article.text = response.data.value.text
          this.article.videoLink = response.data.value.videoLink
          this.article.imageLink = response.data.value.imageLink
          this.article.id = response.data.value.id
          this.article.isVisible = response.data.value.isVisible
          notify.showSucsessNotify("Бесплатная статья найдена");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    async onSubmit() {
      try {
        const response = await editFreeArticle(this.article);
        console.log(response)

        if(response.data.isSuccess){
          notify.showSucsessNotify("Бесплатная статья успешно отредактирована");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.article.title = "";
      this.article.imageLink = "";
      this.article.text = "добавить текст";
      this.article.videoLink = "";
    }
  },
});
</script>
