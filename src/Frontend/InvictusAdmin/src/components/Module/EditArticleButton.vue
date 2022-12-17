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
          <q-input dense v-model="editData.order" type="number" label="Порядковый номер в модуле" />
          <q-input dense v-model="editData.videoLink" label="Ссылка на видео" />
          <div class="q-pa-md">
            <q-table ref="tableRef" title="Поиск ссылок по имени файла (нужно начать вводить)>" :rows="rows" :columns="columns" row-key="id"
              v-model:pagination="pagination" :loading="loading" :filter="filter" binary-state-sort
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

          <div class="q-pa-md q-gutter-sm">
            <q-editor v-model="editData.text" :dense="$q.screen.lt.md" :toolbar="[
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
            ]" />
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
import { defineComponent, ref, onMounted } from "vue";
import { fetchModuleById, fetchFilesData, fetchFilesCount } from "boot/axios";
import notify from "boot/notifyes";
const columns = [
  { name: 'filePath', align: 'left', label: 'Ссылка', field: 'filePath', sortable: false }
]
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
        title: "",
        order: "",
        videoLink: "",
        text: ""
      },
      editDialog: false,
    };
  },
  methods: {
    async getArticleData() {
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
  setup() {
    const tableRef = ref()
    const rows = ref([])
    const filter = ref('')
    const loading = ref(false)
    const pagination = ref({
      sortBy: 'desc',
      descending: false,
      page: 1,
      rowsPerPage: 10,
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
      //tableRef.value.requestServerInteraction()
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
  }
});
</script>
