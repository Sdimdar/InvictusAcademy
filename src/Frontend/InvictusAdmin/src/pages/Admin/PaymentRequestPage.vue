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
    >
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

    let request = [{
    id:1,
    userEmail:'test@email',
    courseId:2,
    courseName:'First Aid',
    ManagerComment:'Wahahhaha',
    purchase: 'HMMM'
},
{
    id:2,
    userEmail:'testSECIND@email',
    courseId:3,
    courseName:'SECOND Aid',
    ManagerComment:'Wahahhaha',
    purchase: 'HMMM' 
}];
let rows = [];

export default{
    name: 'PaymentPage',
    data(){
        return{
            columns,
            rows
        }
    },
    methods: {
        getData(){
            this.rows.push(...request)
            console.log(request)
            console.log(this.rows)
        }
    },
    
}
</script>