<template>
  <div class="search-box">
    <div class="q-pa-md" style="max-width: 350px">
      <div class="q-gutter-md">
        <div class="search-box">
          <q-badge color="teal">Model: "{{ search }}"</q-badge>
        </div>

        <q-input
          v-model="search"
          debounce="500"
          filled
          placeholder="Search"
          hint="Debouncing 500ms"
        >
          <template v-slot:append>
            <q-icon name="search" @click="getCourse"/>
          </template>
        </q-input>
      </div>
    </div>
  </div>
  <div class="course-box" v-if="showCourse">
    <div class="q-pa-md" style="max-width: 300px">
      <q-input
        ref="inputRef"
        filled
        v-model="courseData.model"
        label="Название курса"
        :rules="[ myRule ]"
      />
    </div>
    <div class="q-pa-md" style="max-width: 300px">
      <div class="q-gutter-md">
        <q-input
          filled
          v-model="courseData.price"
          label="Стоимость курса"
          mask="#.##"
          fill-mask="0"
          reverse-fill-mask
          hint="Mask: #.##"
          input-class="text-right"
        />
      </div>
    </div>
    <div class="q-pa-md" style="max-width: 700px">
      <q-input
        v-model="courseData.text"
        filled
        type="textarea"
        :rules="[ myRule ]"
      />
    </div>
    <q-btn color="black" label="Редактировать курс" @click="submitCourse" />
  </div>


  <div class="module" v-if="showModules">


    <div class="module-list">
      <div class="module-item" v-for="module in allModules" :key="module.id">
        <div class="module-title">
          {{ module.title }} <q-icon name="style" size="25px" @click="addModule(module)"/>
        </div>
      </div>
    </div>


    <vue-draggable-group>
      <div class="module-list">
        <vue-draggable-next>
          <div class="module-item" v-for="module in forCreateModules" :key="module.id">
            <div class="module-title">
              {{ module.title }} <q-icon name="warning" color="warning" size="25px" @click="deleteModule(module)"/>
            </div>
          </div>
        </vue-draggable-next>
      </div>
    </vue-draggable-group>

  </div>

  <q-btn class="submitModuleButton" color="black" label="Добавить модули" v-if="showModules" @click="submitInsertModules" />
</template>

<script>
import { ref } from 'vue'
import notify from "boot/notifyes";
import {VueDraggableNext} from "vue-draggable-next";
import {getAllModules, insertModules} from "boot/axios";

export default{
  data(){
    return{
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
      search: ref(''),
      courseData: ref({
        model: ref(''),
        price: ref(null),
        text: ref('')
      }),
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
  // mounted() {
  //   this.getAllModules();
  // },
  methods:{
    async getCourse(){
      this.showCourse = true
    },
    async submitCourse() {

      //ToDo
      // try {
      //   const response = await createCourse(this.courseData);
      //   if (response.data.isSuccess) {
      //     this.responceDataCourse = response.data.value;
      //     if (courseData.id >= 0){
      //       this.showModules = true
      //       notify.showSucsessNotify("Курс создан");
      //     }
      //   }
      //   else {
      //     response.data.errors.forEach(element => { notify.showErrorNotify(element); });
      //   }
      // } catch (e) {
      //   notify.showErrorNotify(e.message);
      // }
      this.showModules = true //DeleteAfter
      this.showCourse = false
    },
    //ToDo
    async submitInsertModules() {
      // try {
      //   const dataForModulesInsert = {
      //     id: this.responceDataCourse.id,
      //     modulesId: [],
      //     startIndex: 0 //Todo?
      //   }
      //   this.forCreateModules.forEach(el => dataForModulesInsert.modulesId.push(el.id))
      //   const response = await insertModules(dataForModulesInsert);
      //   if (response.data.isSuccess) {
      //       notify.showSucsessNotify("Модули добавлены");
      //   }
      //   else {
      //     response.data.errors.forEach(element => { notify.showErrorNotify(element); });
      //   }
      // } catch (e) {
      //   notify.showErrorNotify(e.message);
      // }
      this.showModules = false
    },
    //ToDo
    // async getAllModules(){
    //   try {
    //     const response = await getAllModules();
    //     if (response.data.isSuccess) {
    //       this.allModules = response.data.value;
    //       notify.showSucsessNotify("Все модули получены");
    //     }
    //     else {
    //       response.data.errors.forEach(element => { notify.showErrorNotify(element); });
    //     }
    //   } catch (e) {
    //     notify.showErrorNotify(e.message);
    //   }
    // },
    addModule(module){
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

.search-box{

}
</style>
