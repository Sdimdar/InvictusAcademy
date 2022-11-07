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
        <template v-slot:top-right>
          <q-input borderless dense debounce="300" v-model="filter" placeholder="Поиск">
            <template v-slot:append>
              <q-icon name="search" />
            </template>
          </q-input>
        </template>
      </q-table>
    </div>
  </q-page>
</template>

<script>
import { fetchAllModules } from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";

const columns = [
  { name: 'title', align: 'center', label: 'Название', field: 'title', sortable: false },
  { name: 'shortDescription', align: 'center', label: 'Описание', field: 'shortDescription', sortable: false }
]

export default {
  name: "UsersInfoPage",
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
      const filter = props.filter

      // пока что запретил показывать All
      if(rowsPerPage === 0) rowsPerPage = 3

      loading.value = true

      try {
        const response = await fetchAllModules();
        loading.value = false

        console.log("Response:")
        console.log(response)

        if (response.data.isSuccess) {
          // update rowsCount with appropriate value
          pagination.value.rowsNumber = response.data.value.pageVm.totalPages * rowsPerPage

          // fetch data from "server"
          const returnedData = response.data.value.users

          // clear out existing data and add new
          rows.value.splice(0, rows.value.length, ...returnedData)

          // don't forget to update local pagination object
          pagination.value.pageNumber = pageNumber
          pagination.value.rowsPerPage = rowsPerPage
          pagination.value.sortBy = sortBy
          pagination.value.descending = descending
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      }
      catch (e) {
        console.log(e.message)
      }
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
  }
}
</script>
