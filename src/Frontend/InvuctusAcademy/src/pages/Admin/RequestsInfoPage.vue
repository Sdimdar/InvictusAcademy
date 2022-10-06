<template>
    <table class="table">
      <input v-model="filterString" />
      <button @click="getRequestsData">search</button>
      <tr>
        <th>Имя</th><th>Телефон</th><th>Дата заявки</th><th>Комментарий</th><th>Звонили?</th>
      </tr>
      <tr v-for="request in requests" :key="request">
        <td>{{request.userName}}</td>
        <td>{{request.phoneNumber}}</td>
        <td>{{request.createdDate}}</td>
        <td><input type="text" :value="request.managerComment"></td>
        <td>{{request.wasCalled}}<q-checkbox v-model="teal" color="teal" /></td>
      </tr>
    </table>
    <div>
      <!-- <div class="pagesBox" v-for="pageN in pageVm.totalPages" :key="pageN" @click="rePage(pageN)">
        <button :class="{'current-page': page === pageN}">{{pageN}}</button>
      </div> -->
    </div>
  </template>
  
  <script>
  import { fetchAllRequest } from "boot/axios";
  import notify from "boot/notifyes";
  
  export default {
    name: "RequestInfoPage",
    data(){
      return{
        teal: true,
        requests: [],
        page: 1,
        filterString: "",
        
        pageVm: {
          // totalPages: 0,
          pageNumber: 0
        }
      }
    },
    methods:{
      async getRequestsData(){
        try {
          const response = await fetchAllRequest(this.filterString, this.page);
          console.log("Response:")
          console.log(response)
          if (response.data.isSuccess) {
            this.requests = response.data.value.requests
            this.pageVm = response.data.value.pageVm
            if(this.requests.length == 0) 
            {
              notify.showWarningNotify(`Не найдено ни одной заявки по запросу : ${this.filterString}`);
            }
            // console.log(`Pages count: ${this.pageVm.totalPages}`)
          }
          else{
            response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          }
        }
        catch (e){
          console.log(e.message)
        }
      },
      rePage(pageN){
        this.page = pageN
        console.log(this.page)
        this.getRequestsData()
      },
    },
    updated() {
      // this.getRequestsData()
    },
    mounted() {
      this.getRequestsData()
    }
  }
  </script>
  
  <style scoped>
    table{
      font-family: sans-serif;
      font-size: 14px;
      border-collapse: collapse;
      text-align: center;
    }
    th{
      background: #afcde7;
      color: white;
      padding: 10px 20px;
      border-style: solid;
      border-width: 0 1px 1px 0;
    }
    td{
      border-style: solid;
      border-width: 0 1px 1px 0;
      border-color: white;
      background: #d8e6f3;
    }
    .pagesBox{
      display: inline-block;
      margin-right: 2px;
    }
    .current-page{
      background: blue;
      color: white;
    }
  </style>
  