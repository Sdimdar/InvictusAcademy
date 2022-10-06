<template>
   <div class="search-box">
     <q-input class="input-search" v-model="filterString" filled type="search" hint="Search">
       <template v-slot:append>
         <q-icon name="search" />
       </template>
     </q-input>
     <div>
       <q-btn class="search-btn" @click="getUsersData" label="Найти" type="submit" color="primary"/>
     </div>
   </div>
    <div class="q-pa-md">
      <q-markup-table>
        <thead>
        <tr>
          <th class="text-left">Dessert (100g serving)</th>
          <th class="text-right">Calories</th>
          <th class="text-right">Fat (g)</th>
          <th class="text-right">Carbs (g)</th>
          <th class="text-right">Protein (g)</th>
          <th class="text-right">Sodium (mg)</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="user in users">
          <td class="text-left">{{user.firstName}}</td>
          <td class="text-right">{{user.lastName}}</td>
          <td class="text-right">{{user.lastName}}</td>
          <td class="text-right">{{user.phoneNumber}}</td>
          <td class="text-right">{{user.registrationDate}}</td>
          <td class="text-right">{{user.citizenship}}</td>
        </tr>
        </tbody>
      </q-markup-table>
    </div>
    <div class="q-pa-lg flex flex-center">
      <q-pagination
        v-model="current"
        :max="pageVm.totalPages"
        @click="rePage(current)"
      />
    </div>
</template>

<script>
import { fetchUsersData } from "boot/axios";
import { Notify } from "quasar";
import { ref } from 'vue';

export default {
  setup () {
    return {
      current: ref(1)
    }
  },
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
      page: 1,
      pageSize: 1,
      filterString: "",
      pageVm: {
        totalPages: 0,
        pageNumber: 0
      },
    }
  },
  methods:{
    async getUsersData(){
      try {
        const response = await fetchUsersData(this.filterString, this.pageSize, this.page);
        console.log("Response:")
        console.log(response)
        if (response.data.isSuccess) {
          this.users = response.data.value.users
          this.pageVm = response.data.value.pageVm
          if(this.users.length == 0)
          {
            Notify.create({
              color: "yellow-4",
              textColor: "black",
              icon: "warning",
              message: `Не найдено ни одного пользователя по запросу : ${this.filterString}`,
            });
          }
          console.log(`Pages count: ${this.pageVm.totalPages}`)
        }
        else{
          Notify.create({
            color: "red-5",
            textColor: "white",
            icon: "warning",
            message: response.data.errors[0],
          });
        }
      }
      catch (e){
        console.log(e.message)
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
  },
}
</script>

<style scoped>
.input-search{
  width: 340px;
  margin-left: 20px;
}
.search-box{
  display: inline-flex;
}
.search-btn{
  height: 55px;
  width: 100px;
}
</style>
