<template>
  <div class="q-pa-md" style="max-width: 1000px; margin: 0 auto;">
    <q-table
      ref="tableRef"
      title="Заявки на звонок"
      :rows="rows"
      :columns="columns"
      row-key="id"
      v-model:pagination="pagination"
      :loading="loading"
      binary-state-sort
      @request="onRequest"
    >
      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td key="userName" :props="props">
            {{ props.row.userName }}
          </q-td>
          <q-td key="phoneNumber" :props="props">
            {{ props.row.phoneNumber }}
          </q-td>
          <q-td key="createdDate" :props="props">
            {{ props.row.createdDate }}
          </q-td>
          <q-td key="managerComment" :props="props">
            <q-input v-model="props.row.managerComment" type="text" />
          </q-td>
          <q-td key="wasCalled" :props="props">
            <q-toggle
              v-model="props.row.wasCalled"
              color="green"
            />
          </q-td>
        </q-tr>
      </template>
    </q-table>
  </div>
</template>
  
<script>
import { fetchAllRequest, fetchRequestsCount } from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";

const columns = [
  { name: 'userName', align: 'center', label: 'Имя', field: 'userName', sortable: false },
  { name: 'phoneNumber', align: 'center', label: 'Телефон', field: 'phoneNumber', sortable: false },
  { name: 'createdDate', align: 'center', label: 'Дата заявки', field: 'createdDate', sortable: true },
  { name: 'managerComment', align: 'center', label: 'Комментарий', field: 'managerComment', sortable: false },
  { name: 'wasCalled', align: 'center', label: 'Обзвонен?', field: 'wasCalled', sortable: true }
]

export default {
  name: "RequestInfoPage",
  setup(){
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

    async function onRequest (props) {
      console.log(props)
      let { page, rowsPerPage, sortBy, descending } = props.pagination
      let response;
      
      // пока что запретил показывать All
      if(rowsPerPage === 0) rowsPerPage = 3

      loading.value = true

      // update rowsCount with appropriate value
      try {
        response = await fetchRequestsCount();
        console.log("Response:")
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
        response = await fetchAllRequest(page, rowsPerPage)
        console.log("Response:")
        console.log(response)
        if (response.data.isSuccess) {
          rows.value.splice(0, rows.value.length, ...response.data.value.requests);
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
  }
}
</script>
