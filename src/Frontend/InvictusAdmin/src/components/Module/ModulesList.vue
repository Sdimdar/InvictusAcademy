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
          <q-td key="shortDescription" :props="props">
            {{ props.row.shortDescription }}

          </q-td>
          <q-btn-dropdown color="primary" >
            <q-list>

              <q-btn :class="$attrs.class" label="Детали" @click="openPage(props.row.id)" />

              <updateModule
                      :title="props.row.title"
                      :id="props.row.id"
                      @updateModule="update"/>

               <deleteModule
                      :title="props.row.title"
                      :id="props.row.id"
                      @allModules="update"/>
            </q-list>
            </q-btn-dropdown>
        </q-tr>
      </template>

      </q-table>
    </div>
</template>

<script>
import { fetchAllModules, fetchModulesCount } from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";
import DeleteModule from 'src/components/Module/DeleteModuleButton.vue'
import UpdateModule from 'src/components/Module/UpdateModuleButton.vue'

const columns = [
  { name: 'title', align: 'center', label: 'Название', field: 'title', sortable: false },
  { name: 'shortDescription', align: 'center', label: 'Краткое описание', field: 'shortDescription', sortable: false },
]

export default {
  name: "ModulesInfoPage",
  components:{
    DeleteModule,
    UpdateModule
  },
  data(){
    return{
      title: "",
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
        response = await fetchModulesCount();
        console.log("Response on count:")
        console.log(response)
        if (response.data.isSuccess) {
          pagination.value.rowsNumber = response.data.value.value;
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
        response = await fetchAllModules(pageNumber, rowsPerPage)

        console.log("Response on data:")
        console.log(response)
        if (response.status === 200) {
          console.log("rows")
          console.log(response.data)
          rows.value.splice(0, rows.value.length, ...response.data.value);
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
    openPage(rowId){
      let route = this.$router.resolve({ path: `/admin-panel/moduleDetails/${rowId}` });
      this.$router.push(route);
    },
    async update(){
      //await onRequest({pagination:this.pagination})
      //window.location.reload()
    }
  }
}
</script>
