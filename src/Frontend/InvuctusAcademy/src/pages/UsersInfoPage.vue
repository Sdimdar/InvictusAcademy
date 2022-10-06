<template>
  <table class="table">
    <input v-model="filterString" />
    <button @click="getUsersData">search</button>
    <tr>
      <th>Имя</th><th>Фамилия</th><th>Телефон</th><th>Дата</th><th>Гражданство</th>
    </tr>
    <tr v-for="user in users">
      <td>{{user.firstName}}</td>
      <td>{{user.lastName}}</td>
      <td>{{user.phoneNumber}}</td>
      <td>{{user.registrationDate}}</td>
      <td>{{user.citizenship}}</td>
    </tr>
  </table>
  <div>
    <div class="pagesBox" v-for="pageN in totalPages" :key="pageN" @click="rePage(pageN)">
      <button :class="{'current-page': page === pageN}">{{pageN}}</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "UsersInfoPage",
  data(){
    return{
      users: [{
        firstName: "",
        middleName: "",
        lastName: "",
        phoneNumber: "",
        instagramLink: "",
        citizenship: "",
        registrationDate: ""
      }],
      totalPages: 1,
      page: 1,
      filterString: "",
      pageVm: {
        totalPages: 0,
        pageNumber: 0
      }
    }
  },
  methods:{
    async getUsersData(){
      try {
        const response = await axios.get("https://localhost:7210/user/getusersdata", {
          params: {
            filterString: this.filterString,
            page: this.page
          }
        })
        this.users = response.data.users
        this.pageVm = response.data.pageVm
        this.totalPages = this.pageVm.totalPages
        console.log("Response:")
        console.log(response)
        console.log(this.users)
        console.log(`tpages ${this.totalPages}`)
      }
      catch (e){
        alert(e.message)
      }
    },
    rePage(pageN){
      this.page = pageN
      console.log(this.page)
      this.getUsersData()
    }
  },
  updated() {
    // this.getUsersData()
  },
  mounted() {
    this.getUsersData()
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
