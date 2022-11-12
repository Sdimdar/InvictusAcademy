<template>
  <div class="search-box">
    <div class="q-pa-md" style="max-width: 350px">
      <div class="q-gutter-md">
        <div class="search-box">
          <q-badge color="teal">Model: "{{ filterCourse }}"</q-badge>
        </div>

        <q-input
          v-model="filterCourse"
          debounce="500"
          filled
          placeholder="Search"
          hint="Debouncing 500ms"
        >
          <template v-slot:append>
            <q-icon name="search"/>
          </template>
        </q-input>
      </div>
    </div>
  </div>
  <div class="mobile-table">
    <table class="iksweb">
      <thead>
      <tr>
        <th>Id курса</th>
        <th>Название курса</th>
        <th>Стоимость</th>
        <th>Изменить</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="course in searchedCourse">
        <td>{{course.id}}</td>
        <td>{{course.name}}</td>
        <td>{{course.cost}}</td>
        <td><q-btn @click="$router.push(`/admin-panel/editCourse/${course.id}`)"  class="nav-button" label="Редактировать" ></q-btn></td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import {getAllCourses} from "boot/axios";
import notify from "boot/notifyes";

export default {
  name: "AllCourses",
  components:{
  },
  data(){
    return{
      courses: [
      ],
      filterCourse: '',
      page: 1,
      limit: 15,
      current: 1
    }
  },
  computed:{
    searchedCourse(){
      return this.courses.filter(course=>course.name.toLowerCase().includes(this.filterCourse.toLowerCase()))
    }
  },
  mounted() {
    this.getAllCoursesMounted()
  },
  methods: {
    async getAllCoursesMounted() {
      try {
        const response = await getAllCourses();
        this.courses = response.data.value.courses;
        console.log("Responce:")
        console.log(response)
        if (response.data.isSuccess) {
          console.log(response)
          this.courses = response.data.value.courses;
          console.log(this.courses)
          notify.showSucsessNotify("Все курсы получены");
        } else {
          response.data.errors.forEach(element => {
            notify.showErrorNotify(element);
          });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    async getAllCoursesTest(){
      await getAllCourses().then(function (response) {
        this.courses = response.data.value
        // console.log(this.courses)
        console.log("-------------------------")
        console.log(self.courses)
      })
    }
  }
}
</script>

<style scoped>
table.iksweb{text-decoration: none;border-collapse:collapse;width:100%;text-align:center;}
table.iksweb th{font-weight:normal;font-size:20px; color:#ffffff;background-color:#1287db;}
table.iksweb td{font-size:15px;color:#000000;}
table.iksweb td,table.iksweb th{white-space:pre-wrap;padding:25px 23px;line-height:11px;vertical-align: middle;border: 1px solid #1593ed;}	table.iksweb tr:hover{background-color:#0095ff}
table.iksweb tr:hover td{color:#000000;cursor:default;}
.mobile-table{width: 100%; max-width: 100%; overflow-x: auto;}
</style>
