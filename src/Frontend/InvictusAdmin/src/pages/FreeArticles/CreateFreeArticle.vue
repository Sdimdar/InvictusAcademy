<template>
  <q-card style="min-width: 850px">
    <q-card-section>
      <div class="text-h6 text-center">Добавить новую бесплатную статью</div>
    </q-card-section>
    <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
      <q-card-section>

        <q-input dense v-model="newArticle.title" label="Название статьи" />
        <q-input dense v-model="newArticle.videoLink" label="Ссылка на видео" />
        <q-input dense v-model="newArticle.imageLink" label="Ссылка на картинку статьи" />
        <div class="uploadPage">
          <div class="q-pa-md">
            <q-table ref="tableRef" title="Поиск ссылок по имени файла --------->" :rows="rows" :columns="columns"
              row-key="id" v-model:pagination="pagination" :loading="loading" :filter="filter" binary-state-sort
              @request="onRequest">
              <template v-slot:top-right>
                <q-input borderless dense debounce="300" v-model="filter" placeholder="Поиск">
                  <template v-slot:append>
                    <q-icon name="search" />
                  </template>
                </q-input>
              </template>
            </q-table>
          </div>
        </div>
        <div class="q-pa-md q-gutter-sm">
          <q-editor height="330px" v-model="newArticle.text" :dense="$q.screen.lt.md" :toolbar="[
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
          ]" :fonts="{
  arial: 'Arial',
  arial_black: 'Arial Black',
  comic_sans: 'Comic Sans MS',
  courier_new: 'Courier New',
  impact: 'Impact',
  lucida_grande: 'Lucida Grande',
  times_new_roman: 'Times New Roman',
  verdana: 'Verdana'
}" />
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
import { defineComponent, ref, onMounted } from "vue";
import { createFreeArticle, fetchFilesData, fetchFilesCount } from "boot/axios";
import notify from "boot/notifyes";
const columns = [
  { name: 'filePath', align: 'left', label: 'Ссылка', field: 'filePath', sortable: false }
]
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
      newArticle: {
        title: "",
        videoLink: "",
        imageLink: "",
        text: "добавить текст"
      }
    };
  },
  setup() {
    const tableRef = ref()
    const rows = ref([])
    const filter = ref('')
    const loading = ref(false)
    const pagination = ref({
      sortBy: 'desc',
      descending: false,
      page: 1,
      rowsPerPage: 3,
      rowsNumber: 10
    })

    async function onRequest(props) {
      console.log(props)
      let { page, rowsPerPage, sortBy, descending } = props.pagination
      let response;

      loading.value = true
      try {
        response = await fetchFilesCount();
        console.log("Files on count:")
        console.log(response)
        if (response.data.isSuccess) {
          pagination.value.rowsNumber = response.data.value;
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          return;
        }
      } catch (error) {
        console.log(error.message);
      }

      // fetch data from "server"
      try {
        console.log(page + " " + rowsPerPage)
        if (rowsPerPage === 0) {
          response = await fetchFilesData(0, rowsPerPage)
        }
        else {
          response = await fetchFilesData(page, rowsPerPage, props.filter)
        }
        console.log("Files on data:")
        console.log(response)
        if (response.status === 200) {
          rows.value.splice(0, rows.value.length, ...response.data.value.files);
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          return;
        }
      } catch (error) {
        console.log(error.message);
      }

      // don't forget to update local pagination object
      pagination.value.page = page
      pagination.value.rowsPerPage = rowsPerPage
      pagination.value.sortBy = sortBy
      pagination.value.descending = descending

      // ...and turn of loading indicator
      loading.value = false
    }

    onMounted(() => {
      // get initial data from server (1st page)
      tableRef.value.requestServerInteraction()
    })

    return {
      tableRef,
      filter,
      loading,
      pagination,
      columns,
      rows,
      onRequest
    }
  },
  methods: {
    async onSubmit() {
      try {
        const response = await createFreeArticle(this.newArticle);
        console.log(response)

        if (response.data.isSuccess) {
          notify.showSucsessNotify("Бесплатная статья успешно добавлена");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.newArticle.title = "";
      this.newArticle.imageLink = "";
      this.newArticle.text = "добавить текст";
      this.newArticle.videoLink = "";
    }
  },
});
</script>
