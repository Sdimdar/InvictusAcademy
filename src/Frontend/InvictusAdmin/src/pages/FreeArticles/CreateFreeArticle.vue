<template>
    <q-card style="min-width: 850px">
      <q-card-section>
        <div class="text-h6 text-center">Добавить новую бесплатную статью</div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>

          <q-input dense v-model="newArticle.title" label="Название статьи" />
          <q-input dense v-model="newArticle.videoLink" label="Ссылка на видео" />

          <div class="q-pa-md q-gutter-sm">
            <q-editor height="330px"
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
          <q-btn flat type="submit" label="Добавить" />
        </q-card-actions>
      </q-form>
    </q-card>
</template>

<script>
import { defineComponent } from "vue";
import {createFreeArticle} from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  name: "createFreeArticle",

  props: {
    id: {
      type: Number,
      required: true,
    }
  },
  data() {
    return {
      shortDescription: "",
      newArticle:{
        title:"",
        videoLink:"",
        text:"добавить текст"
      }
    };
  },
  methods: {
    async onSubmit() {
      try {
        const response = await createFreeArticle(this.newArticle);
        console.log(response)

        if(response.data.isSuccess){
          notify.showSucsessNotify("Бесплатная статья успешно добавлена");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.newArticle.title = "";
      this.newArticle.text = "добавить текст";
      this.newArticle.videoLink = "";
    }
  },
});
</script>
