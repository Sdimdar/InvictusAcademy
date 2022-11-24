<template>
  <q-input style="max-width: 200px" dense debounce="300" v-model="filter" placeholder="Поиск">
    <template v-slot:append>
      <q-icon name="search" />
    </template>
  </q-input>
  <div class="q-pa-md">
    <q-table
      ref="tableRef"
      :rows="rows"
      :columns="columns"
      row-key="id"
      v-model:pagination="pagination"
      :loading="loading"
      :filter="filter"
      binary-state-sort
      @request="onRequest"
    >
      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td key="title" :props="props">
            {{ props.row.title }}
          </q-td>
          <q-td key="isVisible" :props="props">
            {{ props.row.isVisible }}

          </q-td>
          <q-btn @click="$router.push(`/admin-panel/editFreeArticle/${props.row.id}`)"  class="nav-button" label="Редактировать" ></q-btn>
        </q-tr>
      </template>

    </q-table>
  </div>
</template>

<script>
import {fetchAllFreeArticles, fetchAllModules, fetchModulesCount, getFreeArticlesCount} from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";
import DeleteModule from 'src/components/Module/DeleteModuleButton.vue'
import UpdateModule from 'src/components/Module/UpdateModuleButton.vue'

const columns = [
  { name: 'title', align: 'center', label: 'Название', field: 'title', sortable: false },
  { name: 'isVisible', align: 'center', label: 'Видимость', field: 'isVisible', sortable: false },
]

export default {
  name: "allFreeArticles",
  data(){
    return{
      title: "",
      isVisible: false,
      id: ""
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
      pageNumber: 1,
      rowsPerPage: 3,
      rowsNumber: 10
    })

    async function onRequest (props) {
      let { pageNumber, rowsPerPage, sortBy, descending } = props.pagination
      let response;

      // пока что запретил показывать All
      //if(rowsPerPage === 0) rowsPerPage = 3

      loading.value = true

      // update rowsCount with appropriate value
      try {
        response = await getFreeArticlesCount();
        console.log("Response on count:")
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
        console.log(pageNumber + " " + rowsPerPage)
        response = await fetchAllFreeArticles(pageNumber, rowsPerPage)

        console.log("Response on data:")
        console.log(response)
        if (response.status === 200) {
          console.log("rows")
          console.log(response.data)
          rows.value.splice(0, rows.value.length, ...response.data.value.freeArticles);
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          return;
        }
      } catch (error) {
        console.log(error.message);
      }

      // don't forget to update local pagination object
      pagination.value.pageNumber = pageNumber
      pagination.value.rowsPerPage = rowsPerPage
      pagination.value.sortBy = sortBy
      pagination.value.descending = descending

      // ...and turn of loading indicator
      loading.value = false
    }

    onMounted(() => {
      // get initial data from server (1st page)
      tableRef.value.requestServerInteraction()
      console.log(rows.value)
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
  methods:{
    // openPage(rowId){
    //   let route = this.$router.resolve({ path: `/admin-panel/moduleDetails/${rowId}` });
    //   this.$router.push(route);
    // },
    // async update(){
    //   //await onRequest({pagination:this.pagination})
    //   //window.location.reload()
    // }
  }
}
</script>
