<template>
  <q-page style="padding: 20px;">
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

          <deleteModule
          :title="props.row.title"
          :id="props.row.id"
          @allModules="setup"/>

                  <q-btn-dropdown color="primary" >
                    <q-list>
                      <q-item clickable v-close-popup>
                        <q-item-section>
                          <q-item-label  @click="getModuleDetails(props.row.id)">Детали</q-item-label>
                        </q-item-section>
                      </q-item>

                      <q-item clickable v-close-popup>
                        <q-item-section>
                          <q-item-label  @click="updateModule(props.row.id)">Редактировать</q-item-label>
                        </q-item-section>
                      </q-item>

                      <q-item clickable v-close-popup>
                        <q-item-section>

                        </q-item-section>
                      </q-item>
                    </q-list>
                  </q-btn-dropdown>
        </q-tr>
      </template>

      </q-table>
    </div>
  </q-page>
</template>

<script>
import { fetchAllModules, fetchModulesCount } from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";
import DeleteModule from 'components/DeleteModuleButton.vue'

const columns = [
  { name: 'title', align: 'center', label: 'Название', field: 'title', sortable: false },
  { name: 'shortDescription', align: 'center', label: 'Краткое описание', field: 'shortDescription', sortable: false },
]

export default {
  name: "ModulesInfoPage",
  components:{
    DeleteModule
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
    console.log(props)
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
        rows.value.splice(0, rows.value.length, ...response.data.value.value);
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
    async getModuleDetails(rowId){
      console.log(rowId)
    },
    async updateModule(rowId){
    console.log(rowId)
    },
    UpdateModulesList(){
      this.onRequest
    }
  }
}
</script>
