<template>
<div class="course-box">
  <div class="q-pa-md" style="max-width: 300px">
    <q-input
      ref="inputRef"
      filled
      v-model="courseData.name"
      label="Название курса"
      :rules="[ myRule ]"
    />
  </div>
  <div class="q-pa-md" style="max-width: 300px">
    <div class="q-gutter-md">
      <q-input
        ref="inputRef"
        filled
        v-model="courseData.cost"
        label="Стоимость курса"
        type="number"
        :rules="[ myRule ]"
      />
    </div>
  </div>
  <div class="q-pa-md" style="max-width: 700px">
    <q-input
      v-model="courseData.description"
      filled
      type="textarea"
      label="Описание"
      :rules="[ myRule ]"
    />
  </div>

  <div class="q-pa-md" style="max-width: 700px">
    <q-input
      v-model="courseData.videoLink"
      filled
      label="Адрес для видео"
      type="textarea"
    />
  </div>
  <div class="q-pa-md" style="max-width: 300px">
    <div class="q-gutter-md">
      <q-select
        label="isActive"
        transition-show="scale"
        transition-hide="scale"
        filled
        v-model="courseData.isActive"
        :options="options"
        style="width: 250px"
      />
    </div>
  </div>
  <q-btn color="black" label="Изменить курс" @click="submitCourse" />

</div>

  <div class="module" v-if="showModules">


    <div class="module-list">
      <div>Все модули</div>
      <div class="module-item" v-for="module in allModules" :key="module.id">
        <div class="module-title">
          {{ module.title }}
          <svg @click="addModule(module)" width="20px" height="20px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z"/></svg>
        </div>
      </div>
    </div>

    <div class="module-list">
      <div>Модули для курса</div>
      <vue-draggable-next :list="forCreateModules" @change="log">
        <div class="module-item" v-for="module in forCreateModules" :key="module.id">
          <div class="module-title">
            {{ module.title }}
            <svg @click="deleteModule(module)" width="20px" height="20px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><!--! Font Awesome Pro 6.2.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2022 Fonticons, Inc. --><path d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z"/></svg>
          </div>
        </div>
      </vue-draggable-next>
    </div>
  </div>

  <q-btn class="submitModuleButton" color="black" label="Добавить модули" v-if="showModules" @click="submitChangeModules" />
</template>

<script>
import { ref } from 'vue'
import notify from "boot/notifyes";
import {VueDraggableNext} from "vue-draggable-next";
import {
  getAllModules,
  getCourse,
  editCourse,
  getCourseModulesId,
  getModulesByListId,
  changeCourseModules
} from "boot/axios";

export default{
  data(){
    return{
      courseId: this.$route.params.id,
      showModules: false,
      showCourse: false,
      allModules: [],
      responceDataCourse: {},
      forCreateModules: []
    }
  },
  components: {
    VueDraggableNext
  },
  setup () {
    const inputRef = ref(null)
    return {
      search: ref(''),
      courseData: ref({
        name: ref(''),
        cost: ref(null),
        description: ref(''),
        isActive: ref(),
        videoLink: ref()
      }),
      options: [
        true, false
      ],
      inputRef,
    }
  },
  mounted() {
    this.getAllModules();
    this.getCourse(this.courseId);
    this.getCourseModules(this.courseId);
  },
  methods:{
    async getCourse(){
        try {
          const response = await getCourse(this.courseId);
          
          if (response.data.isSuccess) {
            this.courseData.name = response.data.value.name
            this.courseData.cost = response.data.value.cost
            this.courseData.description = response.data.value.description
            this.courseData.isActive = response.data.value.isActive
            this.courseData.videoLink = response.data.value.videoLink
            notify.showSucsessNotify("Курс получен");
          }
          else {
            response.data.errors.forEach(element => { notify.showErrorNotify(element); });
          }
        } catch (e) {
          notify.showErrorNotify(e.message);
        }
    },
    async submitCourse() {

      //ToDo
      try {
        this.courseData.id = this.courseId;
        const response = await editCourse(this.courseData);
        
        if (response.data.isSuccess) {
            this.showModules = true
            notify.showSucsessNotify("Курс отредактирован");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    //ToDo
    async submitChangeModules() {
      try {
        const dataForModulesChange = {
          courseId: this.courseData.id,
          modulesId: []
        }
        this.forCreateModules.forEach(el => dataForModulesChange.modulesId.push(el.id))
        console.log(dataForModulesChange)
        const response = await changeCourseModules(dataForModulesChange);
        console.log(response.data)
        if (response.data.isSuccess) {
            notify.showSucsessNotify("Модули добавлены");
            this.showModules = false
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    //ToDo
    async getAllModules(){
      try {
        const response = await getAllModules();
        
        if (response.data.isSuccess) {
          this.allModules = response.data.value;
          notify.showSucsessNotify("Все модули получены");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    async getCourseModules(){
      try {
        const response = await getCourseModulesId(this.courseId);
            
        if (response.data.isSuccess) {
          const modulesId = response.data.value;
          notify.showSucsessNotify("Все id модулей курса получены");
          try {
            const response = await getModulesByListId(modulesId);
            
            if (modulesId.length > 0){
              if (response.data.isSuccess) {
                this.forCreateModules = response.data.value;
                notify.showSucsessNotify("Все модули курса получены");
              }
              else {
                response.data.errors.forEach(element => { notify.showErrorNotify(element); });
              }
            }
          } catch (e) {
            notify.showErrorNotify(e.message);
          }
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    addModule(module){
      for (let i = 0, len = this.forCreateModules.length; i < len; i++) {
        if (this.forCreateModules[i].id === module.id) {
          notify.showErrorNotify("Этот модуль уже имеется в данном курсе");
          return;
        }
      }
      this.forCreateModules.push(module)
    },
    deleteModule(module){

      for (let i = 0, len = this.forCreateModules.length; i < len; i++) {
        if (this.forCreateModules[i] === module) {
          this.forCreateModules.splice(i, 1);
          break;
        }
      }
    }
  }
}
</script>
<style scoped>
.module{
  display: flex;
  justify-content: center;
  margin: 50px 0;
}
.module-list{
  padding: 5px 20px;
  margin: 0 10px;
  border: 1px solid #EEE;
  font-family: sans-serif;
  width: 250px;
  text-align: center;
}
.module-title{
  padding: 5px;
  font-size: 20px;
  font-weight: 600;
  margin-bottom: 10px;
  border-bottom: 1px solid #EEE;
}
.module-item{
  padding: 5px 20px;
  border: 1px solid #DDD;
}
.module-item:hover{
}

.submitModuleButton{
  margin: 0 auto;
  display: block;;
}
</style>
