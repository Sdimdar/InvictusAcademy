<template>
  <table class="table">
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
    <div v-for="pageN in totalPages" :key="pageN" @click="rePage(pageN)">
      <button>{{pageN}}</button>
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

</style>
