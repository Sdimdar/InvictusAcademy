<template>
  <div style="padding-left:16px">
    <p v-if="(id !=0)">История для запроса <strong>№{{id}}</strong></p>
    <p v-else>История для менеджера: <strong>{{email}}</strong></p>
  </div>
  
<div class="q-pa-md" style="max-width: 100%; margin: 0 auto;">
<table class="styled-table">
  <thead>
    <tr>
      <th>Номер запроса</th>
      <th>Email пользователя</th>
      <th>Номер курса</th>
      <th>Название курса</th>
      <th>Статус заявки</th>
      <th>Причина отмены</th>
      <th>Кто подтвердил</th>
      <th>Дата создания</th>
    </tr>
  </thead>
  
  <tbody>
  <template v-for="item in rows">
  <tr>
    <td>{{item.paymentId}}</td>
    <td>{{item.userEmail}}</td>
    <td>{{item.courseId}}</td>
    <td>{{item.courseName}}</td>
    <td>
      <template v-if="(item.paymentState === 0)">Открытая заявка</template>
      <template v-if="(item.paymentState === 1)">Оплачено</template>
      <template v-if="(item.paymentState === 2)">Отмененная заявка</template>
      <template v-if="(item.paymentState === 3)">Возврат средств</template>
    </td>
    <td>{{item.rejectReason}}</td>
    <td>{{item.modifyAdminEmail}}</td>
    <td>{{item.createdDate}}</td>
  </tr>
  </template>
</tbody>
</table>
</div>
</template>
    
    <script>
    import { getHistoryById,getHistoryByName} from 'boot/axios';
    import { ref, onMounted } from 'vue';
    import notify from "boot/notifyes";
    // import { numberLiteralTypeAnnotation } from '@babel/types';

      
    const columns = [
        {name:"id", align:'center', label:"Номер запроса", field:"id", sortable:true, field: row=> row.id, format:val=>`${val}`, requires:true},
        {name:"userEmail", align:'center', label:"Email пользователя", field:'userEmail', sortable:false},
        {name:"courseId", align:'center', label:"Номер курса", field:"courseId", sortable:false},
        {name:"courseName", align:'center', label:"Название курса", field:"courseName", sortable:false},
        {name:'paymentState', align: 'center', label: 'Статус заявки', field: 'paymentState', sortable: false},
        {name:'rejectReason', align: 'center', label: 'Причина отмены', field: 'rejectReason', sortable: false},
        {name:'modifyAdminEmail', align: 'center', label: 'Кто подтвердил', field: 'modifyAdminEmail', sortable: false},
        {name:'сreatedDate', align: 'center', label: 'Дата создания', field: 'сreatedDate', sortable: false}
        ]
    
    
    export default{
    props:{
      id:{type:Number},
      email:{type:String}
    },
    name: "HistoryPage",
    data() {
        return {
            myTitle: "История операций",
            rows:[]
        };
    },
    methods:{
      async  getById() {
            let response;
            // fetch data from "server"
              try {
                console.log(this.id)
              let payload = {
                paymentId:this.id
              }
                response = await getHistoryById(payload);
                if (response.data.isSuccess) {
                  console.log(response.data)
                    this.rows.push(...response.data.value)
                    console.log(this.rows)
                }
                else {
                    response.data.errors.forEach(element => { notify.showErrorNotify(element); });
                    return;
                }
            }
            catch (error) {
                console.log(error.message);
            }
        },
        async getByName(){
      let response;
      try {
                response = await getHistoryByName(this.email);
                if (response.data.isSuccess) {
                  console.log(response.data)
                    this.rows.push(...response.data.value)
                    console.log(this.rows)
                }
                else {
                    response.data.errors.forEach(element => { notify.showErrorNotify(element); });
                    return;
                }
            }
            catch (error) {
                console.log(error.message);
            }
    },
    },
    
    mounted(){
      if(this.id != 0){
        this.getById()
      }else{
        this.getByName()
      }
    }
  
}
    </script>
<style scoped>
    .styled-table {
    border-collapse: collapse;
    margin: 25px 0;
    font-size: 0.9em;
    font-family: sans-serif;
    min-width: 400px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
}
.styled-table thead tr {
    background-color: #009879;
    color: #ffffff;
    text-align: left;
}
.styled-table th,
.styled-table td {
    padding: 12px 15px;
}
.styled-table tbody tr {
    border-bottom: 1px solid #dddddd;
}

.styled-table tbody tr:nth-of-type(even) {
    background-color: #f3f3f3;
}

.styled-table tbody tr:last-of-type {
    border-bottom: 2px solid #009879;
}
.styled-table tbody tr.active-row {
    font-weight: bold;
    color: #009879;
}
</style>
  