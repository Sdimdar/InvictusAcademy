<template>

<div class="q-pa-md" style="max-width: 100%; margin: 0 auto;">
    <q-table
      ref="tableRef"
      :title=myTitle
      :rows="rows"
      :columns="columns"
      row-key="id"
      v-model:pagination="pagination"
      :loading="loading"
      binary-state-sort
      @request="onRequest"
    >
    
    <template v-slot:body="props" >
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
          <q-td key="accept" :props="props" >
            <q-btn
            @click="confirmPayment(props.row.id, rows.indexOf(props.row))"
            color="secondary"
            >Подтвердить оплату</q-btn>
          </q-td>
          <q-td key="rejectReason" :props="props">
              <q-input v-model="props.row.rejectReason" type="text" />
            </q-td>
            <q-td key="reject" :props="props" >
              <q-btn
              @click="reject(props.row.id, props.row.rejectReason,rows.indexOf(props.row))"
              color="deep-orange"
              >Отменить заявку</q-btn>
            </q-td>
        </q-tr>
      </template>
    </q-table>
</div>
</template>

<script>
import {getPaymentsByParams,confirmPaymentById,rejectPayment} from 'boot/axios';
import { ref, onMounted } from 'vue';
import notify from "boot/notifyes";

const columns = [
    {name:"id", align:'center', label:"Номер запроса", field:"id", sortable:true, field: row=> row.id, format:val=>`${val}`, requires:true},
    {name:"userEmail", align:'center', label:"Email пользователя", field:'userEmail', sortable:false},
    {name:"courseId", align:'center', label:"Номер курса", field:"courseId", sortable:false},
    {name:"courseName", align:'center', label:"Название курса", field:"courseName", sortable:false},
    {name:'accept', align: 'center', label: 'Подтверждение оплаты', field: 'accept', sortable: false},
    {name: 'rejectReason', align: 'center', label: 'Причина отмены', field: 'rejectReason', sortable: false},
    {name:'reject', align: 'center', label: 'Отмена заявки', field: 'reject', sortable: false},
    ]


export default{
    name: 'PaymentPage',
    data(){
      return{
        query:{
          status:0
        },
        myTitle : "Открытые заявки",
      }
    },
    setup(){
    let payload = {status:0}
    const tableRef = ref()
    let rows = ref([])
    const filter = ref('')
    const loading = ref(false)
    const pagination = ref({
      sortBy: 'desc',
      descending: false,
      page: 1,
      rowsPerPage: 10,
      rowsNumber: rows.length
    })

async function onRequest (props) {
      console.log(props)
      let { page, rowsPerPage, sortBy, descending } = props.pagination
      let response;
      loading.value = true
      // update rowsCount with appropriate value
      // try {
      //   response = await fetchRequestsCount();
      //   console.log("Response:")
      //   console.log(response)
      //   if (response.data.isSuccess) {
      //     pagination.value.rowsNumber = response.data.value;
      //   }
      //   else {
      //     response.data.errors.forEach(element => { notify.showErrorNotify(element); });
      //     return;
      //   }
      // } catch (error) {
      //   console.log(error.message);
      // }

      // fetch data from "server"
      try {
        
        response = await getPaymentsByParams(payload);
        if (response.data.isSuccess) {
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
        async confirmPayment(id,index){
          try{
            let payload = {paymentId:id}
            let response = await confirmPaymentById(payload);
          if(response.data.isSuccess){
            notify.showSucsessNotify(`Оплата для заявки ${id} подтверждена`)
            delete this.rows[index]
          
          }
          else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          return;
        }
          }
          catch(error){
            console.log(error.message);
          }
        },
        async refreshTable(){
          try {
        let response = await getPaymentsByParams(this.query);
        console.log(response)
        console.log(this.rows.length)
        if (response.data.isSuccess) {

        this.rows.splice(0, this.rows.length, ...response.data.value);
      }
      else {
        response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        return;
      }
    } catch (error) {
      console.log(error.message);
    }
        },
        async reject(id,rejectMessage,index){
            if(rejectMessage === null || rejectMessage.length < 5){
                return notify.showWarningNotify("Заполните причину возврата, не менее 5 символов")
            }
            let payload ={
              paymentId : id,
              rejectReason:rejectMessage
            }
            try{
              let response = await rejectPayment(payload);
              if(response.data.isSuccess){
              notify.showWarningNotify(`Заявка № ${id} отменена`)
              delete this.rows[index]
            }else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          return;
        }
            }
            catch(error){
              console.log(error.message);
            }
          }
    },
    
}
</script>
<style>
.my-select{
  padding: 16 px;
  margin-right: 10px;
  font-size: 18px;
  border-radius: 5px;
  border: 2px solid lightblue;
}
</style>