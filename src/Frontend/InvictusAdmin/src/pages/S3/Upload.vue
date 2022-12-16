<template>
  <div class="uploadPage">
    <div class="q-pa-md">
      <q-table ref="tableRef" title="Files" :rows="rows" :columns="columns" row-key="id" v-model:pagination="pagination"
        :loading="loading" :filter="filter" binary-state-sort @request="onRequest">
        <template v-slot:top-right>
          <q-input borderless dense debounce="300" v-model="filter" placeholder="Поиск">
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
        </template>
      </q-table>
    </div>
    <div class="q-pa-md">
      <div class="q-gutter-sm row items-start">
        <q-uploader :url="cloudStorageUplodadURL" color="teal" flat bordered
          style="max-width: 300px" />
      </div>
    </div>
  </div>

</template>

<script>
import {fetchFilesData, fetchFilesCount, fetchAllFreeArticles} from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";
const columns = [
  { name: 'fileName', align: 'left', label: 'Имя файла', field: 'fileName', sortable: false },
  { name: 'filePath', align: 'left', label: 'Ссылка', field: 'filePath', sortable: false },
  { name: 'createdDate', align: 'left', label: 'Дата создания', field: 'createdDate', sortable: false },
  //{ name: 'lastModifiedDate', align: 'left', label: 'lastModifiedDate', field: 'lastModifiedDate', sortable: false }
]


export default {
  name: "Upload",
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

      // пока что запретил показывать All
      //if(rowsPerPage === 0) rowsPerPage = 3

      loading.value = true

      // update rowsCount with appropriate value
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
        if(rowsPerPage === 0){
          response = await fetchFilesData(0, rowsPerPage)
        }
        else{
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
  data() {
    return {
      cloudStorageUplodadURL: `${process.env.CLOUD_STORAGE_URL}/CloudStorage/UploadFile`
    }
  },
  mounted() {
    console.log(process.env.CLOUD_STORAGE_URL);
  }
}
</script>

<style scoped>
.uploadPage {
  display: flex;
  flex-direction: row;
}
</style>
