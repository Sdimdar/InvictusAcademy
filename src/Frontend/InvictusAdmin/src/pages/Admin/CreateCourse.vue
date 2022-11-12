<template>
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
<!--      <q-select v-model="courseData.isActive" :options="options" label="isActive" />-->
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
  <q-btn color="black" label="Создать курс" @click="submitCourse" />




  <div class="search-modules-box" v-if="showModules">
    <div class="search-box">
      <div class="q-pa-md" style="max-width: 350px">
        <div class="q-gutter-md">
          <div class="search-box">
            <q-badge color="teal">Model: "{{ searchModels }}"</q-badge>
          </div>

          <q-input
            v-model="searchModels"
            debounce="500"
            filled
            placeholder="Search model"
            hint="Debouncing 500ms"
          >
            <template v-slot:append>
              <q-icon name="search"/>
            </template>
          </q-input>
        </div>
      </div>
    </div>
  </div>
  <div class="module" v-if="showModules">

    <div class="module-list">
      <div>Все модули</div>
      <div class="module-item" v-for="module in searchedModules" :key="module.id">
        <div class="module-title">
          {{ module.title }} <q-icon name="style" size="25px" @click="addModule(module)"/>
        </div>
      </div>
    </div>

<!--    <vue-draggable-next :list="forCreateModules" @change="log">-->
<!--    <vue-draggable-group>-->
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
<!--    </vue-draggable-group>-->
<!--    </vue-draggable-next>-->

  </div>

  <q-btn class="submitModuleButton" color="black" label="Добавить модули" v-if="showModules" @click="submitInsertModules" />
</template>

<script>
import { ref } from 'vue'
import notify from "boot/notifyes";
import {VueDraggableNext} from "vue-draggable-next";
import {getAllModules, insertModules, createCourse} from "boot/axios";

export default{
  data(){
    return{
      courseData:{
        name: '',
        cost: 1,
        description: '',
        isActive: true,
        videoLink: ""
      },
      showModules: false,
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
        // {
        //   id: 1,
        //   title: 'Learning vue'
        // },
        // {
        //   id: 2,
        //   title: 'Learning React'
        // },
        // {
        //   id: 3,
        //   title: 'Learning JS'
        // },
      ]
    }
  },
  components: {
    VueDraggableNext
  },
  setup () {
    const inputRef = ref(null)

    return {
      searchModels: ref(''),
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
  computed:{
   searchedModules(){
     return this.allModules.filter(module=>module.title.toLowerCase().includes(this.searchModels.toLowerCase()))
   },
  },
  mounted() {
    this.getAllModules()
  },
  methods:{
    async submitCourse() {
      try {
        console.log(this.courseData)
        const response = await createCourse(this.courseData);
        console.log(response)
        if (response.data.isSuccess) {
          console.log("Succes")
          console.log(response)
          this.responceDataCourse = response.data.value;
          console.log(response.data.value)
          notify.showSucsessNotify("Курс создан");
          this.showModules = true
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
      // this.showModules = true //DeleteAfter
    },
    //ToDo
    async submitInsertModules() {
      console.log(this.forCreateModules)
      try {
        const dataForModulesInsert = {
          courseId: this.responceDataCourse.id,
          modulesId: [],
          startIndex: -1 //Todo?
        }
        this.forCreateModules.forEach(el => dataForModulesInsert.modulesId.push(el.id))
        const response = await insertModules(dataForModulesInsert);
        console.log("responce data")
        console.log(this.responceDataCourse)
        console.log("modules")
        console.log(response)
        if (response.data.value.isSuccess) {
            notify.showSucsessNotify("Модули добавлены");
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
        console.log(response.data)
        if (response.data.value.isSuccess) {
          this.allModules = response.data.value.value;
          notify.showSucsessNotify("Все модули получены");
        }
        else {
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    addModule(module){
      console.log(module)
      for (let i = 0, len = this.forCreateModules.length; i < len; i++) {
        if (this.forCreateModules[i] === module) {
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
    },
    log(){
      console.log(this.forCreateModules)
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
