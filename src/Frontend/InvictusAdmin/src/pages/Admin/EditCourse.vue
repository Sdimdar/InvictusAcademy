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
          {{ module.title }} <q-icon name="style" size="25px" @click="addModule(module)"/>
        </div>
      </div>
    </div>

    <div class="module-list">
      <div>Модули для курса</div>
      <vue-draggable-next :list="forCreateModules" @change="log">
        <div class="module-item" v-for="module in forCreateModules" :key="module.id">
          <div class="module-title">
            {{ module.title }} <q-icon name="warning" color="warning" size="25px" @click="deleteModule(module)"/>
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
      allModules: [
        {
          id: 1,
          title: 'DO vue',
          shortDescription: '',
          articles: []
        },
        {
          id: 2,
          title: 'DO React',
          shortDescription: '',
          articles: []
        },
        {
          id: 3,
          title: 'DO JS',
          shortDescription: '',
          articles: []
        },
        {
          id: 4,
          title: 'Learning vue',
          shortDescription: '',
          articles: []
        },
        {
          id: 5,
          title: 'Learning React',
          shortDescription: '',
          articles: []
        },
        {
          id: 6,
          title: 'Learning JS',
          shortDescription: '',
          articles: []
        },
      ],
      responceDataCourse: {
        id: 0,
        name: "",
        description: ""
      },
      forCreateModules: [
      ]
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
      // model: ref(''),
      inputRef,
      // price: ref(null),
      // text: ref(''),

      myRule (val) {
        // simulating a delay

        return new Promise((resolve, reject) => {
          setTimeout(() => {
            // call
            //  resolve(true)
            //     --> content is valid
            //  resolve(false)
            //     --> content is NOT valid, no error message
            //  resolve(error_message)
            //     --> content is NOT valid, we have error message
            resolve(!!val || '* Required')

            // calling reject(...) will also mark the input
            // as having an error, but there will not be any
            // error message displayed below the input
            // (only in browser console)
          }, 1000)
        })
      }
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
          console.log(response.data.value.value)
          if (response.data.value.isSuccess) {
            this.courseData.name = response.data.value.value.name
            this.courseData.cost = response.data.value.value.cost
            this.courseData.description = response.data.value.value.description
            this.courseData.isActive = response.data.value.value.isActive
            this.courseData.videoLink = response.data.value.value.videoLink
            notify.showSucsessNotify("Курс получен");
          }
          else {
            response.data.value.errors.forEach(element => { notify.showErrorNotify(element); });
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
        if (response.data.value.isSuccess) {
            notify.showSucsessNotify("Модули добавлены");
            this.showModules = false
        }
        else {
          response.data.value.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    //ToDo
    async getAllModules(){
      try {
        const response = await getAllModules();
        if (response.data.value.isSuccess) {
          this.allModules = response.data.value.value;
          notify.showSucsessNotify("Все модули получены");
        }
        else {
          response.data.value.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    async getCourseModules(){
      try {
        const response = await getCourseModulesId(this.courseId);
        if (response.data.value.isSuccess) {
          const modulesId = response.data.value.value;
          notify.showSucsessNotify("Все id модулей курса получены");
          try {
            const response = await getModulesByListId(modulesId);
            console.log("Modules")
            console.log(modulesId)
            console.log(response)
            if (modulesId.length > 0){
              if (response.data.value.isSuccess) {
                this.forCreateModules = response.data.value.value;
                notify.showSucsessNotify("Все модули курса получены");
              }
              else {
                response.data.value.errors.forEach(element => { notify.showErrorNotify(element); });
              }
            }
          } catch (e) {
            notify.showErrorNotify(e.message);
          }
        }
        else {
          response.data.value.errors.forEach(element => { notify.showErrorNotify(element); });
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
