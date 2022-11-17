<template>
    <q-btn @click="getData" >Обновить</q-btn>
<div class="q-pa-md" style="max-width: 100%; margin: 0 auto;">
    <q-table
      ref="tableRef"
      title="Активные заявки на оплату"
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
          <q-td key="id" :props="props">
            {{ props.row.id }}
          </q-td>
          <q-td key="userEmail" :props="props">
            {{ props.row.userEmail }}
          </q-td>
          <q-td key="courseId" :props="props">
            {{ props.row.courseId }}
          </q-td>
          <q-td key="courseName" :props="props">
            {{ props.row.courseName }}
          </q-td>
          <q-td key="managerComment" :props="props" v-if="props.row.purchase">
            <q-input v-model="props.row.managerComment" type="text" 
            @keyup.enter='managerCommentHadler(props.row.id, props.row.managerComment, $event)'/>
          </q-td>
          <q-td key="purchase" :props="props" v-if="!props.row.purchase">
            <q-btn
            v-model="props.row.purchase"
            @click="changeCalledHadler(props.row.id)"
            color="secondary"
            >Подтвердить оплату</q-btn>
          </q-td>
          <q-td key="purchase" :props="props" v-if="props.row.purchase">
            <q-btn
            v-model="props.row.purchase"
            @click="changeCalledHadler(props.row.id)"
            color="deep-orange"
            >Отменить оплату</q-btn>
          </q-td>
        </q-tr>
      </template>
    </q-table>
</div>
</template>

<script>

const columns = [
    {name:"id", align:'center', label:"Номер запроса", field:"id", sortable:true, field: row=> row.id, format:val=>`${val}`, requires:true},
    {name:"userEmail", align:'center', label:"Email пользователя", field:'userEmail', sortable:false},
    {name:"courseId", align:'center', label:"Номер курса", field:"courseId", sortable:false},
    {name:"courseName", align:'center', label:"Название курса", field:"courseName", sortable:false},
    {name: 'managerComment', align: 'center', label: 'Комментарий', field: 'managerComment', sortable: false},
    {name:'purchase', align: 'center', label: 'Подтверждение оплаты', field: 'purshase', sortable: false},
    ]


export default{
    name: 'PaymentPage',
    setup(){
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

async function onRequest (props) {
      console.log(props)
      let { page, rowsPerPage, sortBy, descending } = props.pagination
      let response;
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
        
          response = await fetchAllRequest(page, rowsPerPage)
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
},methods: {
        
    },
    
}
</script>