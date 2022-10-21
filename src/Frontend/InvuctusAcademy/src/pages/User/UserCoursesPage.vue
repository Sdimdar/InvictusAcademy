<template>
  <div>
    <h4 style="text-align: center;">Ваши курсы</h4>

    <q-splitter v-model="splitterModel" style="height: 750px">

      <template v-slot:before>
        <q-tabs v-model="tab" vertical class="text-teal">
          <q-tab name="current" icon="mail" label="Текущие" />
          <q-tab name="completed" icon="alarm" label="Завершённые" />
          <q-tab name="wish" icon="movie" label="Желаемые" />
        </q-tabs>
      </template>

      <template v-slot:after>
        <q-tab-panels v-model="tab" animated swipeable vertical transition-prev="jump-up" transition-next="jump-up">
          <q-tab-panel name="current">
            <div class="text-h4 q-mb-md">Текущие</div>
            <course-card class="list-card" v-for="course in currentCourses" :data="course"/>
          </q-tab-panel>

          <q-tab-panel name="completed">
            <div class="text-h4 q-mb-md">Завершённые</div>
            <course-card class="list-card"  v-for="course in completedCourses" :data="course"/>
          </q-tab-panel>

          <q-tab-panel name="wish">
            <div class="text-h4 q-mb-md">Желаемые</div>
            <course-card class="list-card"  v-for="course in wishedCourses" :data="course"/>
          </q-tab-panel>
        </q-tab-panels>
      </template>

    </q-splitter>
  </div>
</template>

<script>
import { ref } from 'vue'
import CourseCard from 'components/CourseCard.vue'
import { getCurrentCourses, getCompletedCourses, getWishedCourses} from 'boot/axios'

export default {
  props: {
    logined : {
      Type : Boolean
    }
  },
  setup() {
    return {
      tab: ref("current"),
      splitterModel: ref(20)
    };
  },
  mounted(){
    if(!this.logined)
    {
      this.$router.push({ name: 'homepage' })
    }
    this.getCoursesData();
  },
  data() {
    return{
      currentCourses:[
        {
          title: "Course 1",
          courseDescription: "Some Description about course",
          purchased: true
        },
        {
          title: "Course 2",
          courseDescription: "Some Description about course",
          purchased: true
        }
      ],
      completedCourses:[
        {
          title: "Course 3",
          courseDescription: "Some Description about course",
          purchased: true
        }
      ],
      wishedCourses:[
        {
          title: "Course 4",
          courseDescription: "Some Description about course",
          purchased: false
        }
      ]
    }
  },
  methods:{
    async getCoursesData()
    {
      try {
        const response = await getCurrentCourses();
        if (response.data.isSuccess) {
          this.currentCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }
      try {
        const response = await getCompletedCourses();
        if (response.data.isSuccess) {
          this.completedCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }
      try {
        const response = await getWishedCourses();
        if (response.data.isSuccess) {
          this.wishedCourses = response.data.value.courses;
        }
      } catch (error) {
        console.log(error.message);
      }
    }
  },
  components: { CourseCard }
}
</script>

<style lang="scss" scoped>
.list-card{
  margin-bottom: 10px;
}
</style>