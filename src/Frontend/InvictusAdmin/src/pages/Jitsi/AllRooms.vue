<template>
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
          <q-td key="name" :props="props">
            {{ props.row.name }}
          </q-td>
          <q-td key="address" :props="props">
            {{ props.row.address }}
          </q-td>
          <q-td key="isOpened" :props="props">
            <q-btn @click="closeRoom(props.row.address)"  class="nav-button" label="Закрыть" ></q-btn>
          </q-td>
<!--          <q-btn @click="$router.push(`/admin-panel/room/${props.row.address}`)"  class="nav-button" label="Перейти" ></q-btn>-->
          <q-btn v-bind:to="'room/'+props.row.address+'/'+props.row.name"  class="nav-button" label="Перейти" ></q-btn>
        </q-tr>
      </template>

    </q-table>
  </div>
</template>

<script>
import {
  closeRoom,
  getAllStreamingRooms,
  getCountStreamingRooms
} from "boot/axios";
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";

const columns = [
  { name: 'name', align: 'center', label: 'Название', field: 'name', sortable: false },
  { name: 'address', align: 'center', label: 'Адрес', field: 'address', sortable: false },
  { name: 'isOpened', align: 'center', label: 'Открыто', field: 'isOpened', sortable: false },
]

export default {
  name: "allRooms",
  data(){
    return{
      name: "",
      isOpened: false,
      id: "",
      address: ""
    }
  },
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
      //if(rowsPerPage === 0) rowsPerPage = 3

      loading.value = true

      // update rowsCount with appropriate value
      try {
        response = await getCountStreamingRooms();
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
          response = await getAllStreamingRooms(0, rowsPerPage)
        }
        else{
          response = await getAllStreamingRooms(page, rowsPerPage)
        }
        if (response.data.isSuccess) {
          rows.value.splice(0, rows.value.length, ...response.data.value.streamingRooms);
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
  methods:{
    async closeRoom(address) {
      console.log("ADDRESS")
      console.log(address)
      try {
        const response = await closeRoom(address);
        console.log(address)
        if (response.data.isSuccess) {
          this.responceDataCourse = response.data.value;
          notify.showSucsessNotify("Комната закрыта");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    }
  }
}
</script>
